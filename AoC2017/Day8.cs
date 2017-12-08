using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Aoc2017
{
    internal static class Day8
    {
        internal static void Run()
        {
            Calculate(); //5102,6056
            Console.Read();
        }

        private static void Calculate()
        {
            using (var reader = File.OpenText(@"inputs\day8.txt"))
            {
                var registers = new Dictionary<string, int>();
                var highestValue = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var instructions = line.Split(" ").ToList();
                    var register = instructions[0];
                    var op = instructions[1];
                    var value = int.Parse(instructions[2]);
                    var conditionalRegister = instructions[4];
                    var condition = instructions[5];
                    var conditionalValue = int.Parse(instructions[6]);

                    //Get the current value of the register in the condition
                    registers.TryGetValue(conditionalRegister, out int currentConditionRegisterValue);
                    
                    //If the condition matches this value, follow the instruction.
                    if (ConditionPasses(currentConditionRegisterValue, conditionalValue, condition))
                    {
                        if (registers.ContainsKey(register))
                        {
                            registers[register] = GetNewRegisterValue(registers[register], value, op != "inc");
                        }
                        else
                        {
                            registers.Add(register, GetNewRegisterValue(0, value, op != "inc"));
                        }
                        if (registers[register] > highestValue)
                        {
                            highestValue = registers[register];
                        }
                    }
                }
                Console.WriteLine(registers.Values.Max(v => v));
                Console.WriteLine(highestValue);
            }
        }

        private static bool ConditionPasses(int registerValue, int conditionalValue, string condition)
        {
            switch (condition)
            {
                case "==": return registerValue == conditionalValue;
                case "!=": return registerValue != conditionalValue;
                case ">=": return registerValue >= conditionalValue;
                case "<=": return registerValue <= conditionalValue;
                case ">": return registerValue > conditionalValue;
                case "<": return registerValue < conditionalValue;
            }
            throw new Exception("Unexpected condition");
        }

        private static int GetNewRegisterValue(int currentValue, int change, bool isDecrement)
        {
            if (isDecrement)
            {
                return currentValue - change;
            }
            return currentValue + change;
        }
    }
}
