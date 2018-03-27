using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadScoringEngine.Display;

namespace LeadScoringEngine.Test.MockObjects
{
    public class TestTextDisplayer : ITextDisplayer
    {
        private List<string> messages;
        public IEnumerable<string> Messages { get { return messages; } }

        public TestTextDisplayer()
        {
            messages = new List<string>();
        }
        public void Display(string text)
        {
            messages.Add(text);
        }
    }

    public class InternalApplicationException : Exception
    {
        public InternalApplicationException()
            : base() { }

        public InternalApplicationException(string message)
            : base(message) { }
    }
}
