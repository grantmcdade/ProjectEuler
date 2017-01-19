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
    class Problem034 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all numbers which are equal to the sum of the factorial of their digits."; }
        }

        public string Solve()
        {
            BigInteger sumOfCuriousNumbers = 0;

            ulong[] factorials = new ulong[10];
            for (int i = 0; i < factorials.Length; i++)
            {
                factorials[i] = (ulong)Util.Factorial(i);
            }

            // Don't process 0, 1 and 2 as they are not sums and therefore should not be
            // added to the total.
            for (ulong i = 3; i < 1000000; i++)
            {
                ulong sum = SumOfDigitFactorials(i, factorials);
                if (sum == i)
                {
                    sumOfCuriousNumbers += i;
                }
            }

            return Convert.ToString(sumOfCuriousNumbers);
        }

        private ulong SumOfDigitFactorials(ulong value, ulong[] factorials)
        {
            int[] digits = Sets.ToDigits(value);
            ulong sumOfFactorials = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sumOfFactorials += factorials[digits[i]];
            }
            return sumOfFactorials;
        }
    }
}
