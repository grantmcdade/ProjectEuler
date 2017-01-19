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
    class Problem012 : IProblem
    {
        public string Description
        {
            get { return "What is the value of the first triangle number to have over five hundred divisors?"; }
        }

        public string Solve()
        {
            int f = 0, n = 0, i = 0;
            var squares = new HashSet<int>();
            do
            {
                ++i;
                squares.Add(n + n + i);
                n = n + i;
                f = 0;
                var sqrt = Math.Sqrt(n);
                for (int j = 1; j <= sqrt; j++)
                {
                    if (n % j == 0) f += 2;
                }
                if (squares.Contains(n)) --f;
            } while (f < 500);

            return Convert.ToString(n);
        }
    }
}
