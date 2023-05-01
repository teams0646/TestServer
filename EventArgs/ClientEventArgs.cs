using System;

namespace TestServer.Networking
{
    public class ClientEventArgs : EventArgs
    {
        public Client Client { get; }

        public ClientEventArgs(Client client)
        {
            Client = client;
        }
    }
}

