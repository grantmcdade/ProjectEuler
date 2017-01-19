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
    class Problem073 : IProblem
    {
        public string Description
        {
            get { return "How many fractions lie between 1/3 and 1/2 in a sorted set of reduced proper fractions?"; }
        }

        public string Solve()
        {
            const int limit = 12000;
            var fractionCount = 0;
            for (int d = 1; d <= limit; d++)
            {
                var n = (int)Math.Ceiling(d / 3.0d);
                var t1 = n * 2;
                while (t1 < d)
                {
                    if (Util.Gcd(n, d) == 1)
                        ++fractionCount;
                    ++n;
                    t1 = n * 2;
                }
            }

            return Convert.ToString(fractionCount - 1);
        }
    }
}
