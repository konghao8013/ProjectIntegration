using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace NetBuilderServer.dal
{
    public class ProjectSettingServer
    {
        public void SaveProject(ProjectSetting settting)
        {

            var list = ProjectList();
            var project = list.FirstOrDefault(a => a.ProjectName == settting.ProjectName);
            if (project != null)
            {
                list.Remove(project);

            }
            list.Add(settting);
            SaveProject(list);
        }

        private void SaveProject(List<ProjectSetting> list)
        {
            var json = list.Serialize();
            SaveFile(json);
        }

        public void Remove(string projectName)
        {
            var list = ProjectList();
            var project = list.FirstOrDefault(a => a.ProjectName == projectName);
            if (project != null)
            {
                list.Remove(project);

            }
            SaveProject(list);
        }
        public List<ProjectSetting> ProjectList()
        {
            var result = GetFile();
            var list = result.Deserialize<List<ProjectSetting>>();
            if (list == null)
            {
                list = new List<ProjectSetting>();
            }
            return list.Where(a => a != null).ToList();
        }
        public ProjectSetting GetProjectbyName(string name) {
            return ProjectList().FirstOrDefault(a => (!string.IsNullOrEmpty(a.ProjectName)) && a.ProjectName.ToLower() == name.ToLower());
        }
        public string ConfigPath
        {
            get
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\config"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\config");
                }
                var path = AppDomain.CurrentDomain.BaseDirectory + "\\config\\ProjectSetting.json";
                if (!File.Exists(path))
                {
                    var fs = File.CreateText(path);
                    fs.Close();

                    fs.Dispose();
                }
                return path;
            }
        }
        public void SaveFile(string value)
        {
            var sw = new StreamWriter(ConfigPath, false, Encoding.Default);

            sw.Write(value.ToArray());
            sw.Close();
            sw.Dispose();
        }
        public string GetFile()
        {
            var sr = new StreamReader(ConfigPath, Encoding.Default);
            var result = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return result;
        }
    }
}
