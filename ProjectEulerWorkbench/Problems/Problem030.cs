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
    class Problem030 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all the numbers that can be written as the sum of fifth powers of their digits."; }
        }

        public string Solve()
        {
            ulong sumOfNumbers = 0;
            for (ulong i = 2; i < 1000000; i++)
            {
                var sumOfPowers = SumOfPowers(i, 5);
                if (sumOfPowers == i)
                    sumOfNumbers += i;
            }

            return Convert.ToString(sumOfNumbers);
        }

        private ulong SumOfPowers(ulong value, int pow)
        {
            var digits = Sets.ToDigits(value);
            ulong sumOfPowers = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sumOfPowers += Util.Pow((ulong)digits[i], pow);
            }
            return sumOfPowers;
        }
    }
}
