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
    class Problem002 : IProblem
    {

        public string Description
        {
            get { return "By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms."; }
        }

        public string Solve()
        {
            const int limit = 4000000;
            int first = 1, second = 2, current = 3;
            int sum = 2; // initialise the sum to 2 because we won't have a chance to add that value in the loop
            while (current < limit)
            {
                if (current % 2 == 0) sum += current;
                first = second;
                second = current;
                current = first + second;
            }
            return Convert.ToString(sum);
        }
    }
}
