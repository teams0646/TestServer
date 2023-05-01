using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer.Data;
using TestServer.Networking.TestServer;

namespace TestServer
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Group Group { get; set; }
        public List<Test> Tests { get; internal set; }
        public List<UserTest> UserTests { get; internal set; }
        public int Id { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Email { get; internal set; }

        public User(string username, string password, Group group)
        {
            Username = username;
            Password = password;
            Group = group;
            Tests = new List<Test>();
            UserTests = new List<UserTest>();
            Id = 0;

        }

        public User()
        {
        }
    }

}
