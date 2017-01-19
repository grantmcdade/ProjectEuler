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
    class Problem063 : IProblem
    {
        public string Description
        {
            get { return "How many n-digit positive integers exist which are also an nth power?"; }
        }

        public string Solve()
        {
            var foundSpecialNumber = false;
            var specialNumbers = 0;
            var number = 0;
            var power = 0;
            do
            {
                foundSpecialNumber = false;

                ++power;

                BigInteger upperLimit = 1;
                for (int i = 0; i < power; i++)
                    upperLimit *= 10;
                BigInteger lowerLimit = upperLimit / 10UL;

                BigInteger result = 0;
                do
                {
                    ++number;

                    result = 1;
                    for (int i = 0; i < power; i++)
                        result *= number;

                    if (result >= lowerLimit && result < upperLimit)
                    {
                        foundSpecialNumber = true;
                        ++specialNumbers;
                    }
                } while (result < upperLimit);
                number = 0;

            } while (foundSpecialNumber);

            return Convert.ToString(specialNumbers);
        }
    }
}
