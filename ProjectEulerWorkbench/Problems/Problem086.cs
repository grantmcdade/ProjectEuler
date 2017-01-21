using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEulerWorkbench.Problems
{
    class Problem086 : IProblem
    {
        private const int maxCubeCount = 1000000;

        public string Description => "Find the least value of M such that the number of solutions first exceeds one million.";

        public string Solve()
        {
            var numCubes = 0;
            var length = 0;

            while (numCubes < maxCubeCount)
            {
                ++length;
                for (int width = 1; width <= length; width++)
                {
                    for (int height = 1; height <= width; height++)
                    {
                        var distance = Math.Sqrt(length * length + (width + height) * (width + height));
                        if ( distance ==  (int)distance )
                        {
                            ++numCubes;
                        }
                    }
                }
            }

            return length.ToString();
        }
    }
}
