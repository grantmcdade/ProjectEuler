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
    class Problem005 : IProblem
    {
        public string Description
        {
            get { return "What is the smallest number divisible by each of the numbers 1 to 20?"; }
        }

        public string Solve()
        {
            var value = 20;
            var evenlyDivisible = true;
            do
            {
                evenlyDivisible = true;
                for (var i = 1; i <= 20; i++)
                {
                    if (value % i != 0)
                    {
                        evenlyDivisible = false;
                        break;
                    }
                }
                if (evenlyDivisible) break;
                // Only a multiple of 20 can be evenly divisible by 
                // all the numbers between 1 and 20
                value += 20;
            } while (true);
            return Convert.ToString(value);
        }
    }
}
