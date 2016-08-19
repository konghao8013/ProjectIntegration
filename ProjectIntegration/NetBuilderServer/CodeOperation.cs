using CodeBuild;
using CodeManage;
using NetBuilderServer.dal;
using NetBuilderServer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetBuilderServer
{
    public class ProjectStatus
    {
        public string ProjectName { set; get; }
        public ProjectStatusEnum Status { set; get; }
    }
    public enum ProjectStatusEnum
    {
        Start = 1,
        /// <summary>
        /// 进行中
        /// </summary>
        Underway = 2,
        Stop = 0,
        Error = 3
    }
    public class CodeOperation
    {
        static List<ProjectStatus> dicStatus = new List<ProjectStatus>();
        public static ProjectStatus GetStatus(string projectName)
        {
            var s = dicStatus.FirstOrDefault(a => a.ProjectName.ToLower() == projectName.ToLower());
            if (s == null)
            {
                s = new ProjectStatus { ProjectName = projectName, Status = ProjectStatusEnum.Start };
                dicStatus.Add(s);
            }
            return s;
        }
        public static ProjectStatusEnum Builder(string name)
        {

            var project = Tool.ProjectSettingServe.GetProjectbyName(name);
            var status = GetStatus(name);
            if (status.Status == ProjectStatusEnum.Underway)
            {
                return ProjectStatusEnum.Underway;
            }
            var thread = new Thread(() =>
            {


                lock (status)
                {
                    status.Status = ProjectStatusEnum.Underway;
                    try
                    {


                        if (project != null)
                        {
                            LogType.WriteLog(project.ProjectName, "开始执行生成任务", "开始生成项目文件");
                            //更新代码文件
                            UpdateCode(project);
                            //生成项目文件 取消生成项目文件
                            //BuildProject(project);
                            //发布项目文件
                            PubilshWeb(project);
                            LogType.WriteLog(project.ProjectName, "结束执行生成任务", "完成成项目文件");

                        }
                        else
                        {
                            LogType.WriteLog(name, "结束执行生成任务", "未找到指定的项目:" + name);
                        }
                    }
                    catch (Exception e)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine(e.Message);
                        sb.AppendLine(e.StackTrace);
                        LogType.WriteLog(project.ProjectName, "生成错误", sb.ToString());
                    }
                    finally
                    {
                        if (status.Status == ProjectStatusEnum.Underway)
                        {
                            status.Status = ProjectStatusEnum.Stop;
                        }
                    }

                }
            });
            thread.Start();
            return ProjectStatusEnum.Underway;
        }

        private static void PubilshWeb(ProjectSetting project)
        {
            if (project.Publish != null && project.Publish.TargetDirectorys.Count > 0)
            {
                var buildContext = CreateBuildContext(project);
                var build = ACodeBuild.CreateBuild(buildContext);
               
                var target = project.Publish.TargetDirectorys.FirstOrDefault().Path;
                buildContext.publishDirectory = target + "_temp_" + project.ProjectName; ;
                buildContext.PublishItemPath = project.Publish.Path;
                LogType.WriteLog(project.ProjectName, "发布网站项目", string.Format("开始发布网站项目:{0}", project.Publish.Path));
                if (!Directory.Exists(buildContext.publishDirectory))
                {
                    Directory.CreateDirectory(buildContext.publishDirectory);

                }
                LogType.WriteLog(project.ProjectName, "发布网站项目", string.Format("发布地址:{0}", buildContext.publishDirectory));
                try
                {
                    build.Publish(buildContext);
                }
                catch (Exception ee)
                {

                    LogType.WriteLog(project.ProjectName, "发布网站项目", string.Format("网站发布错误:{0}", ee.Message));
                }
                LogType.WriteLog(project.ProjectName, "发布网站项目", string.Format("网站项目发布完成:{0}:", buildContext.Log));
                var log = buildContext.Log;
                if (log.IndexOf("0 个错误") < 0)
                {
                    var status = GetStatus(project.ProjectName);
                    status.Status = ProjectStatusEnum.Error;
                }
                MoveToLose(project);/*生成后再移动忽略文件*/
                //拷贝发布成功的文件
                CopyPublish(project, buildContext);
                MoveToLose(project, false);


            }
        }

        private static void CopyPublish(ProjectSetting project, BuildContext buildContext)
        {
            var dir = new DirectoryInfo(buildContext.publishDirectory);

            foreach (var newPath in project.Publish.TargetDirectorys)
            {
                LogType.WriteLog(project.ProjectName, "发布成功正在拷贝文件", string.Format("拷贝目标:{0}", newPath.Path));
                if (!Directory.Exists(newPath.Path))
                {
                    LogType.WriteLog(project.ProjectName, "发布成功正在拷贝文件", string.Format("拷贝文件失败：{0}", newPath.Path));
                    continue;
                }
                try
                {
                    CopyFiles(dir, buildContext.publishDirectory, newPath.Path);
                    LogType.WriteLog(project.ProjectName, "发布成功正在拷贝文件", string.Format("拷贝文件成功：{0}", newPath.Path));
                }
                catch (Exception e)
                {

                    LogType.WriteLog(project.ProjectName, "发布成功正在拷贝文件", string.Format("拷贝文件成功：{0} msg:{1}", newPath.Path, e.Message));
                }


            }
            dir.Delete(true);
        }

        public static void CopyFiles(DirectoryInfo dir, string oldDir, string newDir)
        {

            var files = dir.GetFiles();
            foreach (var item in files)
            {
                var rP = newDir + GetRelativePath(item.FullName, oldDir);
                var rd = Path.GetDirectoryName(rP);
                if (!Directory.Exists(rd))
                {
                    Directory.CreateDirectory(rd);
                }
                item.CopyTo(rP, true);

            }
            var dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {
                CopyFiles(d, oldDir, newDir);
            }
        }
        public static string GetRelativePath(string fullName, string dirPath)
        {
            return fullName.Substring(dirPath.Length, fullName.Length - dirPath.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="isMove">true移动 false还原</param>
        private static void MoveToLose(ProjectSetting project, bool isMove = true)
        {
            LogType.WriteLog(project.ProjectName, "缓存忽略文件夹", string.Format("正在缓存忽略文件夹"));
            foreach (var t in project.Publish.TargetDirectorys)
            {
                foreach (var l in project.Publish.LoseContext)
                {
                    var path = Path.GetFullPath(t.Path + Path.DirectorySeparatorChar + l.Path);

                    var newPath = path + "_temp_" + project.ProjectName;
                    if (l.LoseType == LoseTypeEnum.Directory)
                    {
                        if (isMove)
                        {
                            if (Directory.Exists(newPath))
                            {
                                Directory.Delete(newPath, true);
                            }
                            if (Directory.Exists(path))
                            {
                                Directory.Move(path, newPath);
                            }
                        }
                        else
                        {
                            if (Directory.Exists(path))
                            {
                                Directory.Delete(path, true);
                            }
                            if (Directory.Exists(newPath))
                            {
                                Directory.Move(newPath, path);
                            }
                        }

                    }
                    else
                    {
                        if (isMove)
                        {
                            if (File.Exists(newPath))
                            {
                                File.Delete(newPath);
                            }

                            if (File.Exists(path))
                            {
                                File.Move(path, newPath);
                            }
                        }
                        else
                        {
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            if (File.Exists(newPath))
                            {
                                File.Move(newPath, path);
                            }
                        }

                    }
                }
            }
            LogType.WriteLog(project.ProjectName, "缓存忽略文件夹", string.Format("完成缓存忽略文件夹：{0}", isMove ? "缓存" : "还原"));
        }

        private static void BuildProject(ProjectSetting project)
        {
            BuildContext buildContext = null;

            ICodeBuild build = null;
            if (project.Solution != null)
            {
                buildContext = CreateBuildContext(project);
                build = ACodeBuild.CreateBuild(buildContext);
                LogType.WriteLog(project.ProjectName, "生成解决方案", string.Format("开始生成解决方案:{0}", buildContext.BuildPath));
                try
                {
                    build.Build(buildContext);
                }
                catch (Exception ee)
                {

                    LogType.WriteLog(project.ProjectName, "生成解决方案", string.Format("生成错误：{0}", ee.Message));
                }
              
                LogType.WriteLog(project.ProjectName, "生成解决方案", string.Format("解决方案生成完成：{0}", buildContext.Log));

            }
        }

        private static BuildContext CreateBuildContext(ProjectSetting project)
        {
            return BuildContext.CreateBuildContext(
    BuildTypeEnum.MSBuild,
   buildPath: project.Solution.BuilderPath,
   codePath: project.Solution.Path,
   publishItemPath: "",
   publishDirectory: "");
        }

        private static void UpdateCode(ProjectSetting project)
        {
            if (project.CodeMgr != null)
            {

                ICodeFile codeMgr = CreateCodeMgr(project.CodeMgr);
                LogType.WriteLog(project.ProjectName, "签出代码文件", string.Format("正在签出代码文件:{0}，请不要做其他操作", (project.CodeMgr.Source)));
                var rest = codeMgr.Clone();
                LogType.WriteLog(project.ProjectName, "签出代码文件", string.Format("代码文件签出成功：{0} msg:{1}", project.CodeMgr.Source, rest.Log));
            }
        }

        public static ICodeFile CreateCodeMgr(CodeMgr mgr)
        {

            ICodeFile result = null;
            if (mgr.Type == UserTypeEnum.SVN)
            {
                result = new SVNCode(mgr.Source, mgr.Target, IdentityKey.CreateIdentityKey(mgr.UserName, mgr.Pwd, RTypeEnum.SVN));
            }
            return result;
        }
        public static void RemoveProject(string name)
        {
            Tool.ProjectSettingServe.Remove(name);
        }
        public static string GetContent(string path)
        {
            var log = new LogType();
            if (File.Exists(path))
            {
                var sr = new StreamReader(path, Encoding.Default);
                var re = sr.ReadToEnd();

                sr.Close();
                sr.Dispose();
                log = re.Deserialize<LogType>();
            }
            return log.Content;
        }
        public static string GetLogByProject(string projectName, int take)
        {
            var logs = LogType.GetLogByProjectName(projectName, "", take).OrderByDescending(a => DateTime.Parse(a.CreateTime));
            var list = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            foreach (var l in logs)
            {

                sb.Append("标题:");
                sb.Append(l.Title);
                sb.Append(" 创建时间:");
                sb.AppendLine(l.CreateTime);
                sb.AppendLine(l.Content);
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string GetProjectList()
        {
            var list = Tool.ProjectSettingServe.ProjectList();
            foreach (var item in list)
            {
                var projectStatus = GetStatus(item.ProjectName);
                if (projectStatus != null)
                {
                    var status = "新建";
                    switch (projectStatus.Status)
                    {
                        case ProjectStatusEnum.Start:
                            status = "初始化项目";
                            break;
                        case ProjectStatusEnum.Underway:
                            status = "项目生成中";
                            break;
                        case ProjectStatusEnum.Stop:
                            status = "生成完成";
                            break;
                        case ProjectStatusEnum.Error:
                            status = "项目生成错误请查看日志";
                            break;
                    }
                    item.Status = status;
                    var log = LogType.GetLogByProjectName(item.ProjectName, "结束执行生成任务").OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    if (log != null)
                    {
                        item.LastTime = log.CreateTime;
                    }
                }


            }
            return list.Serialize();
        }
        public static bool SetProject(string project)
        {
            var result = false;
            try
            {
                Tool.ProjectSettingServe.SaveProject(project.Deserialize<ProjectSetting>());
                result = true;
            }
            catch (Exception e)
            {


            }
            return result;
        }
    }
}
