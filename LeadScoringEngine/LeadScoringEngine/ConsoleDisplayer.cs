using LeadScoringEngine.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine
{
    public class ConsoleDisplayer : ITextDisplayer
    {
        public void Display(string text)
        {
            Console.WriteLine(text);
        }
    }
}
