using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeadScoringEngine.Display;
using LeadScoringEngine.FileLoading;

namespace LeadScoringEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITextDisplayer displayer = new ConsoleDisplayer();
            FileLoader<SalesLead> loader = new FileLoader<SalesLead>(displayer);

            IEnumerable<SalesLead> salesLeads = loader.LoadFromFile(args[0], SalesLead.Deserialize);

            foreach (LeadScore score in new LeadScorer().ScoreLeads(salesLeads))
            {
                displayer.Display(score.ToString());
            }
        }
    }
}
