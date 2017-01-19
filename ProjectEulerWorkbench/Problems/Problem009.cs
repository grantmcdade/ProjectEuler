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

using Euclid = System.Tuple<System.UInt64, System.UInt64, System.UInt64>;

namespace ProjectEulerWorkbench.Problems
{
    class Problem009 : IProblem
    {

        public string Description
        {
            get { return "Find the only Pythagorean triplet, {a, b, c}, for which a + b + c = 1000."; }
        }

        public string Solve()
        {
            ulong product = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = i + 1; j < 1000; j++)
                {
                    Euclid pythagorean = Util.Euclid((ulong)j, (ulong)i);
                    ulong sum = pythagorean.Item1 + pythagorean.Item2 + pythagorean.Item3;
                    if (sum >= 1000)
                    {
                        if (sum == 1000)
                        {
                            // Found it!
                            product = pythagorean.Item1 * pythagorean.Item2 * pythagorean.Item3;
                        }
                        break;
                    }
                }
            }
            return Convert.ToString(product);
        }
    }
}
