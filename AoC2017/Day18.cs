using System; 
using System.Collections.Generic;
using System.IO;

namespace Aoc2017
{
    internal static class Day18
    {
        private static readonly Dictionary<string, long> _registers = new Dictionary<string, long>(); //PartOne
        private static readonly Dictionary<string, long> _registers0 = new Dictionary<string, long>(); //PartTwo
        private static readonly Dictionary<string, long> _registers1 = new Dictionary<string, long>(); //PartTwo
        private static readonly Queue<long> _queue0 = new Queue<long>();
        private static readonly Queue<long> _queue1 = new Queue<long>();
        private static int _p1Count = 0;
        internal static void Run()
        {
            PartOne(); //1187
            PartTwo(); //5969
            Console.Read();
        }

        private static void PartOne()
        {
           var ins = new Instruction[500];
           long frequency = 0;
           using (var reader = File.OpenText(@"inputs\day18.txt"))
           {
               long index = 0;
               while (!reader.EndOfStream)
               {
                   var line = reader.ReadLine().Split(" ");
                   ins[index] = new Instruction
                   {
                       Action = line[0],
                       Register = line[1],
                       Value = line.Length > 2 ? line[2] : ""
                   };
                   index++;
               }
               index = 0;
               while (index <= ins.Length || index < 0)
               {
                   (index, frequency) = ParseInstruction(ins, index, frequency);
               }
               Console.WriteLine(frequency);
           }
        }

        private static void PartTwo()
        {
            var ins = new Instruction[500];
            using (var reader = File.OpenText(@"inputs\day18.txt"))
            {
                long index0 = 0;
                long index1 = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(" ");
                    ins[index0] = new Instruction
                    {
                        Action = line[0],
                        Register = line[1],
                        Value = line.Length > 2 ? line[2] : ""
                    };
                    index0++;
                }
                SetRegister("p", 0, 0);
                SetRegister("p", 1, 1);
                index0 = 0;
                var wait0 = false;
                var wait1 = false;
                
                while ((index0 <= ins.Length || index1 <= ins.Length))
                {
                    (index0, wait0) = ParseInstructionPartTwo(ins, index0, 0, wait0);
                    if (wait0 && wait1)
                    {
                        break;
                    }
                    
                    (index1, wait1) = ParseInstructionPartTwo(ins, index1, 1, wait1);
                    if (wait0 && wait1)
                    {
                        break;
                    }
                }
                Console.WriteLine(_p1Count);
            }
        }

