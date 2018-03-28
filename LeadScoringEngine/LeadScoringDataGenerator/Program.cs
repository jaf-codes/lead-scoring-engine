using LeadScoringEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadScoringDataGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Number of leads: ");
            int numberOfContacts = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Random random = new Random();

            List<SalesLead> leads = new List<SalesLead>();

            while (leads.Count < numberOfContacts)
            {
                leads.Add(new SalesLead()
                {
                    Id = random.Next(1, numberOfContacts),
                    Event = GetRandomEvent(random.Next(3)),
                    Score = (decimal)random.Next(9999) / 1000
                });
            }
            
            using (StreamWriter writer = new StreamWriter(File.Create("test.csv")))
            {
                foreach (SalesLead lead in leads)
                {
                    writer.WriteLine(lead.ToString());
                }
            }
        }

        private static string GetRandomEvent(int number)
        {
            switch (number)
            {
                case 0:
                default:
                    return SalesLead.WEB;
                case 1:
                    return SalesLead.EMAIL;
                case 2:
                    return SalesLead.SOCIAL;
                case 3:
                    return SalesLead.WEBINAR;
            }
        }
    }
}
