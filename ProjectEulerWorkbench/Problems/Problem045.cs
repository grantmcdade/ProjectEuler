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
    class Problem045 : IProblem
    {
        public string Description
        {
            get { return "After 40755, what is the next triangle number that is also pentagonal and hexagonal?"; }
        }

        public string Solve()
        {
            // All triangle numbers are hexagonal so we only need to test pentagonal and hexagonal numbers
            var pentagonal = 0U;
            var hexagonal = 0U;
            var pn = 165U;
            var hn = 143U;
            do
            {
                do
                {
                    ++hn;
                    hexagonal = hn * (2 * hn - 1);
                } while (hexagonal < pentagonal);

                do
                {
                    ++pn;
                    pentagonal = pn * (3 * pn - 1) / 2;    
                } while (pentagonal < hexagonal);
                
            } while (pentagonal != hexagonal);

            return Convert.ToString(pentagonal);
        }
    }
}
