using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ISDCompanion.Exercises.Baseclasses
{
    internal class PathSolution
    {
        public Dictionary<int, string[]> Values { get; set; }
        public string First { get { return "A"; } }
        public string Last { get; set; }

        public PathSolution(Dictionary<int, string[]> values)
        {
            Values = values;
            var hashMap = new Dictionary<string, int>();
            foreach (var value in values)
            {
                var pathValues = value.Value;


                if (hashMap.ContainsKey(pathValues[0]))
                {
                    hashMap[pathValues[0]]++;
                }
                else
                {
                    hashMap.Add(pathValues[0], 1);
                }
                if (hashMap.ContainsKey(pathValues[2]))
                {
                    hashMap[pathValues[2]]++;
                }
                else
                {
                    hashMap.Add(pathValues[2], 1);
                }
            }
            //source https://www.delftstack.com/de/howto/csharp/sort-dictionary-by-value-in-csharp/
            var sortedDict = from entry in hashMap orderby entry.Value ascending select entry.Key;
            int i = 0;
            foreach (var value in sortedDict)
            {
                if (i > 0)
                {
                    Last = value;
                    break;
                }
                i++;
            }

        }
    }
}
