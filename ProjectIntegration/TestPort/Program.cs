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
            var monitor = new MonitorPort(817);
            monitor.Start();
            Console.ReadLine();
        }
    }
}
