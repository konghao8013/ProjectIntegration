using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer.dal
{
    public class Tool
    {
        static ProjectSettingServer _projectSettingServer;
        public static ProjectSettingServer ProjectSettingServe
        {

            get
            {
                return Create(ref _projectSettingServer);
            }
        }
        static T Create<T>(ref T  t) where T : new() {
            if (t == null)
            {
                t = new T();
            }
            return t;
        }
    }
}
