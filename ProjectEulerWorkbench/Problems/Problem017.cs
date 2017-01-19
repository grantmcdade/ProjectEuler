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
    class Problem017 : IProblem
    {
        public string Description
        {
            get { return "How many letters would be needed to write all the numbers in words from 1 to 1000?"; }
        }

        public string Solve()
        {
            StringBuilder sb = new StringBuilder();
            bool teen = false;
            for (int i = 1; i <= 1000; i++)
            {
                if (i == 1000)
                {
                    sb.Append("one thousand");
                    break;
                }

                teen = false;
                if (i >= 100)
                {
                    var h = (i / 100) % 10;
                    if (h > 0)
                        sb.Append(hundreds[h]);

                    // If this is not an even hundred we need to append 'and'
                    if (i % 100 != 0)
                        sb.Append(" and ");
                }

                var tenth = i % 100;
                if (tenth >= 10)
                {
                    if (tenth >= 10 && tenth < 20)
                    {
                        sb.Append(teens[tenth - 10]);
                        teen = true;
                    }
                    else
                    {
                        var t = (tenth / 10) % 10;
                        if (t > 0)
                            sb.Append(tens[t]);

                        // If this is not an even ten we need to append a dash '-'
                        if (tenth % 10 != 0)
                            sb.Append("-");
                    }
                }

                if (!teen)
                {
                    var n = i % 10;
                    if (n > 0)
                        sb.Append(numbers[n]);
                }
                
                sb.AppendLine();
            }

            System.IO.File.WriteAllText("number.txt", sb.ToString());

            ulong totalCharacters = 0;
            for (int j = 0; j < sb.Length; j++)
            {
                if (sb[j] != ' ' && sb[j] != '-' && sb[j] != 0x0A && sb[j] != 0x0D)
                    ++totalCharacters;
            }
            
            return Convert.ToString(totalCharacters);
        }

        private string[] numbers =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        private string[] teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        private string[] tens =
        {
            "unused",
            "teen",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        private string[] hundreds =
        {
            "unused",
            "one hundred",
            "two hundred",
            "three hundred",
            "four hundred",
            "five hundred",
            "six hundred",
            "seven hundred",
            "eight hundred",
            "nine hundred",
        };
    }
}
