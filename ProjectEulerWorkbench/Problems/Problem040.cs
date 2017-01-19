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
    class Problem040 : IProblem
    {
        public string Description
        {
            get { return "Finding the nth digit of the fractional part of the irrational number."; }
        }

        public string Solve()
        {
            int i = 0;
            StringBuilder sb = new StringBuilder(1000000);
            do
            {
                ++i;
                sb.Append(i);
            } while (sb.Length < 1000000);

            const ulong offset = 48;
            int index = 1;
            ulong result = 1;
            do
            {
                result *= (ulong)sb[index - 1] - offset;
                index *= 10;
            } while (index < sb.Length);
            // ulong result = ((uint)sb[1] - offset)*((uint)sb[11] - offset)*((uint)sb[101] - offset)*((uint)sb[1001] - offset)*((uint)sb[10001] - offset)*((uint)sb[100001] - offset)*((uint)sb[1000001] - offset);
            return Convert.ToString(result);
        }
    }
}
