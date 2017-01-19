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
using System.Numerics;

namespace ProjectEulerWorkbench.Problems
{
    class Problem025 : IProblem
    {
        public string Description
        {
            get { return "What is the first term in the Fibonacci sequence to contain 1000 digits?"; }
        }

        public string Solve()
        {
            BigInteger fa = new BigInteger(1);
            BigInteger fb = new BigInteger(1);
            BigInteger fibonacci = new BigInteger();
            ulong counter = 2;
            do
            {
                ++counter;
                fibonacci = fa + fb;
                fa = fb;
                fb = fibonacci;
            } while (fibonacci.ToString().Length < 1000);

            return Convert.ToString(counter);
        }
    }
}
