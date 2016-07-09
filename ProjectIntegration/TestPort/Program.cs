using NetBuilderServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPort
{
    class Program
    {
        static void Main(string[] args)
        {
            var prot = System.Configuration.ConfigurationManager.AppSettings["prot"];
            if (prot.Length > 0)
            {
                var monitor = new MonitorPort(int.Parse(prot));
                monitor.Start();
            }
            Console.ReadLine();
        }
    }
}
