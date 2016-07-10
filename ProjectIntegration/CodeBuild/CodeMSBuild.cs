﻿using LibExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuild
{
    public class CodeMSBuild : ACodeBuild
    {
        public override void Build(BuildContext context)
        {
            var p = new Command();
            p.Call(context.BuildPath + " " + context.CodePath + " /maxcpucount:4");
            p.Exit();
            context.Log = p.CommadnLog;
        }

        public override void Publish(BuildContext context)
        {
            var p = new Command();
            var ax = @"D:\KongHao\KongHao\Code\git\ProjectIntegration\ProjectIntegration\WebNetBuilder\Properties\PublishProfiles\ds.pubxml";
            var command = new StringBuilder();
            command.Append(context.BuildPath);
            command.Append(" ");
            command.Append(context.PublishItemPath);
            command.Append(@" /t:ResolveReferences;Compile /t:_WPPCopyWebApplication  /p:Configuration=Release /maxcpucount:4");
            command.Append(" /p:WebProjectOutputDir=");
            command.Append(context.publishDirectory);
            p.Call(command.ToString());
            p.Exit();
            context.Log = p.CommadnLog;
        }
    }
}
