using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine
{
    public class Quartile
    {
        public decimal Quartile3 { get; set; }
        public decimal Quartile2 { get; set; }
        public decimal Quartile1 { get; set; }
    }

    public class PercentileCalculator
    {
        private List<decimal> elements;

        public PercentileCalculator(List<decimal> sortedElements)
        {
            this.elements = sortedElements;
        }

        public int CalculatePercentile(decimal rawScore)
        {
            if (rawScore == elements.Last())
            {
                return 100;
            }
            if (rawScore == elements.First())
            {
                return 0;
            }
            float index = elements.IndexOf(rawScore);
            return (int)Math.Floor((index / elements.Count) * 100f);
        }
    }
}
