using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class Publish
    {

        /// <summary>
        /// 发布多个文件夹位置
        /// </summary>
        public List<Target> TargetDirectorys { set; get; }
        public List<LoseContext> LoseContext { set; get; }
        public string Path { set; get; }
    }
    public class Target
    {
        public string Path { set; get; }
        public string Guid { set; get; }
    }
}
