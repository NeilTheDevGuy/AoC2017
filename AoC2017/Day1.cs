using System;
using System.IO;

namespace Aoc2017
{
    internal static class Day1
    {
        internal static void Run()
        {
            using (var reader = File.OpenText(@"inputs\day1.txt"))
            {
                var inputDigits = reader.ReadToEnd();
                WriteSum(inputDigits, 1);
                WriteSum(inputDigits, inputDigits.Length / 2);
            }
            Console.Read();
        }
        private static void WriteSum(string input, int offset)
        {
            var sum = 0;

            for (var i = 0; i <= input.Length - 1; i++)
            {
                var digitToCompare = i + offset < input.Length ? i + offset : i + offset - input.Length;
                if (input[i] == input[digitToCompare])
                {
                    sum += int.Parse(input[i].ToString());
                }
            }

            Console.WriteLine(sum);
        }
    }
}
