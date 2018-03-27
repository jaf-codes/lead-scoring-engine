using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeadScoringEngine.FileLoading;

namespace LeadScoringEngine
{
    public class SalesLead
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public decimal Score { get; set; }

        public static SalesLead Deserialize(string fileLine)
        {
            if (string.IsNullOrWhiteSpace(fileLine))
            {
                throw new FormatException(NO_DATA_MESSAGE);
            }
            if (!fileLine.Contains(","))
            {
                throw new FormatException(NO_DELIMETER_MESSAGE);
            }

            string trimmedLine = fileLine.Replace(" ", string.Empty);
            string[] parts = trimmedLine.Split(',');

            if (parts.Length != 3)
            {
                throw new FormatException(INSUFFICIENT_DATA_MESSAGE);
            }

            return new SalesLead()
            {
                Id = Convert.ToInt32(parts[0]),
                Event = parts[1].ToLower(),
                Score = Convert.ToDecimal(parts[2])
            };
        }

        public override bool Equals(object obj)
        {
            SalesLead other = obj as SalesLead;
            return (obj != null &&
                    this.Id == other.Id &&
                    this.Event == other.Event &&
                    this.Score == other.Score);
        }

        public override int GetHashCode()
        {
            //If hashtables have poor lookup time, this might need a better algorithm
            //There's a higher possibility of collision converting down from decimal
            return Convert.ToInt32((Id * 7) + Event.GetHashCode() + (Score * 13));
        }

        internal const string NO_DATA_MESSAGE = "Text contained no data";
        internal const string NO_DELIMETER_MESSAGE = "Text did not contain the following necessary delimeter: " + delimiter;
        internal const string INSUFFICIENT_DATA_MESSAGE = "Text did not contain the correct number of elements: " + elements;

        private const string delimiter = ",";
        private const string elements = "3";
    }
}
