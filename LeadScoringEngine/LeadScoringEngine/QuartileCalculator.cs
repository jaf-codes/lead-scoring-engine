using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine
{
    public class Quartile
    {
        public int Quartile3 { get; set; }
        public int Quartile2 { get; set; }
        public int Quartile1 { get; set; }
    }

    public class QuartileCalculator
    {
        private int highest;
        private int lowest;
        private int average;
        private int spread;

        public QuartileCalculator(int highest, int lowest, int average)
        {
            this.highest = highest;
            this.lowest = lowest;
            this.average = average;
            spread = highest - lowest;
        }

        public int NormalizeScore(decimal rawScore)
        {
            return 0;
        }

        public Quartile CalculateQuartile()
        {

            return new Quartile()
            {
                Quartile3 = 0,
                Quartile2 = 0,
                Quartile1 = 0
            };
        }
    }
}
