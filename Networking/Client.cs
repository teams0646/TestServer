using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using TestServer.Networking;

namespace TestServer
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public string ClientName { get; internal set; }

        private string ip;

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            stream = tcpClient.GetStream();

            BeginRead();
        }

        public Client(string name, string ip)
        {
            this.ClientName = name;
            this.ip = ip;
        }

        public void Send(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void BeginRead()
        {
            byte[] buffer = new byte[1024];

            stream.BeginRead(buffer, 0, buffer.Length, OnReadComplete, buffer);
        }

        private void OnReadComplete(IAsyncResult result)
        {
            int bytesRead = stream.EndRead(result);

            if (bytesRead > 0)
            {
                byte[] buffer = (byte[])result.AsyncState;
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                OnDataReceived(this, new Networking.DataReceivedEventArgs(this, data));

                BeginRead();
            }
            else
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();

            OnDisconnected(this, new ClientEventArgs(this));
        }

        public event EventHandler<Networking.DataReceivedEventArgs> DataReceived;

        protected virtual void OnDataReceived(object sender, Networking.DataReceivedEventArgs e)
        {
            EventHandler<Networking.DataReceivedEventArgs> handler = DataReceived;
            handler?.Invoke(sender, e);
        }

        public event EventHandler<ClientEventArgs> Disconnected;

        protected virtual void OnDisconnected(object sender, ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = Disconnected;
            handler?.Invoke(sender, e);
        }

        public string EndPoint
        {
            get { return tcpClient.Client.RemoteEndPoint.ToString(); }
        }

    }
}