        private static (long, bool) ParseInstructionPartTwo(Instruction[] instructions, long index, int programId, bool wait)
        {
            var instruction = instructions[index];
            long intVal = 0;
            switch (instruction.Action)
            {
                case "snd":
                    var val = GetRegisterValue(instruction.Register, programId);
                    if (programId == 0)
                    {
                        _queue1.Enqueue(val);
                     }
                    if (programId == 1)
                    {
                        _queue0.Enqueue(val);
                        _p1Count++;
                     }
                    index++;
                    break;
                case "set":
                    var setVal = long.TryParse(instruction.Value, out intVal) ? long.Parse(instruction.Value) : GetRegisterValue(instruction.Value, programId);
                    SetRegister(instruction.Register, setVal, programId);
                    index++;
                    break;
                case "add":
                    var addVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register, programId) + long.Parse(instruction.Value) : GetRegisterValue(instruction.Register, programId) + GetRegisterValue(instruction.Value, programId);
                    SetRegister(instruction.Register, addVal, programId);
                    index++;
                    break;
                case "mul":
                    var mulVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register, programId) * long.Parse(instruction.Value) : GetRegisterValue(instruction.Register, programId) * GetRegisterValue(instruction.Value, programId);
                    SetRegister(instruction.Register, mulVal, programId);
                    index++;
                    break;
                case "mod":
                    var modVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register, programId) % long.Parse(instruction.Value) : GetRegisterValue(instruction.Register, programId) % GetRegisterValue(instruction.Value, programId);
                    SetRegister(instruction.Register, modVal, programId);
                    index++;
                    break;
                case "jgz":
                    var jmpVal = long.TryParse(instruction.Register, out intVal) ? long.Parse(instruction.Register) : GetRegisterValue(instruction.Register, programId);
                    if (jmpVal > 0)
                    {
                        var indVal = long.TryParse(instruction.Value, out intVal) ? long.Parse(instruction.Value) : GetRegisterValue(instruction.Value, programId);
                        index += indVal;
                        break;
                    }
                    index++;
                    break;
                case "rcv":
                    if (programId == 0)
                    {
                        if (_queue0.Count > 0)
                        {
                            long rcvVal = _queue0.Dequeue();
                            SetRegister(instruction.Register, rcvVal, programId);
                            index++;
                            wait = false;
                        }
                        else
                        {
                            wait = true;
                        }
                    }
                    if (programId == 1)
                    {
                        if (_queue1.Count > 0)
                        {
                            long rcvVal = _queue1.Dequeue();
                            SetRegister(instruction.Register, rcvVal, programId);
                            index++;
                            wait = false;
                        }
                        else
                        {
                            wait = true;
                        }
                    }
                    break;
            }
            return (index, wait);
        }

        private static (long, long) ParseInstruction(Instruction[] instructions, long index, long frequency)
        {
            var instruction = instructions[index];
            long intVal = 0;
            switch (instruction.Action)
            {
                case "snd":
                    frequency = GetRegisterValue(instruction.Register);
                    index++;
                    break;
                case "set":
                    var setVal = long.TryParse(instruction.Value, out intVal) ? long.Parse(instruction.Value) : GetRegisterValue(instruction.Value);
                    SetRegister(instruction.Register, setVal);
                    index++;
                    break;
                case "add":
                    var addVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register) + long.Parse(instruction.Value) : GetRegisterValue(instruction.Register) + GetRegisterValue(instruction.Value);
                    SetRegister(instruction.Register, addVal);
                    index++;
                    break;
                case "mul":
                    var mulVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register) * long.Parse(instruction.Value) : GetRegisterValue(instruction.Register) * GetRegisterValue(instruction.Value);
                    SetRegister(instruction.Register, mulVal);
                    index++;
                    break;
                case "mod":
                    var modVal = long.TryParse(instruction.Value, out intVal) ? GetRegisterValue(instruction.Register) % long.Parse(instruction.Value) : GetRegisterValue(instruction.Register) % GetRegisterValue(instruction.Value);
                    SetRegister(instruction.Register, modVal);
                    index++;
                    break;
                case "jgz":
                    var jmpVal = long.TryParse(instruction.Register, out intVal) ? long.Parse(instruction.Register) : GetRegisterValue(instruction.Register);
                    if (jmpVal > 0)
                    {
                        var indVal = long.TryParse(instruction.Value, out intVal) ? long.Parse(instruction.Value) : GetRegisterValue(instruction.Value);
                        index += indVal;
                        break;
                    }
                    index++;
                    break;
                case "rcv":
                    if (GetRegisterValue(instruction.Register) != 0)
                    {
                        index += instructions.Length;
                        break;
                    }
                    index++;
                    break;
            }
            return (index, frequency);
        }

        private static void SetRegister(string register, long value)
        {
            if (_registers.ContainsKey(register))
            {
                _registers[register] = value;
            }
            else
            {
                _registers.Add(register, value);
            }
        }

        private static void SetRegister(string register, long value, int programId)
        {
            if (programId == 0)
            {
                if (_registers0.ContainsKey(register))
                {
                    _registers0[register] = value;
                }
                else
                {
                    _registers0.Add(register, value);
                }
            }
            if (programId == 1)
            {
                if (_registers1.ContainsKey(register))
                {
                    _registers1[register] = value;
                }
                else
                {
                    _registers1.Add(register, value);
                }
            }
        }

        private static long GetRegisterValue(string register)
        {
            if (_registers.ContainsKey(register))
            {
                return _registers[register];
            }
            _registers.Add(register, 0);
            return 0;
        }

        private static long GetRegisterValue(string register, int programId)
        {
            if (programId == 0)
            {
                if (_registers0.ContainsKey(register))
                {
                    return _registers0[register];
                }
                _registers0.Add(register, 0);
                return 0;
            }
            if (_registers1.ContainsKey(register))
                {
                    return _registers1[register];
                }
                _registers1.Add(register, 0);
                return 0;
            }
        }
    
    internal class Instruction
    {
        public string Action {get; set; }
        public string Register { get; set; }
        public string Value { get; set; }
    }
 }
