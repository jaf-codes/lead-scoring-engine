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

            List<LeadScore> sortedScores = Scores.Values.OrderBy(f => f.Score).ToList();
            int numberOfScores = sortedScores.Count;

            int quartile1Order = Convert.ToInt32(Math.Floor(.75m * numberOfScores));
            decimal quartile1 = sortedScores[quartile1Order].Score;

            int quartile2Order = Convert.ToInt32(.50m * numberOfScores);
            decimal quartile2 = sortedScores[quartile2Order].Score;

            int quartile3Order = Convert.ToInt32(.25m * numberOfScores);
            decimal quartile3 = sortedScores[quartile3Order].Score;

            Quartile quartile = new Quartile()
            {
                Quartile3 = quartile1,
                Quartile2 = quartile2,
                Quartile1 = quartile3
            };

            foreach (LeadScore score in Scores.Values)
            {
                int normalizedScore = Convert.ToInt32((score.Score / (Highest - Lowest)) * 100);
                score.Score = normalizedScore;
                score.Quartile = FindQuartile(normalizedScore, quartile);
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

        public string FindQuartile(int normalizedScore, Quartile quartile)
        {
            if (normalizedScore > quartile.Quartile3)
            {
                return PLATINUM;
            }
            else if (normalizedScore > quartile.Quartile2)
            {
                return GOLD;
            }
            else if (normalizedScore > quartile.Quartile1)
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
