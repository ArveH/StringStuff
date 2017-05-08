using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDistance;

namespace StringStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Source: ");
                var source = Console.ReadLine();
                if (string.IsNullOrEmpty(source)) break;

                Console.Write("Target: ");
                var target = Console.ReadLine();
                if (string.IsNullOrEmpty(target)) break;

                Wikipedia.Levenshtein_Distance(source, target);
                Wikipedia.OSA_Distance(source, target);
                Wikipedia.DL_Distance(source, target);

                var distance = StackOverflow.Distance(source, target);
                Console.WriteLine("StackOverflow: {0}", distance);

                Console.WriteLine();
                
            }
        }
    }
}
