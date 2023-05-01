using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TestServer.Networking;
using TestServer.Networking.TestServer;

namespace TestServer
{
    public class Server
    {
        private List<User> users = new List<User>();
        private List<Test> tests = new List<Test>();
        private List<Client> clients = new List<Client>();
        private TcpListener tcpListener;

        public Server(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Server started.");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Client client = new Client(tcpClient);
                clients.Add(client);

                client.DataReceived += OnDataReceived;
                client.Disconnected += OnClientDisconnected;

                Console.WriteLine($"Client connected: {client.EndPoint}");

                OnClientConnected(this, new ClientEventArgs(client));
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
            Console.WriteLine("Server stopped.");
        }

        public User AuthenticateUser(string username, string password)
        {
            User user = users.Find(u => u.Username == username && u.Password == password);
            return user;
        }

        public void CreateUser(string username)
        {
            User user = new User(username,"password", new Group("guest"));
            users.Add(user);
        }

        public void CreateGroup(string groupName)
        {
            Group group = new Group { Name = groupName, Users = new List<User>() };
        }

        public void AddUserToGroup(User user, Group group)
        {
            group.Users.Add(user);
            user.Group = group;
        }

        public void UploadTest(Test test)
        {
            tests.Add(test);
        }

        public void AssignTest(Test test, Group group)
        {
            group.Tests.Add(test);
        }

        public void AssignTest(Test test, User user)
        {
            user.Tests.Add(test);
        }

        private void OnDataReceived(object sender, Networking.DataReceivedEventArgs e)
        {
            Console.WriteLine(value: $"Data received from {e.Client.EndPoint}: {e.Data}");
        }

        private void OnClientDisconnected(object sender, ClientEventArgs e)
        {
            clients.Remove(e.Client);

            Console.WriteLine($"Client disconnected: {e.Client.EndPoint}");

            OnClientDisconnected(this, e);
        }

        public event EventHandler<ClientEventArgs> ClientConnected;

        protected virtual void OnClientConnected(object sender, ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = ClientConnected;
            handler?.Invoke(sender, e);
        }

        

       
    }
}
