using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Networking
{
    using System.Collections.Generic;

    namespace TestServer
    {
        public class Group
        {
            public string Name { get; set; }
            public List<User> Users { get; set; }
            public List<Test> Tests { get; set; }

            public Group()
            {
                Users = new List<User>();
                Tests = new List<Test>();
            }

            public Group(string name)
            {
                Name = name;
                Users = new List<User>();
                Tests = new List<Test>();
            }

        }
    }

}
