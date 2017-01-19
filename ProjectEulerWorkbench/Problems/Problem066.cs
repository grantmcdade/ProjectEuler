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
using System.Numerics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem066 : IProblem
    {
        public string Description
        {
            get { return "Find the value of D <= 1000 in minimal solutions of x for which the largest value of x is obtained."; }
        }

        public string Solve()
        {
            // The Diophantine equation is as follows:
            // x^2 - D * y^2 = 1

            // This can be solved using the Chakravala method, but it's harder than it looks :)
            // Chakravala(61);

            // Instead we use continued fractions to solve it
            BigInteger maxX = 0;
            var maxD = 0;
            for (int i = 2; i <= 1000; i++)
            {
                var series = ContinuedFractions.GetContinuedFractions(i);
                var convergent = 0;
                BigInteger result = 0;
                Tuple<BigInteger, BigInteger> answer = null;
                do
                {
                    ++convergent;
                    answer = ContinuedFractions.ResolveContinuedFraction(series, convergent);
                    if (answer == null)
                        break;
                    result = (answer.Item1 * answer.Item1) - (i * answer.Item2 * answer.Item2);
                } while (result != 1);

                if (answer != null && answer.Item1 > maxX)
                {
                    maxX = answer.Item1;
                    maxD = i;
                }
            }

            return Convert.ToString(maxD);
        }

        private Tuple<int, int> Chakravala(int N)
        {
            // Find k by letting b = 1 and letting a = the nearest square to N
            var sq = (int)Math.Ceiling(Math.Sqrt(N));
            var a = sq;
            var k = a * a - N;

            // Select m such that (a + bm)/k is an integer and m^2 - N is minimised
            var r = a % k;
            var m = a - r + 1;

            // Transpose the first triple
            var answer = new Triple(a, 1, k);
            do
            {
                answer = Compose(answer, m, N);

                // Calculate a new value for m, keep in mind that b is no longer 1
                m = sq;
                var minK = Int32.MaxValue;
                var bestM = m;
                while ((answer.a + (answer.b * m)) % answer.k != 0)
                {
                    --m;
                    var newK = Math.Abs(m * m - N);
                    if (newK < minK)
                    {
                        minK = newK;
                        bestM = m;
                    }

                    if (m <= 0)
                        break;
                }
                m = bestM;

            } while (answer.k != 1);


            return new Tuple<int, int>(0, 0);
        }

        private Triple Compose(Triple t, int m, int N)
        {
            // return new Triple() { a = (t.a * m + N * t.b) / t.k, b = (t.a + t.b * m) / t.k, k = (m * m - N) / t.k };

            var a = (t.a * m + N * t.b) / t.k;
            var b = (t.a + t.b * m) / t.k;
            var k = (m * m - N) / t.k;

            return new Triple(a, b, k);
        }

        private class Triple
        {
            public int a { get; set; }
            public int b { get; set; }
            public int k { get; set; }

            public Triple()
            {
            }

            public Triple(int a, int b, int k)
            {
                this.a = a;
                this.b = b;
                this.k = k;
            }
        }
    }
}
