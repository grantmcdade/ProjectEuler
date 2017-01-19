﻿/*
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
    class Problem053 : IProblem
    {
        public string Description
        {
            get { return "How many values of C(n,r), for 1 ≤ n ≤ 100, exceed one-million?"; }
        }

        public string Solve()
        {
            ulong count = 0;
            for (int n = 23; n <= 100; n++)
            {
                for (int r = 1; r <= n; r++)
                {
                    var ncr = Util.Factorial(n) / (Util.Factorial(r) * Util.Factorial(n - r));
                    if (ncr > 1000000)
                        ++count;
                }
            }
            return Convert.ToString(count);
        }
    }
}
