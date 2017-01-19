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
    class Problem055 : IProblem
    {
        public string Description
        {
            get { return "How many Lychrel numbers are there below ten-thousand?"; }
        }

        public string Solve()
        {
            const int length = 10000;
            int lychrelCount = 0;
            for (int i = 0; i < length; i++)
            {
                var value = (ulong)i;
                bool isLychrel = true;
                for (int j = 0; j < 50; j++)
                {
                    int[] digits = Sets.ToDigits(value);
                    Array.Reverse(digits);
                    var reverseValue = Sets.ToNumber(digits);
                    var sum = value + reverseValue;
                    if (IsPalindrome(sum))
                    {
                        isLychrel = false;
                        break;
                    }
                    value = sum;
                }
                if (isLychrel)
                    ++lychrelCount;
            }

            return Convert.ToString(lychrelCount);
        }

        private bool IsPalindrome(ulong value)
        {
            if (value < 10)
                return true;

            int[] digits = Sets.ToDigits(value);

            int testRange;
            if (digits.Length % 2 == 0)
                testRange = digits.Length / 2;
            else
                testRange = (digits.Length - 1) / 2;

            for (int i = 0; i < testRange; i++)
            {
                if (digits[i] != digits[digits.Length - 1 - i])
                    return false;
            }

            return true;
        }
    }
}
