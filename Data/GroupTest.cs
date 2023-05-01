using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Data
{
    public class GroupTest
    {
        public int GroupTestId { get; set; }
        public int TestId { get; set; }
        public int GroupId { get; set; }
        public DateTime AssignedOn { get; set; }

        public Test Test { get; set; }
        public UserGroup UserGroup { get; set; }
    }

}
