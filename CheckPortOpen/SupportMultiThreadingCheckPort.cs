using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPortOpen
{
    class SupportMultiThreadingCheckPort
    {
        private List<int> OpenedPortList { get; set; }
        private int ProcessingPort { get; set; }
        private IPAddress HostAddress { get;set; }
        private int Start { get; set; }
        private int Stop { get; set; }

        public List<int> CheckPort(string hostAddress, int startPort, int stopPort, int numThread)
        {
            this.Start = startPort;
            this.Stop = stopPort;
            this.HostAddress= Dns.GetHostAddresses(hostAddress)[0];
            bool x=ThreadPool.SetMinThreads(400, 400);
            Console.WriteLine(x);
            var taskList = new List<Task>();

            for (var thread = 0; thread < numThread; thread++)
            {
                var task = new Task(CheckOpenPortByConnect);
                task.Start();
                taskList.Add(task);
            }
            Thread.Sleep(1000);
            while (taskList.Count !=0)
            {
                foreach (var t in taskList)
                {
                    if (!t.IsCompleted) continue;
                    taskList.Remove(t);
                    break;
                }
            }
            return OpenedPortList;
        }
       

        public void CheckOpenPortByConnect()
        {
            var port=ProcessingPort++;
            while (port <= this.Stop)
            { 

                using (TcpClient tcpClient = new TcpClient(AddressFamily.InterNetwork))
                try
                {
                        tcpClient.Connect(HostAddress,port);
                        
                        OpenedPortList.Add(port);
                        System.Diagnostics.Debug.WriteLine("Open at" + port);

                }
                catch
                {
                    //System.Diagnostics.Debug.WriteLine("Close at" + port);
                }
                finally
                    {
                        tcpClient.Close();
                    }
                port = ProcessingPort++;

            }
            

        }
        public SupportMultiThreadingCheckPort()
        {
            OpenedPortList = new List<int>();
            ProcessingPort = 0;
        }
        public void CheckOpenPortByBeginConnect()
        {
            var port = ProcessingPort++;
            try
            {
                var timeout = 5000;
                var newClient = new TcpClient();


                var state = new IsTcpPortOpen
                {
                    MainClient = newClient,
                    tcpOpen = true
                };
                IAsyncResult ar = newClient.BeginConnect("trungtvq.ddns.net", port, AsyncCallback, state);


                if (ar.AsyncWaitHandle.WaitOne(timeout, false) == false || newClient.Connected == false)
                {
                    //throw new Exception();
                    return;
                }
                OpenedPortList.Add(port);
                //System.Diagnostics.Debug.WriteLine("Open at" + port);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Close at" + port);
            }
        }

        private class IsTcpPortOpen
        {
            public TcpClient MainClient { get; set; }
            public bool tcpOpen { get; set; }
        }

        void AsyncCallback(IAsyncResult asyncResult)
        {
            var state = (IsTcpPortOpen) asyncResult.AsyncState;
            TcpClient client = state.MainClient;

            try
            {
                client.EndConnect(asyncResult);
            }
            catch
            {
                return;
            }

            if (client.Connected && state.tcpOpen)
            {
                return;
            }

            client.Close();
        }


        public TcpClient Connect(string hostName, int port, int timeout)
        {
            var newClient = new TcpClient();

            var state = new IsTcpPortOpen
            {
                MainClient = newClient,
                tcpOpen = true
            };

            IAsyncResult ar = newClient.BeginConnect(hostName, port, AsyncCallback, state);
            state.tcpOpen = ar.AsyncWaitHandle.WaitOne(timeout, false);

            if (state.tcpOpen == false || newClient.Connected == false)
            {
                System.Diagnostics.Debug.WriteLine("tao nem");
                throw new Exception();
            }

            return newClient;
        }
        
        
    }
}

