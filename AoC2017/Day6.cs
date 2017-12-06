using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace Aoc2017
{
    internal static class Day6
    {
        internal static void Run()
        {
            Calculate(); //14029, 2765
            Console.Read();
        }

        private static void Calculate()
        {
            using (var reader = File.OpenText(@"inputs\day6.txt"))
            {
                var count = 0;
                var input = reader.ReadToEnd().Split(",");
                var variations = new List<string>();
                var banks = new Dictionary<int, int>();
                for (var i = 0; i < input.Length; i++)
                {
                    banks.Add(i, int.Parse(input[i]));
                }

                while (!variations.Contains(GetVariation(banks)))
                {
                    variations.Add(GetVariation(banks));
                    count++;
                    var highestBank = banks.First(b => b.Value == banks.Values.Max());
                    banks[highestBank.Key] = 0;
                    var index = highestBank.Key + 1;
                    for (var i = 1; i <= highestBank.Value; i++)
                    {
                        index = index >= banks.Count ? 0 : index;
                        banks[index]++;
                        index++;
                    }
                }
                Console.WriteLine(count); //Part One
                var firstState = variations.IndexOf(GetVariation(banks));
                Console.WriteLine(count - firstState); //Part Two
            }
        }

        
        private static string GetVariation(Dictionary<int, int> banks) 
            => banks.Values.Aggregate("", (current, value) => current + (value.ToString() + ","));
        
    }
}
