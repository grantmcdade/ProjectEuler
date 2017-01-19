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
    class Problem006 : IProblem
    {
        public string Description
        {
            get { return "What is the difference between the sum of the squares and the square of the sums?"; }
        }

        public string Solve()
        {
            long sumOfSquares = 0;
            long squareOfSums = 0;
            for (int i = 1; i <= 100; i++)
            {
                sumOfSquares += (i * i);
                squareOfSums += i;
            }
            squareOfSums = squareOfSums * squareOfSums;
            return Convert.ToString(squareOfSums - sumOfSquares);
        }
    }
}
