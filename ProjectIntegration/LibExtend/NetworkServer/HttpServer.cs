using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LibExtend.NetworkServer
{
    public class HttpServer
    {

        int Port { set; get; }
        IPAddress IP { set; get; }
        TcpListener listener;
        bool is_active = true;

        /// <summary>
        /// 
        /// </summary>
        public event Action<HttpProcessor> HttpGetEvent;
        /// <summary>
        /// 
        /// </summary>
        public event Action<HttpProcessor, string> HttpPostEvent;

        public HttpServer(IPAddress ip, int port)
        {
            Port = port;
            IP = ip;
        }

        public void listen()
        {
            listener = new TcpListener(IP, Port);
            listener.Start();
            while (is_active)
            {
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        processor.process();
                    }
                    catch (Exception e)
                    {

                        Debug.WriteLine(e.Message);
                    }
                });
                thread.Start();
                Thread.Sleep(1);
            }
        }
        public void Stop()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }

        public void handleGETRequest(HttpProcessor p)
        {
           
            if (HttpGetEvent != null)
            {
                HttpGetEvent(p);
            }

        }
        public void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
           
            if (HttpPostEvent != null)
            {
                var data = inputData.ReadLine();
                HttpPostEvent(p, data);

            }

        }

    }
}
