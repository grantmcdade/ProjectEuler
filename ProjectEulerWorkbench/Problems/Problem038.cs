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
    class Problem038 : IProblem
    {
        public string Description
        {
            get { return "What is the largest 1 to 9 pandigital that can be formed by multiplying a fixed number by 1, 2, 3, ... ?"; }
        }

        public string Solve()
        {
            int i = 0;
            uint largest = 0;
            do
            {
                string number = String.Empty;
                int count = 0;
                ++i;
                do
                {
                    ++count;
                    number += Convert.ToString(i * count);
                } while (number.Length < 9);
                if (count <= 1)
                    break;
                if (number.Length == 9)
                {
                    char[] digits = number.ToCharArray();
                    Array.Sort(digits);

                    bool isPandigital = true;
                    for (int j = 0; j < 9; j++)
                    {
                        if ((digits[j] - 48) != (j + 1))
                        {
                            isPandigital = false;
                            break;
                        }
                    }

                    if (isPandigital)
                    {
                        uint result = Convert.ToUInt32(number);
                        if (result > largest)
                            largest = result;
                    }
                }
            } while (i < 99999);
            return Convert.ToString(largest);
        }
    }
}
