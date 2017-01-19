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
    class Problem021 : IProblem
    {
        public string Description
        {
            get { return "Evaluate the sum of all amicable pairs under 10000."; }
        }

        public string Solve()
        {
            const ulong size = 10000;
            var sums = new ulong[size];
            var amicableNumbers = new HashSet<ulong>();
            for (ulong i = 1; i < size; i++)
            {
                var factors = Util.GetFactors(i);
                // Calculate the sum of the factors excluding the number itself
                var sum = Util.Sum(factors, factors.Length - 1);
                sums[i] = sum;
                if (sum < size && sums[sum] == i && i != sum)
                {
                    amicableNumbers.Add(i);
                    amicableNumbers.Add(sum);
                }
            }

            ulong amicableSum = 0;
            foreach (var number in amicableNumbers)
            {
                amicableSum += number;
            }

            return Convert.ToString(amicableSum);
        }
    }
}
