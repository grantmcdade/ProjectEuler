using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEulerWorkbench.Problems
{
    class Problem085 : IProblem
    {
        public string Description => "Although there exists no rectangular grid that contains exactly two million rectangles, find the area of the grid with the nearest solution.";

        public string Solve()
        {
            var width = 0;
            var height = 0;

            var blockCount = GetBlockCount(width, height);
            var maxWidth = 0;
            var maxHeight = 0;

            for (;;)
            {
                height++;
                width = 0;
                for (;;)
                {
                    width++;
                    var newCount = GetBlockCount(width, height);
                    if (newCount > 2000000)
                        break;

                    if (newCount > blockCount)
                    {
                        blockCount = newCount;
                        maxWidth = width;
                        maxHeight = height;
                    }
                }

                if (width == 1)
                    break;
            }

            return $"{maxWidth * maxHeight}, {blockCount}";
        }

        // From the formula n(n+1)m(m+1)/4
        int GetBlockCount(int width, int height)
        {
            var blockCount = (height * (height + 1) * width * (width + 1)) / 4;
            return blockCount;
        }
    }
}
