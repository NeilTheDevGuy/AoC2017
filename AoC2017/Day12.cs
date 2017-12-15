using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Aoc2017
{
    internal static class Day12
    {
        internal static void Run()
        {
            PartOne(); //175
            PartTwo(); //213
            Console.Read();
        }

        private static void PartOne()
        {
            var programs = new Dictionary<int, Prog>();
            using (var reader = File.OpenText(@"inputs\day12.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split("<->");
                    var prog = new Prog
                    {
                        Id = int.Parse(line[0].Trim()),
                        Connections = new List<int>(line[1].Split(", ").Select(int.Parse))
                    };
                    programs.Add(prog.Id, prog);
                }

                var count = FindConnections(programs[0].Connections, new List<int> {0}, programs, 1);
                Console.WriteLine(count);
            }
        }


        private static int FindConnections(List<int> connections, List<int> usedConnections, Dictionary<int, Prog> programs, int count)
        {
            foreach (var connection in connections)
            {
                if (usedConnections.Contains(connection))
                {
                    continue;
                }
                count++;
                usedConnections.Add(connection);
                var thisProg = programs[connection];
                count = FindConnections(thisProg.Connections, usedConnections, programs, count);
            }
            return count;
        }

        private static void PartTwo()
        {
            var programs = new Dictionary<int, Prog>();
            using (var reader = File.OpenText(@"inputs\day12.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split("<->");
                    var prog = new Prog
                    {
                        Id = int.Parse(line[0].Trim()),
                        Connections = new List<int>(line[1].Split(", ").Select(int.Parse))
                    };
                    programs.Add(prog.Id, prog);
                }

                var groups = new List<Group>();
                foreach (var prog in programs)
                {
                    var thisGroup = new Group
                    {
                        ProgramIds = FindConnections(prog.Value.Connections, new List<int> {}, programs)
                    };
                    var groupExists = false;
                    foreach (var group in groups)
                    {
                        if (group.ProgramIds.Intersect(thisGroup.ProgramIds).Any())
                        {
                            groupExists = true;
                            break;
                        }
                    }
                    if (!groupExists)
                    {
                        groups.Add(thisGroup);
                    }
                }
                Console.WriteLine(groups.Count);
            }
        }
        private static List<int> FindConnections(List<int> connections, List<int> usedConnections, Dictionary<int, Prog> programs)
        {
            foreach (var connection in connections)
            {
                if (usedConnections.Contains(connection))
                {
                    continue;
                }
                usedConnections.Add(connection);
                var thisProg = programs[connection];
                usedConnections = FindConnections(thisProg.Connections, usedConnections, programs);
            }
            return usedConnections;
        }

    }

    internal class Prog
    {
        public int Id { get; set; }
        public List<int> Connections = new List<int>();
    }

    internal class Group
    {
        public List<int> ProgramIds = new List<int>();
    }
}
