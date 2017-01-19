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
    class Problem003 : IProblem
    {
        public string Description
        {
            get { return "Find the largest prime factor of a composite number."; }
        }

        public string Solve()
        {
            const long value = 600851475143;

            // Divide value by prime numbers over and over until the result is also a prime number
            ulong denominator = 2;
            ulong numerator = value;
            ulong largestFactor = 0;
            do
            {
                if (numerator % denominator == 0)
                {
                    // divisor is a factor
                    largestFactor = denominator;
                    numerator = numerator / denominator;
                }
                else
                {
                    // divisor is not a factor, find the next prime number to test
                    do { ++denominator; } while (!Util.IsPrime(denominator));
                }
            } while (numerator != 1);

            return Convert.ToString(largestFactor);
        }
    }
}
