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
    class Problem057 : IProblem
    {
        public string Description
        {
            get { return "Investigate the expansion of the continued fraction for the square root of two."; }
        }

        public string Solve()
        {
            int count = 0;
            for (int i = 1; i <= 1000; i++)
            {
                var rootFraction = GetRootFraction(i);
                if (DigitCount(rootFraction.Item1) > DigitCount(rootFraction.Item2))
                {
                    ++count;
                }
            }
            return Convert.ToString(count);
        }

        private int DigitCount(BigInteger value)
        {
            int count = 0;
            do
            {
                ++count;
                value /= 10;
            } while (value > 0);
            return count;
        }

        private Tuple<BigInteger, BigInteger> GetRootFraction(int iteration)
        {
            BigInteger n = 1, d = 2;
            for (int i = iteration; i > 0; i--)
            {
                if (i > 1)
                {
                    // Calculate the new numerator
                    n = 2 * d + n;
                    // Swap the numerator and denominator
                    d = n + d - (n = d);
                }
                else
                {
                    n = d + n;
                }
            }
            return new Tuple<BigInteger, BigInteger>(n, d);
        }
    }
}
