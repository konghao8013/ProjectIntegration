using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuild
{
    public abstract class ACodeBuild : ICodeBuild
    {
        public abstract void Build(BuildContext context);

        public abstract void Publish(BuildContext context);

        public static ICodeBuild CreateBuild(BuildContext context)
        {
            ICodeBuild build = null;
            switch (context.Type)
            {
                case BuildTypeEnum.MSBuild:
                    build = new CodeMSBuild();
                    break;

                default: break;
            }
            return build;
        }
    }
}
