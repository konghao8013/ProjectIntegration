using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBuilderServer
{
    public class LoseContext
    {
        public string Path { set; get; }
        public List<LoseTypeEnum> Lose = new List<LoseTypeEnum>();
    }
}
