using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2017
{
    internal static class Day3
    {
        private static Dictionary<string,int> _partOneGrid = new Dictionary<string, int>();
        private static Dictionary<string, int> _partTwoGrid = new Dictionary<string, int>();
        public static void Run()
        {
            PartOne();
            Console.Read();
        }

        private static void PartOne()
        {
            var arraySize = 23;
            var currentCoordinates = "0,1";
            var thisMove = Move.Left; //This and the co-ords above will start "1" at position 0,0
            for (int i = 1; i <= arraySize; i++)
            {
                thisMove = GetNextDirection(thisMove);
                currentCoordinates = GetNextCoordinates(currentCoordinates, thisMove);
                _partOneGrid.Add(currentCoordinates, i);
            }
            var locationOfTarget = _partOneGrid.FirstOrDefault(v => v.Value == arraySize).Key;
            var locationOfStart = _partOneGrid.FirstOrDefault(v => v.Value == 1).Key;
            Console.WriteLine($"Location Of Target: {locationOfTarget}");
            Console.WriteLine($"Location Of Start: {locationOfStart}");
            var targetX = int.Parse(locationOfTarget.Split(",")[0]);
            var targetY = int.Parse(locationOfTarget.Split(",")[1]);
            var startX = int.Parse(locationOfStart.Split(",")[0]);
            var startY = int.Parse(locationOfStart.Split(",")[1]);
            var stepsAcross = targetX - startX;
            var stepsUpDown = targetY - startY;
            Console.WriteLine($"Steps Across: {stepsAcross}");
            Console.WriteLine($"Steps Up/Down: {stepsUpDown}");
            Console.WriteLine($"Total Steps: {stepsAcross + stepsUpDown}");
        }

        private static string GetNextCoordinates(string currentCoordinates, Move thisMove)
        {
            var currentX = int.Parse(currentCoordinates.Split(",")[0]);
            var currentY = int.Parse(currentCoordinates.Split(",")[1]);

            if (_partOneGrid.ContainsKey(GetCoordinate(++currentX, currentY)))

            switch (thisMove)
            {
                case Move.Right:
                    while (true)
                    {
                        if (!_partOneGrid.ContainsKey(GetCoordinate(++currentX, currentY)))
                        {
                            return GetCoordinate(currentX, currentY);
                        }
                    }
                case Move.Up:
                    while (true)
                    {
                        if (!_partOneGrid.ContainsKey(GetCoordinate(currentX, ++currentY)))
                        {
                            return GetCoordinate(currentX, currentY);
                        }

                    }
                case Move.Left:
                    while (true)
                    {
                        if (!_partOneGrid.ContainsKey(GetCoordinate(--currentX, currentY)))
                        {
                            return GetCoordinate(currentX, currentY);
                        }
                    }
                case Move.Down:
                    while (true)
                    {
                        if (!_partOneGrid.ContainsKey(GetCoordinate(currentX, --currentY)))
                        {
                            return GetCoordinate(currentX, currentY);
                        }
                    }
            }
            throw new Exception("Invalid Input");
        }

    private static string GetCoordinate(int x, int y) => x + "," + y;
    

    private static Move GetNextDirection(Move lastMove)
        {
            switch (lastMove)
            {
                case Move.Right:
                    return Move.Up;
                case Move.Up:
                    return Move.Left;
                case Move.Left:
                    return Move.Down;
                case Move.Down:
                    return Move.Right;
            }
            throw new Exception("Invalid input");
        }
    }

    internal enum Move
    {
        Right,
        Up,
        Left,
        Down
    }
}