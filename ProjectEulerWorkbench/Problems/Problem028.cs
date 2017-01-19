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
    class Problem028 : IProblem
    {
        public string Description
        {
            get { return "What is the sum of both diagonals in a 1001 by 1001 spiral?"; }
        }

        public string Solve()
        {
            const int size = 1001;

            // Build a spiral multi dimensional array
            int[,] spiral = BuildSpiral(size);

            // Calculate the sum of the diagonals
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                int j = (size - 1) - i;
                if (i == j)
                    sum += spiral[i, i];
                else
                    sum += spiral[i, i] + spiral[j, i];
            }

            return Convert.ToString(sum);
        }

        private int[,] BuildSpiral(int size)
        {
            int[,] spiral = new int[size, size];
            int x = size / 2;
            int y = size / 2;
            int step = 1;
            int direction = 0;
            do
            {
                spiral[x, y] = step;
                ++step;

                // Determine which direction to move next
                switch (direction)
                {
                    case 0: // Positive y-axis movement
                        if (step > 2 && spiral[x + 1, y] == 0) // Check if we can turn right
                            ++direction;
                        break;
                    case 1: // Positive x-axis movement
                        if (spiral[x, y - 1] == 0)
                            ++direction;
                        break;
                    case 2: // Negative y-axis movement
                        if (spiral[x - 1, y] == 0)
                            ++direction;
                        break;
                    case 3: // Negative x-axis movement
                        if (spiral[x, y + 1] == 0)
                            direction = 0;
                        break;
                }

                switch (direction)
                {
                    case 0:
                        ++y;
                        break;
                    case 1:
                        ++x;
                        break;
                    case 2:
                        --y;
                        break;
                    case 3:
                        --x;
                        break;
                }

            } while (x < spiral.GetLength(0) && y < spiral.GetLength(1));

            return spiral;
        }

        private void ShowSpiral(int[,] spiral)
        {
            for (int x = 0; x < spiral.GetLength(0); x++)
            {
                for (int y = 0; y < spiral.GetLength(1); y++)
                {
                    System.Diagnostics.Debug.Write(String.Format("{0,2}", spiral[x, y]));
                }
                System.Diagnostics.Debug.WriteLine(String.Empty);
            }
        }
    }
}
