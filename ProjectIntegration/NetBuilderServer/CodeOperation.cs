using NetBuilderServer.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class CodeOperation
    {
        public void Builder()
        {

        }
        public static void RemoveProject(string name)
        {
            Tool.ProjectSettingServe.Remove(name);
        }
        public static string GetProjectList()
        {
            return Tool.ProjectSettingServe.ProjectList().Serialize();
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
