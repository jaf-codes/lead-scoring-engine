using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeadScoringEngine.Display;

namespace LeadScoringEngine.FileLoading
{
    public interface IFileLoader<T>
    {
        IEnumerable<T> LoadFromFile(string fileName);
    }

    public class FileLoader<T>
    {
        private ITextDisplayer displayer;

        public FileLoader(ITextDisplayer displayer)
        {
            this.displayer = displayer ?? new NothingDisplayer();
        }

        public IEnumerable<T> LoadFromFile(string fileName, Func<string, T> Deserialize)
        {
            List<T> collection = new List<T>();
            try
            {
                foreach (string line in File.ReadLines(fileName))
                {
                    collection.Add(Deserialize(line));
                }
            }
            catch (Exception exception)
            {
                displayer.Display(exception.Message);
            }
            
            return collection;
        }
    }
}
