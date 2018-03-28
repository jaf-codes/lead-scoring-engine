using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringEngine
{
    public class LeadScore
    {
        public int Id { get; set; }
        public string Quartile { get; set; }
        public decimal Score { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Id, Quartile, Score);
        }        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }        public override int GetHashCode()
        {
            return Id;
        }
    }

    public class LeadScorer
    {
        internal Dictionary<int, LeadScore> Scores { get; private set; }
        internal decimal RunningTotal { get; private set; }
        internal decimal? Highest { get; private set; }
        internal decimal? Lowest { get; private set; }

        public LeadScorer()
        {
            Scores = new Dictionary<int, LeadScore>();
            RunningTotal = 0;
        }

        public IEnumerable<LeadScore> ScoreLeads(IEnumerable<SalesLead> leads)
        {
            foreach (SalesLead lead in leads)
            {
                decimal score = ScoreLead(lead); //This creates an entry if there isn't one
                Scores[lead.Id].Score += score; //Please don't eliminate a double from the stack
            }

            PercentileCalculator calculator = new PercentileCalculator(Scores.Values.Select(sc => sc.Score).ToList());

            foreach (LeadScore score in Scores.Values)
            {
                int normalizedScore = calculator.CalculatePercentile(score.Score);
                score.Score = normalizedScore;
                score.Quartile = FindQuartile(normalizedScore);
            }

            return Scores.Values;
        }

        public decimal ScoreLead(SalesLead lead)
        {
            if (!Scores.ContainsKey(lead.Id))
            {
                Scores[lead.Id] = new LeadScore() { Id = lead.Id, Score = 0 };
            }

            return lead.Score * FindMultiplier(lead.Event);            
        }

        public decimal FindMultiplier(string eventName)
        {
            switch (eventName.ToLower())
            {
                case SalesLead.WEB:
                    return 1.0m;
                case SalesLead.EMAIL:
                    return 1.2m;
                case SalesLead.SOCIAL:
                    return 1.5m;
                case SalesLead.WEBINAR:
                    return 2.0m;
                default:
                    return 0m;
            }
        }

        public string FindQuartile(int percentile)
        {
            if (percentile > 75)
            {
                return PLATINUM;
            }
            else if (percentile > 50)
            {
                return GOLD;
            }
            else if (percentile > 25)
            {
                return SILVER;
            }
            else
            {
                return BRONZE;
            }
        }

        internal const string PLATINUM = "platinum";
        internal const string GOLD = "gold";
        internal const string SILVER = "silver";
        internal const string BRONZE = "bronze";
    }
}
