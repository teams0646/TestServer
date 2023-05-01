using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class Test
    {
        public string Title { get; set; }

        public int TestId { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public string TestName { get; internal set; }
        public int TestDuration { get; internal set; }

        public Test(int testId,string title, string description, int testDuration)
        {
            Title = title;
            TestDuration = testDuration;
            TestId = testId;
            Description = description;
            Questions = new List<Question>();
        }

        public Test(string title, string description)
        {
            Title = title;
            Description = description;
            Questions = new List<Question>();
        }

        public Test()
        {
        }

        public void AddQuestion(string questionText, List<string> choices, int correctChoiceIndex)
        {
            Question question = new Question(questionText, choices, correctChoiceIndex);
            Questions.Add(question);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
