using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CodeBuild.Tests
{
    [TestClass()]
    public class CodeMSBuildTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var context = BuildContext.CreateBuildContext(
                BuildTypeEnum.MSBuild,
               buildPath: @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe",
               codePath: @"D:\KongHao\KongHao\Code\git\AlosTRM\AlosNew.sln",
               publishItemPath: @"D:\KongHao\KongHao\Code\git\AlosTRM\WebServer\WebServer.csproj",
               publishDirectory: @"D:\TESTsvn\BBT"
                );
            var build = ACodeBuild.CreateBuild(context);
            build.Build(context);
            var log = context.Log;
            if (log.IndexOf("0 个错误") > -1)
            {
                Debug.WriteLine(log);
            }
            else
            {
                Debug.WriteLine(log);
                Assert.Fail();

            }


        }

        [TestMethod()]
        public void PublishTest()
        {
            var context = BuildContext.CreateBuildContext(
                BuildTypeEnum.MSBuild,
               buildPath: @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe",
               codePath: @"D:\KongHao\KongHao\Code\git\AlosTRM\AlosNew.sln",
               publishItemPath: @"D:\KongHao\KongHao\Code\git\AlosTRM\WebServer\WebServer.csproj",
               publishDirectory: @"D:\TESTsvn\BBT"
                );
            var build = ACodeBuild.CreateBuild(context);
            build.Publish(context);
            var log = context.Log;
            if (log.IndexOf("0 个错误") >-1)
            {
                Debug.WriteLine(log);
            }
            else
            {
                Debug.WriteLine(log);
                Assert.Fail();

            }
        }
    }
}