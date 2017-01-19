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

namespace ProjectEulerWorkbench.Problems
{
    class Problem077 : IProblem
    {
        public string Description
        {
            get { return "What is the first value which can be written as the sum of primes in over five thousand different ways?"; }
        }

        public string Solve()
        {
            ulong ways = 0;
            ulong number = 9;
            do
            {
                ++number;
                ways = Ways(number);
            } while (ways <= 5000);

            return Convert.ToString(number);
        }

        private ulong Ways(ulong target)
        {
            ulong[] primes = Util.SieveOfAtkinInt64(target);
            var ways = new ulong[target + 1];
            ways[0] = 1;
            for (ulong i = 0; i < (ulong)primes.Length; i++)
            {
                for (ulong j = primes[i]; j <= target; j++)
                {
                    ways[j] += ways[j - primes[i]];
                }
            }
            return ways[target];
        }
    }
}
