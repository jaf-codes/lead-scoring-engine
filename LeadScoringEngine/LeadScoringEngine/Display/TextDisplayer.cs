using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine.Display
{
    public interface ITextDisplayer
    {
        void Display(string text);
    }

    public class NothingDisplayer : ITextDisplayer
    {
        public void Display(string text)
        {

        }
    }
}
