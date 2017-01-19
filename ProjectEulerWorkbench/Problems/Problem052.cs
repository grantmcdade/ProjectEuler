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
    class Problem052 : IProblem
    {
        public string Description
        {
            get { return "Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits in some order."; }
        }

        public string Solve()
        {
            ulong counter = 0;
            int length = 0;
            bool hasUniqueDigits = true;
            bool hasSameLength = true;
            do
            {
                ++counter;
                hasUniqueDigits = true;
                hasSameLength = true;

                // Check the length of all test values
                for (ulong i = 1; i <= 6; i++)
                {
                    ulong value = counter * i;
                    if (i == 1)
                    {
                        length = Sets.DigitCount(value);
                    }
                    else if (length != Sets.DigitCount(value))
                    {
                        hasSameLength = false;
                        break;
                    }
                }

                if (hasSameLength)
                {
                    // All test values have the same length, now test the digits
                    int[][] digitsUsed = new int[6][];
                    for (int i = 0; i < digitsUsed.Length; i++)
                    {
                        digitsUsed[i] = new int[10];
                    }
                    for (ulong i = 1; i <= 6; i++)
                    {
                        if (!HasUniqueDigits(counter * i, digitsUsed[i - 1]))
                        {
                            hasUniqueDigits = false;
                            break;
                        }
                    }

                    // If all the digits are unique then we need to check if all instances use
                    // the same digits
                    if (hasUniqueDigits)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (digitsUsed[0][j] != digitsUsed[i][j])
                                {
                                    hasUniqueDigits = false;
                                    break;
                                }
                            }
                            if (!hasUniqueDigits)
                                break;
                        }
                    }
                }

            } while (!(hasUniqueDigits && hasSameLength));

            return Convert.ToString(counter);
        }

        private bool HasUniqueDigits(ulong value, int[] digitsUsed)
        {
            // Clear the digits used array
            for (int i = 0; i < digitsUsed.Length; i++)
                digitsUsed[i] = 0;

            // Count each digit
            int[] digits = Sets.ToDigits(value);
            for (int i = 0; i < digits.Length; i++)
            {
                // If we saw this digit before then return false
                if (digitsUsed[digits[i]] != 0)
                    return false;
                ++digitsUsed[digits[i]];
            }
            return true;
        }
    }
}
