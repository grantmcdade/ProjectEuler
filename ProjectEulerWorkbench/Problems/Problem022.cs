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

using ProjectEuler.Library;
using System;
using System.Collections.Generic;

namespace ProjectEulerWorkbench.Problems
{
    class Problem022 : IProblem
    {
        private IPathProvider _provider;

        public Problem022(IPathProvider provider)
        {
            _provider = provider;
        }

        public string Description
        {
            get { return "What is the total of all the name scores in the file of first names?"; }
        }

        public string Solve()
        {
            var namesList = System.IO.File.ReadAllText(_provider.GetFullyQualifiedPath("names.txt"));
            var namesSplit = namesList.Split(',');
            var names = new SortedList<string, int>();
            foreach (var line in namesSplit)
            {
                names.Add(line, 0);
            }

            ulong sum = 0;
            for (int i = 0; i < names.Keys.Count; i++)
            {
                var nameValue = GetNameValue(names.Keys[i]);
                var totalValue = (ulong)(i + 1) * nameValue;
                sum += totalValue;
            }

            return Convert.ToString(sum);
        }

        private ulong GetNameValue(string name)
        {
            ulong sum = 0;
            foreach (var character in name)
            {
                if (character > 64)
                    sum += (ulong)(character - 64);
            }
            return sum;
        }
    }
}
