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
    class Problem076 : IProblem
    {
        public string Description
        {
            get { return "How many different ways can one hundred be written as a sum of at least two positive integers?"; }
        }

        public string Solve()
        {
            long target = 100;
            long[] ways = new long[target+1];
            ways[0] = 1;
            for (int i = 1; i < target; i++)
			{
                for (int j = i; j <= target; j++)
                {
                    ways[j] += ways[j - i];
                }
			}
            return Convert.ToString(ways[target]);
        }
    }
}
