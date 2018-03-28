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
                decimal score = ScoreLead(lead);
                Scores[lead.Id].Score += score;
                RunningTotal += score;

                if (Highest == null || score > Highest)
                {
                    Highest = score;
                }
                else if (Lowest == null || score < Lowest)
                {
                    Lowest = score;
                }
            }

            foreach (LeadScore score in Scores.Values)
            {
                score.Quartile = FindQuartile(score.Score, Scores.Values.Select(sc => sc.Score).ToList());
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

        public string FindQuartile(decimal rawScore, List<decimal> elements)
        {
            decimal quartile2 = (elements.Max() + elements.Min()) / 2;
            decimal quartile3 = (elements.Max() + quartile2) / 2;
            decimal quartile1 = (quartile2 + elements.Min()) / 2;

            if (rawScore > quartile3)
            {
                return PLATINUM;
            }
            else if (rawScore > quartile2)
            {
                return GOLD;
            }
            else if (rawScore > quartile1)
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
