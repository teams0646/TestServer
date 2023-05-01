using System;
using TestServer.Data;

namespace TestServer.Networking
{
    public class TestEventArgs : EventArgs
    {
        public Test Test { get; set; }
        public GroupTest GroupTest { get; internal set; }
        public int UserId { get; internal set; }
        public int TestId { get; internal set; }

        public TestEventArgs(Test test)
        {
            Test = test;
        }
    }
}

