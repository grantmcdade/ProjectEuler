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

using variable = System.UInt64;

namespace ProjectEulerWorkbench.Problems
{
    class Problem023 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers."; }
        }

        public string Solve()
        {
            const variable limit = 28123;
            var primes = new List<variable>(Util.SieveOfAtkinInt64(limit));
            var abundantNumbers = new List<variable>();
            var excludeNumbers = new HashSet<variable>();
            variable runningTotal = 0;
            // Find all abundent numbers which, when added to it's previous number is less than 28123.
            for (variable i = 1; i <= limit; i++)
            {
                // Prime numbers are always deficient so we can eliminate those quickly
                if (!Util.IsPrime(i, primes))
                {
                    var factors = Util.GetFactors(i);
                    var sum = Util.Sum(factors, factors.Length - 1);
                    if (sum > i)
                    {
                        abundantNumbers.Add(i);
                        for (int j = 0; j < abundantNumbers.Count; j++)
                        {
                            // Calculate all the possible values which are the sum of two abundant numbers
                            // using the abundant numbers that we have found so far
                            excludeNumbers.Add(abundantNumbers[j] + i);
                        }
                    }
                }

                if (!excludeNumbers.Contains(i))
                    runningTotal += i;
            }

            return Convert.ToString(runningTotal);
        }
    }
}
