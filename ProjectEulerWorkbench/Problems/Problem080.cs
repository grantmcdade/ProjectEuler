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
    class Problem080 : IProblem
    {
        public string Description
        {
            get { return "Calculating the digital sum of the decimal digits of irrational square roots."; }
        }

        public string Solve()
        {
            // Attempt to solve using the digit by digit calculation method described on Wikipedia (http://en.wikipedia.org/wiki/Methods_of_computing_square_roots)

            var digits = new List<int>(100);
            var totalSum = 0UL;
            for (int i = 2; i < 100; i++)
            {
                // Note: For values larger than 99 it would be necesary to split them
                // into groups of 2 and feed them into the algorithm when calculating the
                // next value of c. This could be done quite well with a queue.

                digits.Clear();
                BigInteger c = i;
                BigInteger p = 0;
                while (digits.Count < 100)
                {
                    // Determine the greatest digit x such that y = (20 * p + x) * x does not exceed c.
                    BigInteger x = 0;
                    if (p > 0)
                    {
                        x = c / (20 * p);
                    }

                    // Keep tweak x until it satisfies the equation
                    var y = (20 * p + x) * x;
                    var nextY = (20 * p + (x + 1)) * (x + 1);
                    while (!(y <= c && nextY > c))
                    {
                        if (y > c) --x; else ++x;
                        y = (20 * p + x) * x;
                        nextY = (20 * p + (x + 1)) * (x + 1);
                    }

                    // Collect the digit x
                    digits.Add((int)x);

                    // Calculate the next value of p and the remainder which
                    // becomes the next value of c
                    p = p * 10 + x;
                    c = (c - y) * 100;

                    // Note: To support values larger than 99 we would need to add
                    // the next group of 2 to c at this point

                    // If there is no remainder then this is a perfect square
                    // and there is no fraction
                    if (c == 0)
                        break;
                }

                if (digits.Count == 100)
                {
                    var sum = digits.Sum();
                    totalSum += (ulong)sum;
                }
            }

            return Convert.ToString(totalSum);
        }

        public string Solve_Bruteforce()
        {
            const int length = 101;
            var totalSum = 0L;
            BigInteger limit = BigInteger.Parse("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            for (int i = 2; i <= length; i++)
            {
                var sum = 0;
                var fractions = ContinuedFractions.GetContinuedFractions(i);
                if (fractions.Count > 1)
                {
                    var j = 2;
                    var answer = ContinuedFractions.ResolveContinuedFraction(fractions, j++);
                    while (answer.Item2 < limit)
                        answer = ContinuedFractions.ResolveContinuedFraction(fractions, j++);

                    var m = answer.Item2 / limit;
                    var f = answer.Item1 * limit;

                    var s = f.ToString();
                    for (int k = 0; k < 100; k++)
                    {
                        sum += s[k] - 48;
                    }
                }
                totalSum += sum;
            }
            return Convert.ToString(totalSum);
        }
    }
}
