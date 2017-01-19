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
    class Problem058 : IProblem
    {
        public string Description
        {
            get { return "Investigate the number of primes that lie on the diagonals of the spiral grid."; }
        }

        public string Solve()
        {
            // Each layer adds 2 additional numbers to the side length
            var primeCount = 0;
            var totalLength = 1;
            var sideLength = 0;
            var diagonals = 1;
            var ratio = 0d;
            do
            {
                diagonals += 4;
                sideLength += 2;
                for (int i = 0; i < 4; i++)
                {
                    totalLength += sideLength;
                    if (Util.IsPrime(totalLength))
                    {
                        ++primeCount;
                    }
                }
                ratio = (double)primeCount / (double)diagonals;
            } while (ratio > 0.10d);

            return Convert.ToString(sideLength + 1);
        }
    }
}
