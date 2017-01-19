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
    class Problem019 : IProblem
    {
        public string Description
        {
            get { return "How many Sundays fell on the first of the month during the twentieth century?"; }
        }

        public string Solve()
        {
            var dt = new DateTime(1901, 1, 1);
            var end = new DateTime(2000, 12, 31);

            int firstSundays = 0;
            while (dt < end)
            {
                if (dt.Day == 1 && dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    firstSundays += 1;
                }
                dt = dt.AddDays(1);
            }

            return Convert.ToString(firstSundays);
        }
    }
}
