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
    class Problem358 : IProblem
    {
        public string Description
        {
            get { return "Cyclic numbers"; }
        }

        public string Solve()
        {
            string top = "00000000137";
            string match = "0000000013743210";
            // string bottom = "56789";

            var digits = new StringBuilder();
            var found = false;
            var sum = 0UL;

            // Observation: The numbers at the end of the first half must be 43210 in order to create 99999 when the cyclic number is split and the two halves added

            // 724637687UL is the first prime which produces a cyclic number starting with '00000000137', 729927008 is the first prime which produces '00000000136'
            // this leaves us with 258 891 primes and posible answers in that range! At 33 seconds per number we need a faster algorithm to calculate this... it will
            // take more than 98 days to test all possibilities
            // var p = 724637682UL;
            // var prime = 724637686UL; // Starting prime
            var prime = 724638127UL; // Last checked value
            //var prime = 16UL;
            do
            {
                ++prime;
                if (Util.IsPrime(prime))
                {
                    found = false;
                    sum = 0UL;
                    var end = prime / 2 - 5UL;
                    var digitCount = 0UL;
                    var remainder = 1UL;
                    var product = 0UL;
                    var digit = 0UL;
                    digits.Clear();
                    do
                    {
                        ++digitCount;
                        product = remainder * 10;
                        digit = product / prime;
                        remainder = product % prime;
                        sum += digit;

                        // Collect the digits at the start
                        if (digitCount <= 11)
                        {
                            digits.Append(digit);
                            if (digits.Length == 11)
                            {
                                var currentStart = digits.ToString();
                                if (currentStart == top)
                                {
                                    found = true;
                                }
                            }
                        }

                        // Collect the digits at the end
                        if (digitCount > end)
                        {
                            digits.Append(digit);

                            // Only capture 5 digits
                            if (digitCount >= end + 5)
                                break;
                        }
                    } while (remainder != 1);

                    var result = digits.ToString();
                    if (result == match)
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                    }

                    //if (t != (p - 1))
                    //{
                    //    found = false;
                    //}
                    //else
                    //{
                    //    // Found a cyclic number
                    //    found = true;
                    //}
                }
            } while (!found);

            return Convert.ToString(sum);
        }
    }
}
