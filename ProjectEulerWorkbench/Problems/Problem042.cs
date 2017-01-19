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
using ProjectEuler.Library;

namespace ProjectEulerWorkbench.Problems
{
    class Problem042 : IProblem
    {
        private IPathProvider _provider;

        public Problem042(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "How many triangle words does the list of common English words contain?"; }
        }

        public string Solve()
        {
            string allWords = File.ReadAllText(_provider.GetFullyQualifiedPath("words.txt"));
            allWords = allWords.Replace("\"", String.Empty);
            string[] words = allWords.Split(',');

            int triangleWords = 0;
            for (int i = 0; i < words.Length; i++)
            {
                int number = CountLetters(words[i]);
                if (Util.IsTriangleNumber(number))
                {
                    ++triangleWords;
                }
            }

            return Convert.ToString(triangleWords);
        }

        private int CountLetters(string word)
        {
            int count = 0;

            for (int i = 0; i < word.Length; i++)
            {
                count += (word[i] - 64);
            }

            return count;
        }

    }
}
