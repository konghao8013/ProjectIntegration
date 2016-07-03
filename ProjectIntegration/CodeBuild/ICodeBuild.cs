using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuild
{
    public interface ICodeBuild
    {
        void Build(BuildContext context);
        void Publish(BuildContext context);
    }
}
