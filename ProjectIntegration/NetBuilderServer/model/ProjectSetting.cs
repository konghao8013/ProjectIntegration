using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class ProjectSetting
    {
        public string ProjectName { set; get; }
        public Publish Publish { set; get; }

        public Solution Solution { set; get; }

        public CodeMgr CodeMgr { set; get; }

        public String Status { set; get; }

        public string LastTime { set; get; }


    }
}
