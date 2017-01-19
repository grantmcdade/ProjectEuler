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
    class Problem078 : IProblem
    {
        public string Description
        {
            get { return "Investigating the number of ways in which coins can be separated into piles."; }
        }

        /// <summary>
        /// Solved using the Pentagonal number theorem (http://en.wikipedia.org/wiki/Pentagonal_number_theorem)
        /// </summary>
        /// <returns></returns>
        public string Solve()
        {
            var pt = new int[1000001]; // [1]
            pt[0] = 1;
            var n = 0;
            var answer = 1;

            while (answer % 1000000 != 0)
            {
                ++n;
                var r = 0;
                var f = -1;
                var i = 0;
                while (true)
                {
                    var k = generalised_pentagonal(i);
                    if (k > n)
                        break;
                    if ((i & 1) == 0)
                        f = -f;
                    r += f * pt[n - k];
                    ++i;
                }
                // We don't keep the whole value of r as it would get very large
                // I was generating it initially using BigInteger but this takes
                // much longer and is not necessary, so we just keep the remainder
                // of one million
                pt[n] = r % 1000000;
                answer = r;
            }

            return Convert.ToString(n);
        }

        private Func<int, int> pentagonal = x => x * (3 * x - 1) / 2;

        private int generalised_pentagonal(int n) // : # 0, 1, -1, 2, -2
        {
            if (n < 0)
                return 0;

            if ((n & 1) == 0)
                return pentagonal(n / 2 + 1);
            else
                return pentagonal(-(n / 2 + 1));
        }
        
        public string Solve_Bruteforce()
        {
            var cache = new ulong[10000, 10000];
            var input = 0UL;
            var answer = 0UL;
            do
            {
                ++input;
                answer = p(input, cache);
            } while (answer % 1000000 != 0);

            return Convert.ToString(input);
        }

        private ulong p(ulong n, ulong[,] cache)
        {
            return p(1, n, cache);
        }

        private ulong p(ulong k, ulong n, ulong[,] cache)
        {
            if (k > n)
                return 0;

            if (k == n)
                return 1;

            if (cache[k, n] != 0)
                return cache[k, n];

            var result = p(k + 1, n, cache) + p(k, n - k, cache);
            cache[k, n] = result;
            return result;
        }
    }
}
