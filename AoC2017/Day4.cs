using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace Aoc2017
{
    internal static class Day4
    {
        internal static void Run()
        {
            PartOne();
            PartTwo();
            Console.Read();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"inputs\day4.txt"))
            {
                var count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(" ").ToList();
                    if (!words.GroupBy(w => w).Any(c => c.Count() > 1))
                    {
                        count++;
                    }
                }
                Console.WriteLine(count);
                //383
            }
        }

        private static void PartTwo()
        {
            using (var reader = File.OpenText(@"inputs\day4.txt"))
            {
                var count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(" ").ToList();
                    var sortedList = new List<string>();
                    foreach (var word in words)
                    {
                        var sortedWord = word.ToCharArray();
                        Array.Sort(sortedWord);
                        sortedList.Add(new string(sortedWord));
                    }
                    if (!sortedList.GroupBy(w => w).Any(c => c.Count() > 1))
                    {
                        count++;
                    }
                }
                Console.WriteLine(count);
                //265
            }
        }
    }
}
