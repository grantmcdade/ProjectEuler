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
    class Problem041 : IProblem
    {
        public string Description
        {
            get { return "What is the largest n-digit pandigital prime that exists?"; }
        }

        public string Solve()
        {
            var largestPrime = 0UL;
            for (int i = 4; i < 10; i++)
            {
                int[] digits = new int[i];
                for (int j = 0; j < i; j++)
                    digits[j] = j + 1;

                foreach (var set in Sets.GetLexicographicPermutations(digits))
                {
                    var value = Sets.ToNumber(set);
                    if (Util.IsPrime(value) && value > largestPrime)
                        largestPrime = value;
                }
            }

            return Convert.ToString(largestPrime);
        }

    }
}
