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
    class Problem043 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all pandigital numbers with an unusual sub-string divisibility property."; }
        }

        public string Solve()
        {
            int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var specialPandigitals = new List<int[]>();
            foreach (var set in Sets.GetLexicographicPermutations(digits))
            {
                if (((set[1] * 100 + set[2] * 10 + set[3]) % 2 == 0)
                    && ((set[2] * 100 + set[3] * 10 + set[4]) % 3 == 0)
                    && ((set[3] * 100 + set[4] * 10 + set[5]) % 5 == 0)
                    && ((set[4] * 100 + set[5] * 10 + set[6]) % 7 == 0)
                    && ((set[5] * 100 + set[6] * 10 + set[7]) % 11 == 0)
                    && ((set[6] * 100 + set[7] * 10 + set[8]) % 13 == 0)
                    && ((set[7] * 100 + set[8] * 10 + set[9]) % 17 == 0))
                {
                    int[] copy = new int[10];
                    set.CopyTo(copy, 0);
                    specialPandigitals.Add(copy);
                }                
            } 

            var specialPandigitalSet = new HashSet<ulong>();
            foreach (var item in specialPandigitals)
            {
                specialPandigitalSet.Add(Sets.ToNumber(item));
            }

            BigInteger sum = 0;
            foreach (var item in specialPandigitalSet)
            {
                sum += item;
            }

            return Convert.ToString(sum);
        }
    }
}
