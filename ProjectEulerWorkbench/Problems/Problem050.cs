﻿/*
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
    class Problem050 : IProblem
    {
        public string Description
        {
            get { return "Which prime, below one-million, can be written as the sum of the most consecutive primes?"; }
        }

        public string Solve()
        {
            const int limit = 1000000;
            var primeList = Util.SieveOfAtkinInt64(limit);
            var primes = new HashSet<ulong>(primeList);

            ulong largestPrime = 0;
            ulong mostTerms = 0;
            for (int i = 0; i < primeList.Length; i++)
            {
                ulong sum = 0;
                ulong terms = 0;
                for (int j = i; j < primeList.Length; j++)
                {
                    ++terms;
                    sum += primeList[j];
                    if (sum > limit)
                        break;

                    if (terms > mostTerms && primes.Contains(sum) && j != i)
                    {
                        largestPrime = sum;
                        mostTerms = terms;
                    }
                }
            }
            return Convert.ToString(largestPrime);
        }
    }
}
