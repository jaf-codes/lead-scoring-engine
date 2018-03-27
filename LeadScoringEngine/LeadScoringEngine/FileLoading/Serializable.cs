using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine.FileLoading
{
    public abstract class Serializable
    {
        public abstract Serializable Deserialize(string fileLine);
    }
}
