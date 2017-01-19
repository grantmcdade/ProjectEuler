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
    class Problem024 : IProblem
    {
        public string Description
        {
            get { return "What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?"; }
        }

        private void Swap(int[] digits, int x, int y)
        {
            int t = digits[x];
            digits[x] = digits[y];
            digits[y] = t;
        }

        public string Solve()
        {
            const int size = 10;

            int[] digits = new int[size];
            for (int i = 0; i < size; i++)
                digits[i] = i;

            int counter = 1;
            bool permutationFound = false;
            do
            {
                permutationFound = false;
                for (int k = size - 2; k >= 0; k--)
                {
                    if (digits[k] < digits[k + 1])
                    {
                        for (int l = digits.Length - 1; l >= 0; l--)
                        {
                            if (digits[k] < digits[l])
                            {
                                Swap(digits, k, l);
                                Array.Reverse(digits, k + 1, digits.Length - (k + 1));
                                break;
                            }
                        }
                        permutationFound = true;
                        ++counter;
                        break;
                    }
                }
                if (!permutationFound)
                    break;
            } while (counter < 1000000);

            ulong permutationValue = UInt64.Parse(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                digits[0], digits[1], digits[2], digits[3], digits[4],
                digits[5], digits[6], digits[7], digits[8], digits[9]));
            return Convert.ToString(permutationValue);
        }

        void GenerateRandomPermutation()
        {
            const int size = 10;

            int[] digits = new int[size];
            for (int i = 0; i < size; i++)
                digits[i] = i;

            var random = new Random();
            var permutationSet = new HashSet<ulong>();
            do
            {
                for (int i = 0; i < size; i++)
                {
                    int index = random.Next(i, size);
                    int temp = digits[i];
                    digits[i] = digits[index];
                    digits[index] = temp;

                    ulong permutationValue = UInt64.Parse(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                        digits[0], digits[1], digits[2], digits[3], digits[4],
                        digits[5], digits[6], digits[7], digits[8], digits[9]));
                    permutationSet.Add(permutationValue);
                }
            } while (permutationSet.Count < Int16.MaxValue);
        }
    }
}
