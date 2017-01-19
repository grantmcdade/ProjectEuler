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
using System.Diagnostics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem071 : IProblem
    {
        public string Description
        {
            get { return "Listing reduced proper fractions in ascending order of size."; }
        }

        public string Solve()
        {
            // Try using cross multiplication to find a numerator which is as close as possible to 3 / 7
            var nearestN = 0;
            var largestFraction = 0d;
            for (int d = 1000000; d >= 0; d--)
            {
                var n = (int)Math.Floor(d / 7.0f * 3.0f);
                if (n <= 0)
                    break;

                for (; n < d; n++)
                {
                    var t1 = n * 7;
                    var t2 = d * 3;
                    if (t1 > t2)
                        break;

                    if (Util.Gcd(d, n) == 1)
                    {
                        var key = (double)n / (double)d;
                        if (key > largestFraction && n != 3)
                        {
                            largestFraction = key;
                            nearestN = n;
                        }
                    }
                }
            }

            return Convert.ToString(nearestN);
        }
    }
}
