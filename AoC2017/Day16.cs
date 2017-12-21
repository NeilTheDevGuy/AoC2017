using System;
using System.IO;
using System.Linq;

namespace Aoc2017
{
    internal static class Day16
    {
        public static void Run()
        { //pogbjfihclkemadn (Part 2)
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2017\Aoc2017\AoC2017\Inputs\day16.txt"))
            {
                var moves = reader.ReadToEnd().Split(',');
                var programs = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
                var origPrograms = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
                //For Part 2, repeat x 1 billion, For Part 1 - just run once.
                //That'd take too long, find out when it exactly matches the original state and mod a billion by that.
                //Turns out, that's 60 iterations
                for (var r = 0; r < 1000000000 % 60; r++)
                {
                    foreach (var move in moves)
                    {
                        var progs = move.Substring(1).Split('/');
                        char tmp;
                        switch (move[0])
                        {
                            case 's':
                                var end = int.Parse(move.Substring(1));
                                var newPrograms = new char[16];
                                var newIndex = 0;
                                for (var i = 16 - end; i < 16; i++)
                                {
                                    newPrograms[newIndex] = programs[i];
                                    newIndex++;
                                }
                                for (var i = 0; i < 16 - end; i++)
                                {
                                    newPrograms[newIndex] = programs[i];
                                    newIndex++;
                                }
                                programs = newPrograms;
                                break;

                            case 'x':
                                tmp = programs[int.Parse(progs[0])];
                                programs[int.Parse(progs[0])] = programs[int.Parse(progs[1])];
                                programs[int.Parse(progs[1])] = tmp;
                                break;

                            case 'p':
                                var loc1 = Find(programs, Convert.ToChar(progs[0]));
                                var loc2 = Find(programs, Convert.ToChar(progs[1]));
                                tmp = programs[loc1];
                                programs[loc1] = programs[loc2];
                                programs[loc2] = tmp;
                                break;
                        }
                        if (programs.SequenceEqual(origPrograms))
                        {
                            Console.WriteLine($"Match after {r}");
                        }
                    }
                }
                foreach (var c in programs)
                {
                    Console.Write(c);
                }
            }
            Console.Read();
        }

        private static int Find(char[] programs, char toFind)
        {
            for (var i = 0; i < programs.Length; i++)
            {
                if (programs[i] == toFind)
                {
                    return i;
                }
            }
            throw new Exception("Not Found");
        }
    }
}
