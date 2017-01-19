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
    class Problem010 : IProblem
    {
        public string Description
        {
            get { return "Calculate the sum of all the primes below two million."; }
        }

        public string Solve()
        {
            ulong[] primes = Util.GetPrimeNumbersLessThan((ulong)2000000);
            ulong sum = 0;
            foreach (var item in primes)
            {
                sum += item;
            }
            return Convert.ToString(sum);
        }
    }
}
