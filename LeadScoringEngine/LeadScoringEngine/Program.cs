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
            FileLoader<SalesLead> loader = new FileLoader<SalesLead>(new ConsoleDisplayer());

            IEnumerable<SalesLead> salesLeads = loader.LoadFromFile(args[0], SalesLead.Deserialize);



            foreach (SalesLead salesLead in salesLeads)
            {

            }
        }
    }
}
