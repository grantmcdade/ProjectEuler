using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEulerWorkbench.Problems
{
    class Problem084 : IProblem
    {
        private const int simulatioCount = 1000000;
        private const int squareJail = 10;
        private const int squareGoToJail = 30;
        private const int squareE3 = 24;
        private const int squareGo = 0;
        private const int squareC1 = 11;
        private const int squareH2 = 39;
        private const int squareR1 = 5;

        public string Description => "Monopoly odds";

        public string Solve()
        {
            var rand = new Random();
            var board = new int[40];
            int[] communityChestSquares = { 2, 17, 33 };
            int[] chanceSquares = { 7, 22, 36 };
            var communityChestCard = rand.Next(16);
            var chanceCard = rand.Next(16);
            var currentPosition = 0;
            var doubleCount = 0;

            // Simulate the game
            for (int i = 0; i < simulatioCount; i++)
            {
                var dice1 = rand.Next(4) + 1;
                var dice2 = rand.Next(4) + 1;

                doubleCount = (dice1 == dice2) ? ++doubleCount : 0;
                if (doubleCount == 3)
                {
                    doubleCount = 0;
                    currentPosition = squareJail;
                }
                else
                {
                    currentPosition = (currentPosition + dice1 + dice2) % 40;

                    // We are on a communit chest square
                    if (communityChestSquares.Contains(currentPosition))
                    {
                        if (communityChestCard % 8 == 0)
                        {
                            currentPosition = (communityChestCard % 16 == 0) ? squareJail : squareGo;
                        }
                        ++communityChestCard;
                    }
                    else if (chanceSquares.Contains(currentPosition)) // We are on a chance square
                    {
                        var selectedCard = chanceCard % 16;
                        switch (selectedCard)
                        {
                            case 0:
                                currentPosition = squareGo;
                                break;
                            case 1:
                                currentPosition = squareJail;
                                break;
                            case 2:
                                currentPosition = squareC1;
                                break;
                            case 3:
                                currentPosition = squareE3;
                                break;
                            case 4:
                                currentPosition = squareH2;
                                break;
                            case 5:
                                currentPosition = squareR1;
                                break;
                            case 6:
                                currentPosition = GetNextR(currentPosition);
                                break;
                            case 7:
                                currentPosition = GetNextR(currentPosition);
                                break;
                            case 8:
                                currentPosition = GetNextU(currentPosition);
                                break;
                            case 9:
                                currentPosition -= 3;
                                break;
                            default:
                                break;
                        }
                        ++chanceCard;
                    }
                    else if (currentPosition == squareGoToJail)
                    {
                        currentPosition = squareJail;
                    }
                }
                ++board[currentPosition];
            }

            // Create the modal string
            var names = new string[40];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = $"{i:00}";
            }

            Array.Sort(board, names);

            return $"{names[39]}{names[38]}{names[37]}";
        }

        private int GetNextR(int currentPosition)
        {
            int[] positionsForR = { 5, 15, 25, 35 };
            return GetNext(currentPosition, positionsForR);
        }

        private int GetNextU(int currentPosition)
        {
            int[] positionsForU = { 12, 28 };
            return GetNext(currentPosition, positionsForU);
        }

        private int GetNext(int currentPosition, int[] positions)
        {
            var currentIndex = Array.FindLastIndex(positions, delegate(int position) { return position < currentPosition; });
            if ( currentIndex >= 0 )
            {
                ++currentIndex;
                if (currentIndex >= positions.Length)
                {
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex = 0;
            }
            return positions[currentIndex];
        }
    }
}
