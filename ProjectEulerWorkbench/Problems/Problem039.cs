/*
    Grant McDade
    Copyright(C) 2017 Grant McDade

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Triple = System.Tuple<int, int, int>;
using TripleList = System.Collections.Generic.List<System.Tuple<int, int, int>>;
using TripleDictionary = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Tuple<int, int, int>>>;

namespace ProjectEulerWorkbench.Problems
{
    class Problem039 : IProblem
    {
        public string Description
        {
            get { return "If p is the perimeter of a right angle triangle, {a, b, c}, which value, for p ≤ 1000, has the most solutions?"; }
        }

        public string Solve()
        {
            return SolveUsingGrantMethod();
        }

        private string SolveUsingGrantMethod()
        {
            double c;
            const int length = 500;
            var triples = new TripleList();
            for (int a = 1; a <= length; a++)
            {
                for (int b = 1; b <= length; b++)
                {
                    c = Math.Sqrt(a * a + b * b);
                    if (c % 1d == 0d)
                    {
                        var t = new Triple(a, b, (int)c);
                        if (Perimeter(t) <= 1000)
                            triples.Add(t);
                    }
                }
            }

            var triplesByPerimeter = new TripleDictionary();
            foreach (var triple in triples)
            {
                var perimeter = Perimeter(triple);
                if (!triplesByPerimeter.ContainsKey(perimeter))
                    triplesByPerimeter.Add(perimeter, new TripleList());
                triplesByPerimeter[perimeter].Add(triple);
            }

            int perimeterWithMostSets = 0;
            int maxSets = 0;
            foreach (var item in triplesByPerimeter)
            {
                if (item.Value.Count > maxSets)
                {
                    maxSets = item.Value.Count;
                    perimeterWithMostSets = item.Key;
                }
            }

            return Convert.ToString(perimeterWithMostSets);
        }



        private string SolveUsingSquares()
        {
            var result = new TripleDictionary();
            int perimeter = 0;
            int b = 0;
            do
            {
                ++b;
                var triples = CalculateTriplesUsingSquares(b);
                foreach (var triple in triples)
                {
                    perimeter = Perimeter(triple);

                    if (perimeter <= 1000)
                    {
                        if (!result.ContainsKey(perimeter))
                            result.Add(perimeter, new TripleList());
                        result[perimeter].Add(triple);
                    }
                }
            } while (b <= 1000);

            int currentMax = 0;
            foreach (var item in result)
            {
                if (item.Value.Count > currentMax)
                {
                    currentMax = item.Value.Count;
                    perimeter = item.Key;
                }
            }

            return Convert.ToString(perimeter);
        }

        private Triple[] CalculateTriplesUsingSquares(int b)
        {
            var result = new TripleList();
            bool doubled = (b % 2) != 0;
            if (doubled)
                b *= 2;

            var sqrt = Math.Sqrt(b);
            for (int i = 1; i < sqrt; i++)
            {
                if (b % i == 0)
                {
                    int m = b / i;
                    int n = i;

                    if (doubled)
                        result.Add(new Triple((m * m - n * n) / 2, b / 2, (m * m + n * n) / 2));
                    else
                        result.Add(new Triple(m * m - n * n, b, m * m + n * n));
                }
            }

            return result.ToArray();
        }

        private int Perimeter(Triple value)
        {
            return value.Item1 + value.Item2 + value.Item3;
        }
    }
}
