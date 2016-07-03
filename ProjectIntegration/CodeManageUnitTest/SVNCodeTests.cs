using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace CodeManage.Tests
{
    [TestClass()]
    public class SVNCodeTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            var target = @"D:\test\bBA";
            var key = IdentityKey.CreateIdentityKey("konghao", "111111", RTypeEnum.SVN);
            ICodeFile svn = new SVNCode("https://DESKTOP-1O16UC8/svn/AutoTest/",target,key);
            var log=svn.Clone().Log;
            
            var dir = new DirectoryInfo(target);
            if (dir.GetDirectories().Count() == 0)
                Assert.Fail(log);
            else {
               Debug.WriteLine(log);
            }
        }
    }
}