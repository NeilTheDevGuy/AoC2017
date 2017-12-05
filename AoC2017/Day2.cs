using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aoc2017
{
    internal static class Day2
    {
        public static void Run()
        {
            PartOne();
            PartTwo();
            Console.Read();
        }

        private static void PartOne()
        {
            Console.WriteLine("PART ONE");
            using (var reader = File.OpenText(@"inputs\day2.txt"))
            {
                var sumOfDifference = 0;
                while (!reader.EndOfStream)
                {
                    var sheet = reader.ReadLine();
                    var line = sheet.Split("\t");
                    var highest = 0;
                    var lowest = Int32.MaxValue;
                    foreach (var cell in line)
                    {
                        var thisValue = int.Parse(cell);
                        if (thisValue > highest)
                        {
                            highest = thisValue;
                        }
                        if (thisValue < lowest)
                        {
                            lowest = thisValue;
                        }
                    }
                    sumOfDifference += highest - lowest;
                    Console.WriteLine($"Highest: {highest}, Lowest: {lowest}, Difference: {highest - lowest}, Sum: {sumOfDifference}");
                }
                Console.WriteLine(sumOfDifference);
            }
            //45972
        }

        private static void PartTwo()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("PART TWO");
            using (var reader = File.OpenText(@"inputs\day2.txt"))
            {
                var sumOfDivisors = 0;
                while (!reader.EndOfStream)
                {
                    var sheet = reader.ReadLine();
                    var line = sheet.Split("\t");
                    var gotLineValue = false;
                    foreach (var outerCell in line)
                    {
                        var outerValue = int.Parse(outerCell);
                        foreach (var innerCell in line)
                        {
                            var innerValue = int.Parse(innerCell);
                            if (innerValue != outerValue)
                            {
                                if (outerValue % innerValue == 0)
                                {
                                    sumOfDivisors += outerValue / innerValue;
                                    Console.WriteLine($"OuterValue: {outerValue}, InnerValue: {innerValue}, SumOfDivisors: {sumOfDivisors}");
                                    gotLineValue = true;
                                }
                                else if (innerValue % outerValue == 0)
                                {
                                    sumOfDivisors += innerValue / outerValue;
                                    Console.WriteLine($"OuterValue: {outerValue}, InnerValue: {innerValue}, SumOfDivisors: {sumOfDivisors}");
                                    gotLineValue = true;
                                }
                            }
                            if (gotLineValue)
                            {
                                break;
                            }
                        }
                        if (gotLineValue)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine(sumOfDivisors);
                //326
            }
        }
    }
}
