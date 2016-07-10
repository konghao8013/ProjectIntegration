using LibExtend.NetworkServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;

namespace NetBuilderServer
{
    public class MonitorPort
    {

        int Port { set; get; }
        HttpServer _Server;
        public MonitorPort(int port)
        {
            Port = port;

        }
        public void Start()
        {
            var address = IPAddress.Parse("0.0.0.0");
            _Server = new HttpServer(address, Port);
            var thread = new Thread(() =>
            {
                _Server.listen();
            });
            thread.Start();
            _Server.HttpGetEvent += _Server_HttpGetEvent;
            _Server.HttpPostEvent += _Server_HttpPostEvent;

        }

        private void _Server_HttpPostEvent(HttpProcessor p, string data)
        {
            Invoke(p, data);
            // throw new NotImplementedException();
        }


        private void _Server_HttpGetEvent(HttpProcessor p)
        {
            Invoke(p);
            //Type.GetType()
            //  p.outputStream.WriteLine("你好这是测试");
            // throw new NotImplementedException();
        }
        void Invoke(HttpProcessor p, string data = null)
        {
            var result = "";
           
            switch (p.AbsolutePath)
            {
                case "builder":
                    var ppN = p.Params["name"];
                    if (!string.IsNullOrEmpty(ppN))
                    {
                        result = CodeOperation.Builder(ppN) + "";
                    }
                    break;
                case "codeupdate":

                    break;
                case "getlogentity":
                    var path = p.Params["path"];
                    if (!string.IsNullOrEmpty(path))
                    {
                        result = CodeOperation.GetContent(path);
                        p.writeDownload();
                        p.outputStream.Write(result.ToArray());
                        return;
                    }
                    break;
                case "removeproject":
                    var pn = p.Params["name"];
                    if (!string.IsNullOrEmpty(pn))
                    {
                        CodeOperation.RemoveProject(pn);
                        result = "true";
                    }
                    break;
                case "projectsetting":

                    if (!string.IsNullOrEmpty(data))
                    {
                        result = CodeOperation.SetProject(data) + "";
                    }
                    break;
                case "getlog":
                    var projecName = p.Params["name"];
                    var take = 100;
                    if (!string.IsNullOrEmpty(projecName))
                    {

                        if (!Int32.TryParse(p.Params["take"], out take))
                        {
                            take = 100;
                        }

                        result = CodeOperation.GetLogByProject(projecName, take);

                    }
                    break;
                case "getprojects":
                    result = CodeOperation.GetProjectList();
                    break;
                default:
                    p.outputStream.WriteLine("返回测试界面" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    break;

            }
            if (string.IsNullOrEmpty(result))
            {
                result = "".Serialize();
            }
            p.writeSuccess();
            p.outputStream.WriteLine(result);

            //p.Params[""]
            // var request = WebRequest.Create(p.http_url);
            //  request.pa

            // var request=new HttpRequest("", p.http_url, "");

        }


        public void Stop()
        {
            if (_Server != null)
            {
                _Server.Stop();
            }
        }

    }
}
