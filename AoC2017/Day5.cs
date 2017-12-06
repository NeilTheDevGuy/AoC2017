using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aoc2017
{
    internal static class Day5
    {
        internal static void Run()
        {
            FindExit(i => i + 1); //376976
            FindExit(i => i >= 3 ? i - 1 : i + 1); //29227751
            Console.Read();
        }
        private static void FindExit(Func<int, int> incrementor)
        {
            using (var reader = File.OpenText(@"inputs\day5.txt"))
            {
                var count = 0;
                var input = reader.ReadToEnd().Split("\n");
                var index = 0;
                while (true)
                {
                    count++;
                    var instruction = int.Parse(input[index]);
                    var alteredInstruction = incrementor(instruction);
                    input[index] = alteredInstruction.ToString();
                    index += instruction;
                    if (index >= input.Length)
                    {
                        break;
                    }
                }
                Console.WriteLine(count);
            }
        }
    }
}
