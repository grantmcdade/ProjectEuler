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
    class Problem051 : IProblem
    {
        public string Description
        {
            get { return "Find the smallest prime which, by changing the same part of the number, can form eight different primes."; }
        }

        public string Solve()
        {
            const int limit = 1000000;
            var primeList = Util.SieveOfAtkinInt64(limit);
            var primeSet = new HashSet<ulong>(primeList);
            var subset = new HashSet<ulong>();

            for (int i = 0; i < primeList.Length; i++)
            {
                if (primeList[i] < 10)
                    continue;

                var prime = primeList[i];
                var bitCount = Sets.DigitCount(prime);
                var maxPermutations = Util.Pow(2, bitCount);
                for (int j = 1; j < maxPermutations; j++)
                {
                    // Run through the replacement digits and modify the value according to the mask
                    subset.Clear();
                    var digits = Sets.ToDigits(prime);
                    for (int l = 0; l < 10; l++)
                    {
                        for (int k = 0; k < bitCount; k++)
                        {
                            var mask = 1 << k;
                            if ((j & mask) == mask)
                            {
                                digits[k] = l;
                            }
                        }

                        var value = Sets.ToNumber(digits);
                        if (primeSet.Contains(value) & digits[0] != 0)
                        {
                            // Fond a new member of the subset
                            subset.Add(value);
                        }
                    }
                    if (subset.Count >= 8)
                    {
                        // Found a subset with valid prime numbers
                        return Convert.ToString(subset.First());
                    }
                }
            }

            return String.Empty;
        }
    }
}
