using LibExtend.NetworkServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBuilderServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace NetBuilderServer.Tests
{
    [TestClass()]
    public class MonitorPortTests
    {
        [TestMethod()]
        public void StartTest()
        {
            var monitor = new MonitorPort(817);
            monitor.Start();
      
            var help = new HttpHelper();
            var str = help.HttpGet("http://127.0.0.1:817/getprojects", "");
            if (str.Length > 0)
            {
                Debug.WriteLine(str);

            }
            else
            {
                Assert.Fail();
            }
            monitor.Stop();

        }
    }
}