using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2017
{
    internal static class Day3
    {
        public static void Run()
        {
            PartOne(); //480
            PartTwo(); //349975
            Console.Read();
        }

        private static void PartTwo()
        {
            var gridSize = 347991;
            var grid = new Dictionary<string, int>();
            var moveOutcome = new MoveOutcome
            {
                X = 0,
                Y = 0,
                Direction = Move.First
            };

            for (var i = 1; i <= gridSize; i++)
            {
                moveOutcome = GetNextCoordinates(moveOutcome, grid);
                var sum = GetSumOfNeighbours(moveOutcome, grid);
                if (sum > gridSize)
                {
                    Console.WriteLine(sum);
                    break;
                }
                grid.Add(GetCoordinate(moveOutcome.X, moveOutcome.Y), sum);
            }
        }

        private static int GetSumOfNeighbours(MoveOutcome moveOutcome, Dictionary<string, int> grid)
        {
            //Check everything around (if there).
            var sum = 0;
            if (moveOutcome.X == 0 && moveOutcome.Y == 0)
            {
                //First move, return 1
                return 1;
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X + 1, moveOutcome.Y)))
            {
                sum += grid[GetCoordinate(moveOutcome.X + 1, moveOutcome.Y)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X + 1, moveOutcome.Y + 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X + 1, moveOutcome.Y + 1)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X, moveOutcome.Y + 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X, moveOutcome.Y + 1)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X - 1, moveOutcome.Y + 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X - 1, moveOutcome.Y + 1)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X - 1, moveOutcome.Y)))
            {
                sum += grid[GetCoordinate(moveOutcome.X - 1, moveOutcome.Y)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X - 1, moveOutcome.Y - 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X - 1, moveOutcome.Y - 1)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X, moveOutcome.Y - 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X, moveOutcome.Y - 1)];
            }
            if (grid.ContainsKey(GetCoordinate(moveOutcome.X + 1, moveOutcome.Y - 1)))
            {
                sum += grid[GetCoordinate(moveOutcome.X + 1, moveOutcome.Y - 1)];
            }
            return sum;
        }

        private static void PartOne()
        {
            var gridSize = 347991;
            var grid = new Dictionary<string, int>();
            var moveOutcome = new MoveOutcome
            {
                X = 0,
                Y = 0,
                Direction = Move.First
            };

            for (int i = 1; i <= gridSize; i++)
            {
                moveOutcome = GetNextCoordinates(moveOutcome, grid);
                grid.Add(GetCoordinate(moveOutcome.X, moveOutcome.Y), i);
            }
            var locationOfTarget = grid.FirstOrDefault(v => v.Value == gridSize).Key;
            var locationOfStart = grid.FirstOrDefault(v => v.Value == 1).Key;
            Console.WriteLine($"Location Of Target: {locationOfTarget}");
            Console.WriteLine($"Location Of Start: {locationOfStart}");
            var targetX = int.Parse(locationOfTarget.Split(",")[0]);
            var targetY = int.Parse(locationOfTarget.Split(",")[1]);
            var startX = int.Parse(locationOfStart.Split(",")[0]);
            var startY = int.Parse(locationOfStart.Split(",")[1]);
            var stepsAcross = Math.Abs(targetX - startX);
            var stepsUpDown = Math.Abs(targetY - startY);
            Console.WriteLine($"Steps Across: {stepsAcross}");
            Console.WriteLine($"Steps Up/Down: {stepsUpDown}");
            Console.WriteLine($"Total Steps: {(stepsAcross + stepsUpDown)}");
        }

        private static MoveOutcome GetNextCoordinates(MoveOutcome lastMove, Dictionary<string, int> grid)
        {
            var currentX = lastMove.X;
            var currentY = lastMove.Y;

            switch (lastMove.Direction)
            {
                case Move.First: //Special case for first move.
                    return new MoveOutcome
                    {
                        Direction = Move.Right,
                    };

                case Move.Right:
                    while (true)
                    {
                        return new MoveOutcome
                        {
                            Direction = GetNextDirection(currentX + 1, currentY, lastMove.Direction, grid),
                            X = currentX + 1,
                            Y = currentY
                        };
                    }

                case Move.Up:
                    while (true)
                    {
                        return new MoveOutcome
                        {
                            Direction = GetNextDirection(currentX, currentY + 1, lastMove.Direction, grid),
                            X = currentX,
                            Y = currentY + 1
                        };
                    }
                case Move.Left:
                    while (true)
                    {
                        return new MoveOutcome
                        {
                            Direction = GetNextDirection(currentX - 1, currentY, lastMove.Direction, grid),
                            X = currentX - 1,
                            Y = currentY
                        };
                    }
                case Move.Down:
                    while (true)
                    {
                        return new MoveOutcome
                        {
                            Direction = GetNextDirection(currentX, currentY - 1, lastMove.Direction, grid),
                            X = currentX,
                            Y = currentY - 1
                        };
                    }
            }
            throw new Exception("Invalid Input");
        }

        private static Move GetNextDirection(int x, int y, Move move, Dictionary<string, int> grid)
        {
            switch (move)
            {
                case Move.Right: //Peek at the one above to see if we should switch to Up
                    if (!grid.ContainsKey(GetCoordinate(x, y + 1)))
                    {
                        return Move.Up;
                    }
                    return Move.Right;
                case Move.Up: //Peek at the one Left to see if we should switch to Left
                    if (!grid.ContainsKey(GetCoordinate(x - 1, y)))
                    {
                        return Move.Left;
                    }
                    return Move.Up;
                case Move.Left: //Peek at the one below to see if we should switch to Down
                    if (!grid.ContainsKey(GetCoordinate(x, y - 1)))
                    {
                        return Move.Down;
                    }
                    return Move.Left;
                case Move.Down: //Peek at the one to the right to see if we should switch to Right
                    if (!grid.ContainsKey(GetCoordinate(x + 1, y)))
                    {
                        return Move.Right;
                    }
                    return Move.Down;
            }
            throw new Exception("Invalid Input");
        }

        
        private static string GetCoordinate(int x, int y) => x + "," + y;

        internal enum Move
        {
            Right,
            Up,
            Left,
            Down,
            First,            
        }

        internal class MoveOutcome
        {
            public int X;
            public int Y;
            public Move Direction;
        }
    }
}