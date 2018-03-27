using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeadScoringEngine.FileLoading;

namespace LeadScoringEngine
{
    public class SalesLead : Serializable
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public decimal Score { get; set; }

        public override Serializable Deserialize(string fileLine)
        {
            throw new NotImplementedException();
        }
    }
}
