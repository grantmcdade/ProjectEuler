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
    class Problem035 : IProblem
    {
        public string Description
        {
            get { return "How many circular primes are there below one million?"; }
        }

        public string Solve()
        {
            int number = 0;
            for (ulong i = 2; i < 1000000; i++)
            {
                if (IsCircularPrime(i))
                {
                    ++number;
                    System.Diagnostics.Debug.WriteLine("Circular Prime: {0,5}", i);
                }
            }

            return Convert.ToString(number);
        }

        private bool IsCircularPrime(ulong value)
        {
            bool result = true;

            if (!Util.IsPrime(value))
                return false;

            int length = Sets.DigitCount(value);
            for (int i = 1; i < length; i++)
            {
                value = Rotate(value, length);
                if (!Util.IsPrime(value))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private ulong Rotate(ulong value, int length)
        {
            // We can't rotate single digit numbers
            if (value < 10)
                return value;

            ulong upperMask = (ulong)Math.Pow(10, length - 1);
            ulong result = (value % upperMask); // Mask off the top digit
            ulong topDigit = (value - result) / upperMask; // Calculate the top digit
            result *= 10; // Shift by 10
            result += topDigit; // Add the top digit as the new bottom digit

            return result;
        }
    }
}
