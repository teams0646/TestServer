using System;

namespace TestServer.Networking
{
    public class DataReceivedEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public string Data { get; private set; }

        public DataReceivedEventArgs(Client client, string data)
        {
            Client = client;
            Data = data;
        }
    }
}

