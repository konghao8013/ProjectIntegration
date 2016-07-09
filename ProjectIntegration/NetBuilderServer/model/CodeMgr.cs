using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class CodeMgr
    {
        public string Source { set; get; }
        public string Target { set; get; }
        public string UserName { set; get; }
        public string Pwd { set; get; }
        public UserTypeEnum Type { set; get; }          
    }
}
