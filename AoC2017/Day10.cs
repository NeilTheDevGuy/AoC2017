using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aoc2017
{
    internal static class Day10
    {
        internal static void Run()
        {
            PartOne(); //11375
            PartTwo(); //e0387e2ad112b7c2ef344e44885fe4d8
            Console.Read();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"inputs\day10.txt"))
            {
                const int listSize = 256;
                var list = Enumerable.Range(0, listSize).ToArray();
                var lengths = reader.ReadToEnd().Split(',');
                var currentPosition = 0;
                var skipSize = 0;
                foreach (var length in lengths)
                {
                    if (int.Parse(length) > 1)
                    {
                        var section = new int[int.Parse(length)];
                        var sectionIndex = 0;
                        var listIndex = currentPosition;
                        for (var x = 0; x < int.Parse(length); x++)
                        {
                            if (listIndex >= listSize)
                            {
                                listIndex = 0;
                            }
                            section[sectionIndex] = list[listIndex];
                            sectionIndex++;
                            listIndex++;
                        }
                        var reversedArray = section.Reverse().ToArray();
                        var reversedIndex = 0;
                        listIndex = currentPosition;
                        for (var i = 0; i < int.Parse(length); i++)
                        {
                            if (listIndex >= listSize)
                            {
                                listIndex = 0;
                            }
                            list[listIndex] = reversedArray[reversedIndex];
                            reversedIndex++;
                            listIndex++;
                        }
                    }
                    currentPosition += int.Parse(length) + skipSize;
                    if (currentPosition >= listSize)
                    {
                        currentPosition -= listSize;
                    }
                    skipSize++;
                }
                Console.WriteLine(list[0] * list[1]);
            }
        }

        private static void PartTwo()
        {
            using (var reader = File.OpenText(@"inputs\day10.txt"))
            {
                var input = reader.ReadToEnd().Trim();
                const int listSize = 256;
                var list = Enumerable.Range(0, listSize).ToArray();
                var lengths = input.Select(x => (int) x).ToList();
                lengths = lengths.Concat(new int[] {17, 31, 73, 47, 23}).ToList();

                var currentPos = 0;
                var skipSize = 0;

                for (var i = 0; i < 64; i++)
                {
                    foreach (var length in lengths)
                    {
                        var section = new int[length];
                        for (var x = 0; x < length; x++)
                        {
                            section[x] = list[(currentPos + x) % listSize];
                        }

                        section = section.Reverse().ToArray();

                        for (var x = 0; x < length; x++)
                        {
                            list[(currentPos + x) % listSize] = section[x];
                        }
                        currentPos += length + skipSize++;
                        currentPos %= listSize;
                    }
                }

                var denseHash = new List<int>();
                for (var i = 0; i < listSize; i += 16)
                {
                    var hash = 0;

                    for (var x = 0; x < 16; x++)
                    {
                        hash ^= list[x + i];
                    }
                    denseHash.Add(hash);
                }

                var result = string.Join(string.Empty, denseHash.Select(x => x.ToString("x2")));
                Console.WriteLine(result);
            }
        }
    }
}
