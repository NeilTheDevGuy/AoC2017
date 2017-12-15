using System;
using System.Collections.Generic;
using System.IO;

namespace Aoc2017
{
    internal static class Day11
    {
        internal static void Run()
        {
            Calculate(); //824, //1548
            Console.Read();
        }

        private static void Calculate()
        {
            using (var reader = File.OpenText(@"inputs\day11.txt"))
            {
                var tiles = new List<Tile>();
                var input = reader.ReadToEnd().Trim().Split(',');
                var x = 0;
                var y = 0;
                var furthestDistance = 0;
                foreach (var move in input)
                {
                    switch (move)
                    {
                        case "n":
                            y+=2;
                            tiles.Add(new Tile {X = x, Y = y});
                            break;
                        case "ne":
                            y++; x++;
                            tiles.Add(new Tile { X = x, Y = y });
                            break;
                        case "se":
                            y--; x++;
                            tiles.Add(new Tile { X = x, Y = y });
                            break;
                        case "s":
                            y-=2;
                            tiles.Add(new Tile { X = x, Y = y });
                            break;
                        case "sw":
                            y--; x--;
                            tiles.Add(new Tile { X = x, Y = y });
                            break;
                        case "nw":
                            y++; x--;
                            tiles.Add(new Tile { X = x, Y = y });
                            break;
                    }
                    furthestDistance = Math.Max(furthestDistance, CalcDistance(x, y));
                }
                var distance = CalcDistance(x, y);
                Console.WriteLine(distance);
                Console.WriteLine(furthestDistance);
            }
        }

        private static int CalcDistance(int x, int y) => Math.Abs(x - 0) + (Math.Abs(y - 0) - Math.Abs(x - 0)) / 2;
        
    }

    internal class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        //public int Z => 0 - (X + Y);
    }
}
