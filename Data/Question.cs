using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Choices { get; set; }
        public int CorrectChoiceIndex { get; set; }

        public Question(string text, List<string> choices, int correctChoiceIndex)
        {
            Text = text;
            Choices = choices;
            CorrectChoiceIndex = correctChoiceIndex;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Text);
            for (int i = 0; i < Choices.Count; i++)
            {
                sb.Append((char)('A' + i)).Append(". ").AppendLine(Choices[i]);
            }
            sb.AppendLine($"Correct answer: {(char)('A' + CorrectChoiceIndex)}");
            return sb.ToString();
        }
    }
}
