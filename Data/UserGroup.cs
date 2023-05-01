using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Data
{
    public class UserGroup
    {
        private string name;

        public UserGroup(string name)
        {
            this.name = name;
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<User> Users { get; set; }
        public List<Test> Tests { get; internal set; }
    }

}
