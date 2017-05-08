using System;

namespace StringDistance
{
    /// <summary>
    /// The algorithms found here:
    ///   https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
    /// Rough translation of the pseudo code given to c#, without any concerns
    /// for nice code, or performance
    /// 
    /// It will print the matrix used to the Console
    /// </summary>
    public static class Wikipedia
    {
        public static int Levenshtein_Distance(string a, string b)
        {
            var dxSize = a.Length + 1;
            var dySize = b.Length + 1;
            var d = new int[dxSize, dySize];
            for (var dx = 0; dx < dxSize; dx++) d[dx, 0] = dx;
            for (var dy = 0; dy < dySize; dy++) d[0, dy] = dy;

            for (int dx = 1; dx < dxSize; dx++)
            {
                var aPos = dx - 1;
                for (int dy = 1; dy <= b.Length; dy++)
                {
                    var bPos = dy - 1;
                    var cost = a[aPos] == b[bPos] ? 0 : 1;
                    d[dx, dy] = Min(
                        d[dx - 1, dy] + 1, // deletion
                        d[dx, dy - 1] + 1, // insertion
                        d[dx - 1, dy - 1] + cost); // substitution
                }
            }

            var distance = d[dxSize - 1, dySize - 1];
            Console.WriteLine("Levenshtein_Distance: {0}", distance);
            Print(d, dxSize, dySize, a, b);
            return distance;
        }

        // Optimal string alignment distance
        public static int OSA_Distance(string a, string b)
        {
            var dxSize = a.Length + 1;
            var dySize = b.Length + 1;
            var d = new int[dxSize, dySize];
            for (var dx = 0; dx < dxSize; dx++) d[dx, 0] = dx;
            for (var dy = 0; dy < dySize; dy++) d[0, dy] = dy;

            for (int dx = 1; dx <= a.Length; dx++)
            {
                var aPos = dx - 1;
                for (int dy = 1; dy <= b.Length; dy++)
                {
                    var bPos = dy - 1;
                    var cost = a[aPos] == b[bPos] ? 0 : 1;
                    d[dx, dy] = Min(
                        d[dx - 1, dy] + 1, // deletion
                        d[dx, dy - 1] + 1, // insertion
                        d[dx - 1, dy - 1] + cost); // substitution

                    if (dx > 1 && 
                        dy > 1 && 
                        a[aPos] == b[bPos - 1] && 
                        a[aPos - 1] == b[bPos])
                    {
                        d[dx, dy] = Math.Min(
                            d[dx, dy],
                            d[dx - 2, dy - 2] + cost); // transposition
                    }
                }
            }

            var distance = d[dxSize - 1, dySize - 1];
            Console.WriteLine("OSA_Distance: {0}", distance);
            Print(d, dxSize, dySize, a, b);
            return distance;
        }

        // Damerau–Levenshtein distance with adjacent transpositions
        public static int DL_Distance(string a, string b)
        {
            var dxSize = a.Length + 2;
            var dySize = b.Length + 2;
            var da = new int[256];
            for (int i = 0; i < 256; i++)
                da[i] = 1;
            var d = new int[dxSize, dySize];

            var maxdist = a.Length + b.Length;
            for (var dx = 0; dx < dxSize; dx++)
            {
                d[dx, 0] = maxdist;
                if (dx < dxSize - 1) d[dx + 1, 1] = dx;
            }
            for (var dy = 0; dy < dySize; dy++)
            {
                d[0, dy] = maxdist;
                if (dy < dySize - 1) d[1, dy + 1] = dy;
            }

            for (int dx = 2; dx < dxSize; dx++)
            {
                var aPos = dx - 2;
                var db = 1;
                for (int dy = 2; dy < dySize; dy++)
                {
                    var bPos = dy - 2;
                    var k = da[b[bPos]];
                    var l = db;
                    int cost;
                    if (a[aPos] == b[bPos])
                    {
                        cost = 0;
                        db = dy;
                    }
                    else
                    {
                        cost = 1;
                    }

                    d[dx, dy] = Min(
                        d[dx - 1, dy -1 ] + cost, // substitution
                        d[dx, dy - 1] + 1, // insertion
                        d[dx - 1, dy] + 1, // deletion
                        d[k - 1, l - 1] + (dx - k - 1) + 1 + (dy - l - 1)); // transposition
                }
                da[a[aPos]] = dx;
            }

            var distance = d[dxSize - 1, dySize - 1];
            Console.WriteLine("DL_Distance: {0}", distance);
            Print(d, dxSize, dySize, a, b);
            return distance;
        }

        public static void Print(int[,] d, int dxSize, int dySize, string source, string target)
        {
            var padding = dxSize - source.Length;

            Console.WriteLine(new string('-', dxSize * 3 + 3));
            PrintEmpty(padding+1);
            foreach (var c in source)
                Console.Write($" {c}|");
            Console.WriteLine();

            for (int dy = 0; dy < dySize; dy++)
            {
                Console.WriteLine(new string('-', dxSize*3+3));
                if (dy < padding) PrintEmpty(1);
                if (dy >= padding && dy < dySize) Console.Write($"|{target[dy-padding]}|");
                for (int dx = 0; dx < dxSize; dx++)
                {
                    Console.Write($"{d[dx, dy]:D2}|");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', dxSize * 3 + 3));
            Console.WriteLine();
        }

        private static void PrintEmpty(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write("  |");
            }
        }

        public static int Min(int i1, int i2, int i3)
        {
            return Math.Min(
                i1,
                Math.Min(i2, i3));
        }

        public static int Min(int i1, int i2, int i3, int i4)
        {
            return Math.Min(
                Min(i1, i2, i3),
                i4);
        }
    }
}