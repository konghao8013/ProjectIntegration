using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class LoseContext
    {
        public string Path { set; get; }
        public LoseTypeEnum LoseType
        {
            get
            {
                return Path[Path.Length - 1] == System.IO.Path.DirectorySeparatorChar ? LoseTypeEnum.Directory : LoseTypeEnum.File;
            }
        }
        public string Guid { set; get; }
    }
}
