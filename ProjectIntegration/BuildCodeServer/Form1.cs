using NetBuilderServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BuildCodeServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var prot = ConfigurationManager.AppSettings["prot"];
            if (prot.Length > 0)
            {
            
                var monitor = new MonitorPort(int.Parse(prot));
                monitor.Start();
                ShowInTaskbar = false;
                Opacity = 0d;
            }
            else
            {
                MessageBox.Show("端口号配置错误");
            }

        }
    }
}
