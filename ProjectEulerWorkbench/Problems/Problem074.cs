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
    class Problem074 : IProblem
    {
        public string Description
        {
            get { return "Determine the number of factorial chains that contain exactly sixty non-repeating terms."; }
        }

        private int[] digitFactorials = new int[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

        public string Solve()
        {
            const int limit = 1000000;
            const int cacheSize = 3 * limit;

            var cache = new int[cacheSize];
            var sums = new HashSet<int>();
            var counter = 0;
            var totalCounter = 0;
            for (int i = 69; i < limit; i++)
            {
                var sum = SumOfDigitFactorials(i);
                counter = 1;
                sums.Clear();
                sums.Add(i);
                while (!sums.Contains(sum))
                {
                    if (sum < cacheSize && cache[sum] != 0)
                    {
                        counter += cache[sum];
                        break;
                    }
                    else
                    {
                        ++counter;
                        sums.Add(sum);
                        sum = SumOfDigitFactorials(sum);
                    }
                }
                if (counter == 60)
                {
                    ++totalCounter;
                }
                cache[i] = counter;
            }

            return Convert.ToString(totalCounter);
        }

        private int SumOfDigitFactorials(int number)
        {
            int sum = 0;
            var digits = Sets.ToDigits(number);
            for (int i = 0; i < digits.Length; i++)
            {
                sum += digitFactorials[digits[i]];
            }
            return sum;
        }
    }
}
