using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetBuilderServer.model
{
    public class LogType
    {
        public String CreateTime { set; get; }
        public String Title { set; get; }
        public String Content { set; get; }

        public static void WriteLog(string project, string title, string content)
        {
            var log = new LogType();
            log.CreateTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            log.Title = content;
            log.Content = content;
            string path = CheckProjectLogPath(project);
            var guid = Guid.NewGuid().ToString("N");
            path = path + "\\" + title + "_" + guid + ".json";
            CheckFile(path);
            var fs = new StreamWriter(path, false, Encoding.UTF8);
            fs.WriteLine(log.Serialize());
            fs.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 返回项目日志文件夹路劲
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private static string CheckProjectLogPath(string project)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\log";
            CheckDirectory(path);
            path = path + "\\" + project;
            CheckDirectory(path);
            return path;
        }
        /// <summary>
        /// 返回最近50条操作日志
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        public static List<LogType> GetLogByProjectName(string projectName, string like)
        {
            var list = new List<LogType>();
            string path = CheckProjectLogPath(projectName);
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles("*" + like + "*").OrderByDescending(a => a.CreationTime).Take(100);
            foreach (var item in files)
            {

                var sr = new StreamReader(item.FullName);
                var value = sr.ReadToEnd();
                var type = value.Deserialize<LogType>();
                if (type != null)
                {
                    list.Add(type);
                }
            }
            return list;
        }
        static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                var fs = File.CreateText(path);
                fs.Close();
                fs.Dispose();
            }
        }
    }
}
