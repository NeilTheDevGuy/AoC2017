using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2017.Inputs
{
    internal static class Day15
    {
        internal static void Run()
        {
            PartOne(); //638
            PartTwo(); //343
            Console.Read();
        }

        private static void PartOne()
        {
            Int64 genAVal = 289;
            Int64 genBVal = 629;
            Int64 genAFactor = 16807;
            Int64 genBFactor = 48271;
            Int64 divisor = 2147483647;
            var matches = 0;

            for (var i = 0; i < 40000000; i++)
            {
                genAVal = genAVal * genAFactor % divisor;
                genBVal = genBVal * genBFactor % divisor;
                if ((genAVal & 0xFFFF) == (genBVal & 0xFFFF))
                {
                    matches++;
                }
            }
            Console.WriteLine(matches);
        }

        private static void PartTwo()
        {
            Int64 genAVal = 289;
            Int64 genBVal = 629;
            Int64 genAFactor = 16807;
            Int64 genBFactor = 48271;
            Int64 divisor = 2147483647;
            Queue<Int64> aQueue = new Queue<Int64>();
            Queue<Int64> bQueue = new Queue<Int64>();

            var matches = 0;
            var i = 0;
            while (i < 5000000)
            {
                genAVal = genAVal * genAFactor % divisor;
                genBVal = genBVal * genBFactor % divisor;
                if (genAVal % 4 == 0)
                {
                    aQueue.Enqueue(genAVal & 0xFFFF);
                }
                if (genBVal % 8 == 0)
                {
                    bQueue.Enqueue(genBVal & 0xFFFF);
                }
                if (aQueue.Any() && bQueue.Any())
                {
                    var judgeA = aQueue.Dequeue();
                    var judgeB = bQueue.Dequeue();
                    if (judgeA == judgeB)
                    {
                        matches++;
                    }
                    i++;
                }
            }
            Console.WriteLine(matches);
        }
    }
}

