using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID
{
    public class SingleResponsibilityPrinciple
    {
        public class Journal
        {
            private readonly List<string> _entries = new List<string>();
            private static int _count = 0;

            public int AddNew(string entry)
            {
                _entries.Add($"{++_count}: " + entry);
                return _count;
            }

            public void RemoveEntry(int id)
            {
                _entries.RemoveAt(id);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, _entries);
            }

        }

        public class Persistence
        {
            public void Save(Journal journal, string fileName)
            {
                File.WriteAllText(fileName, journal.ToString());
            }

            public Journal Load(string file)
            {
                throw new NotImplementedException();
            }

            public Journal Load(Uri uri)
            {
                throw new NotImplementedException();
            }
        }
    }


}
