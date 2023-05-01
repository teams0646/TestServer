using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Data
{
    public class UserTest
    {
        private User user;
        private Test test;

        public UserTest(User user, Test test)
        {
            this.user = user;
            this.test = test;
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedOn { get; set; }
    }
}
