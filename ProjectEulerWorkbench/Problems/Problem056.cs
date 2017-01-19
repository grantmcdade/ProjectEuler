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
    class Problem056 : IProblem
    {
        public string Description
        {
            get { return "Considering natural numbers of the form, ab, finding the maximum digital sum."; }
        }

        public string Solve()
        {
            BigInteger maximumSum = 0;
            for (int a = 1; a < 100; a++)
            {
                for (int b = 1; b < 100; b++)
                {
                    var value = Pow(a, b);
                    var sum = Convert.ToString(value).Sum(c => c - '0');
                    if (sum > maximumSum)
                        maximumSum = sum;
                }
            }
            return Convert.ToString(maximumSum);
        }

        private BigInteger Pow(BigInteger a, BigInteger b)
        {
            BigInteger result = 1;
            for (BigInteger i = 0; i < b; i++)
            {
                result *= a;
            }
            return result;
        }
    }
}
