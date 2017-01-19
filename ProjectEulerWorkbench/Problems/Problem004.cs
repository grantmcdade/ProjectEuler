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
    class Problem004 : IProblem
    {
        public string Description
        {
            get { return "Find the largest palindrome made from the product of two 3-digit numbers."; }
        }

        public string Solve()
        {
            HashSet<long> palindromes = new HashSet<long>();
            for (int i = 100; i <= 999; i++)
            {
                for (int j = 100; j <= 999; j++)
                {
                    long value = i * j;
                    // Check if the value is a palindrome
                    char[] characters = Convert.ToString(value).ToCharArray();
                    Array.Reverse(characters);
                    long reverse = Convert.ToInt64(new string(characters));
                    if (value == reverse)
                    {
                        palindromes.Add(value);
                    }
                }
            }

            long[] sortedPalindromes = new long[palindromes.Count];
            palindromes.CopyTo(sortedPalindromes);
            Array.Sort<long>(sortedPalindromes);

            return Convert.ToString(sortedPalindromes[sortedPalindromes.Length - 1]);
        }
    }
}
