using Italbytz.Ports.Exam.Networks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Exercises.Extensions
{
    public static class MstvExtensions
    {
        public static int Count(this IMinimumSpanningTreeSolution solution)
        {
            int count = 0;
            foreach (var edge in solution.Edges)
            {
                count++;
            }
            return count;
        }

        public static ITaggedEdge<string, double> GetByIndex(this IMinimumSpanningTreeSolution solution, int index)
        {
            var values = solution.Edges.GetEnumerator();

            if (index == 0)
            {
                values.MoveNext();
                return values.Current;
            }
            else
            {
                var count = solution.Count();
                if (index <= count - 1 && index > 0)
                {
                    for (var i = 0; i <= index; i++)
                    {
                        values.MoveNext();
                    }
                    return values.Current;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
