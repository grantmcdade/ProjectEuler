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
    class Problem014 : IProblem
    {

        public string Description
        {
            get { return "Find the longest sequence using a starting number under one million."; }
        }

        public string Solve()
        {
            const uint limit = 1000000;
            uint[] steps = new uint[limit];
            steps[1] = 1;
            for (uint i = 2; i < limit; i++)
            {
                uint n = i;
                steps[i] = 0;
                do
                {
                    // Increment the step count
                    ++steps[i];

                    // Calculate the next number in the step
                    if (n % 2 == 0)
                        n = n / 2;
                    else
                        n = 3 * n + 1;

                    // If we reach a number that we have already calculated
                    // then simply add the step count that we already know
                    // and exit
                    if (n < i)
                    {
                        steps[i] += steps[n];
                        break;
                    }
                } while (n > 1);
            }

            // Find out which number produced the longest sequence
            uint number = 0;
            for (uint i = 0; i < limit; i++)
            {
                if (steps[i] > steps[number])
                    number = i;
            }
            return Convert.ToString(number);
        }
    }
}
