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
        private const int m = 100;

        private bool[] xMarkers;
        private bool[] yMarkers;
        private bool[] zMarkers;

        public Problem086()
        {
            xMarkers = new bool[m+1];
            yMarkers = new bool[m+1];
            zMarkers = new bool[m+1];
        }

        public string Description => "Find the least value of M such that the number of solutions first exceeds one million.";

        public string Solve()
        {
            int numCubes = 0;

            for (int x = 1; x <= m; x++)
            {
                for (int y = 1; y <= m; y++)
                {
                    for (int z = 1; z <= m; z++)
                    {
                        // Not seen cube before
                        if (!SeenCube(x, y, z))
                        {
                            double[] paths = new double[3];
                            paths[0] = Math.Sqrt(x * x + (y + z) * (y + z));
                            paths[1] = Math.Sqrt(y * y + (x + z) * (x + z));
                            paths[2] = Math.Sqrt(z * z + (y + x) * (y + x));

                            var countCube = false;
                            for (int i = 0; i < 3; i++)
                            {
                                countCube = IsInteger(paths[i]) ? true : countCube;
                            }

                            if (countCube)
                            {
                                ++numCubes;
                                Debug.WriteLine($"{x} {y} {z}");
                            }

                            // Seen cube now
                            MarkCubeAsSeen(x, y, z);
                        }
                    }
                }
            }

            return numCubes.ToString();
        }

        private void MarkCubeAsSeen(int x, int y, int z)
        {
            xMarkers[x] = true;
            yMarkers[y] = true;
            zMarkers[z] = true;
        }

        private bool SeenCube(int x, int y, int z)
        {
            if (xMarkers[x] && yMarkers[y] && zMarkers[z]) return true;
            if (yMarkers[x] && zMarkers[y] && xMarkers[z]) return true;
            if (zMarkers[x] && xMarkers[y] && yMarkers[z]) return true;
            return false;
        }

        private bool IsInteger(double value)
        {
            return (value * 10) % 10 == 0;
        }

        private double GetMaxLength(int width, int height)
        {
            return Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));
        }
    }
}
