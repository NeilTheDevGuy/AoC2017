using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aoc2017
{
    internal static class Day19
    {
        internal static void Run()
        {
            Route(); //GPALMJSOY, 16204
            Console.Read();
        }

        private static void Route()
        {
            using (var reader = File.OpenText(@"inputs\day19.txt"))
            {
                var diagram = new string[10000];
                var lineIndex = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    diagram[lineIndex] = line;
                    lineIndex++;
                }
                var y = 1;
                var x = diagram[0].IndexOf('|');
                var direction = Direction.Down;
                var cont = true;
                var letters = "";
                var moves = 0;
                while (cont)
                {
                    moves++;
                    var thisChar = diagram[y][x];
                    switch (thisChar)
                    {
                        case '|':
                        case '-':
                            if (direction == Direction.Down)
                            {
                                y++;
                            }
                            if (direction == Direction.Up)
                            {
                                y--;
                            }
                            if (direction == Direction.Right)
                            {
                                x++;
                            }
                            if (direction == Direction.Left)
                            {
                                x--;
                            }
                            break;
                        case ' ':
                            cont = false;
                            break;
                        case '+':
                        {
                            direction = GetNextDirection(direction, x, y, diagram);
                            if (direction == Direction.Down) y++;
                            if (direction == Direction.Up) y--;
                            if (direction == Direction.Right) x++;
                            if (direction == Direction.Left) x--;
                            break;
                        }
                        default:
                        {
                            letters += thisChar;
                            if (direction == Direction.Down) y++;
                            if (direction == Direction.Up) y--;
                            if (direction == Direction.Right) x++;
                            if (direction == Direction.Left) x--;
                            }
                            break;
                    }
                }
                Console.WriteLine(letters);
                Console.WriteLine(moves);
            }
        }

        private static Direction GetNextDirection(Direction currentDirection, int x, int y, string[] diagram)
        {
            if (currentDirection == Direction.Left || currentDirection == Direction.Right)
            {
                if (diagram[y - 1][x] != ' ')
                {
                    return Direction.Up;
                }
                if (diagram[y + 1][x] != ' ')
                {
                    return Direction.Down;
                }
            }
            if (currentDirection == Direction.Up || currentDirection == Direction.Down)
            {
                if (diagram[y][x + 1] != ' ')
                {
                    return Direction.Right;
                }
                if (diagram[y][x - 1] != ' ')
                {
                    return Direction.Left;
                }
            }
            throw new Exception("Invalid Input");
        }
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
