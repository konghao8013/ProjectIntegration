using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Command
{
    public Process Process { set; get; }
    public Process CreateProcess()
    {
        if (Process != null)
        {
            return Process;
        }
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        p.StartInfo.CreateNoWindow = true;//不显示程序窗口
        p.Start();
        p.OutputDataReceived += P_OutputDataReceived;
        p.BeginOutputReadLine();
        Process = p;
        return p;
    }

    private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        _log.AppendLine(e.Data);
    }
    public void Exit()
    {
        Call("exit");
        Process.WaitForExit();
        Process.Close();
        Process.Dispose();
        Process = null;
    }
    public void Call(string command)
    {
        CreateProcess();
        var sb = new StringBuilder();
        Process.StandardInput.WriteLine(command);

    }
    StringBuilder _log = new StringBuilder();

    public string CommadnLog
    {
        get
        {
            return _log.ToString();
        }
    }

    public static string Call(StringBuilder command)
    {
        //  string str = Console.ReadLine();


        // p.StartInfo.Arguments = " " + command;
        //  StringBuilder output = new StringBuilder();
        //  p.OutputDataReceived += (sender, e) => { output.AppendLine(e.Data); };
        var cmd = new Command();
        var p = cmd.CreateProcess();
        command.AppendLine("exit");
        p.StandardInput.WriteLine(command.ToString());
        var output = p.StandardOutput.ReadToEnd();
        // p.BeginOutputReadLine();

        //启动程序
        //获取cmd窗口的输出信息
        // string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        p.Close();
        p.Dispose();
        return output;
    }
}

