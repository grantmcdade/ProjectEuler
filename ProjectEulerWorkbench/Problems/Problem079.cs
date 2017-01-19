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
using System.IO;
using System.Collections;
using System.Diagnostics;
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem079 : IProblem
    {
        private IPathProvider _provider;

        public Problem079(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "By analysing a user's login attempts, can you determine the secret numeric passcode?"; }
        }

        public string Solve()
        {
            var keys = File.ReadAllLines(_provider.GetFullyQualifiedPath("keylog.txt"));

            var lastDigit = 0;
            var usedDigits = new List<int>();
            foreach (var key in keys)
            {
                lastDigit = 0;
                foreach (var digit in key)
                {
                    var currentDigit = digit - 48;
                    if (!usedDigits.Contains(currentDigit))
                    {
                        if (lastDigit != 0)
                            usedDigits.Insert(usedDigits.IndexOf(lastDigit) + 1, currentDigit);
                        else
                            usedDigits.Add(currentDigit);
                    }
                    else if (lastDigit != 0)
                    {
                        // Verify that this digit is in the correct order relative to the last digit
                        var currentIndex = usedDigits.IndexOf(currentDigit);
                        var lastIndex = usedDigits.IndexOf(lastDigit);
                        if (currentIndex < lastIndex)
                        {
                            // If they are not in the correct order then move the current digit
                            // to the correct location
                            usedDigits.RemoveAt(currentIndex);
                            usedDigits.Insert(usedDigits.IndexOf(lastDigit) + 1, currentDigit);
                        }
                    }
                    lastDigit = currentDigit;
                }
            }

            var number = Sets.ToNumber(usedDigits.ToArray());

            return Convert.ToString(number);
        }
    }
}
