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
    class Problem036 : IProblem
    {
        public string Description
        {
            get { return "Find the sum of all numbers less than one million, which are palindromic in base 10 and base 2."; }
        }

        public string Solve()
        {
            const int length = 1000000;
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                string value = Convert.ToString(i);
                string binary = Convert.ToString(i, 2);

                char[] valueCharacters = value.ToCharArray();
                Array.Reverse(valueCharacters);
                string reverseValue = new String(valueCharacters);
                
                char[] binaryCharacters = binary.ToCharArray();
                Array.Reverse(binaryCharacters);
                string reverseBinary = new String(binaryCharacters);
                
                if (String.Equals(value, reverseValue, StringComparison.Ordinal)
                    && String.Equals(binary, reverseBinary, StringComparison.Ordinal))
                {
                    sum += i;
                }
            }

            return Convert.ToString(sum);
        }
    }
}
