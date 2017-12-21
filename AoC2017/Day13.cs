using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc2017
{
    internal static class Day13
    {
        internal static void Run()
        {
            PartOne(); //1612
            PartTwo(); //3907994
            Console.Read();
        }

        private static void PartOne()
        {
            var severity = 0;
            var lines = new List<string>();
            using (var reader = File.OpenText(@"inputs\day13.txt"))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }
            var firewall = GetFirewall(lines);
            for (var i = 0; i < firewall.Keys.Max(); i++)
            {
                severity += GetSeverity(firewall, i);
                MoveScanners(firewall, i + 1);
            }
            Console.WriteLine(severity);
        }

        private static void PartTwo()
        {
            var lines = new List<string>();
            using (var reader = File.OpenText(@"inputs\day13.txt"))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            var delay = 0;
            var caught = true;
            while (caught)
            {
                caught = false;
                var firewall = GetFirewall(lines);
                //Move the scanners on by the number of delays:
                MoveScanners(firewall, delay);

                //Now check if we can get through without being caught.
                for (var i = 0; i <= firewall.Keys.Max(); i++)
                {
                    if (CheckCaught(firewall, i))
                    {
                        caught = true;
                        delay++;
                        break;
                    }
                    MoveScanners(firewall, delay + i + 1);
                }
            }
            Console.WriteLine(delay);
            
        }

        private static int GetSeverity(Dictionary<int, FirewallLayer> firewall, int layer)
        {
            
            if (!firewall[layer].IsEmpty && firewall[layer].ScannerPosition == 0)
            {
                Console.WriteLine($"Caught on Layer {layer}. Range: {firewall[layer].Range}. Sum: {layer * firewall[layer].Range}");
                return layer * firewall[layer].Range;
            }
            return 0;
        }

        private static bool CheckCaught(Dictionary<int, FirewallLayer> firewall, int layer)
        {
            if (firewall[layer].ScannerPosition == 0)
            {
              return true;
            }
            return false;
        }

        private static void MoveScanners(Dictionary<int, FirewallLayer> firewall, int delay)
        {
            foreach (var layer in firewall)
            {
               layer.Value.SetScanner(delay);
            }
        }

        private static Dictionary<int, FirewallLayer> GetFirewall(List<string> input)
        {
            var fw = new Dictionary<int, FirewallLayer>();
            foreach (var thisLine in input)
            {
                var line = thisLine.Split(":");
                var layer = new FirewallLayer
                {
                    IsEmpty = false,
                    Range = int.Parse(line[1]),
                    ScannerPosition = 0
                };
                fw.Add(int.Parse(line[0]), layer);
            }
            FleshOutFirewall(fw);
            return fw;
        }

        private static void FleshOutFirewall(Dictionary<int, FirewallLayer> firewall)
        {
            var count = firewall.Keys.Max();
            for (var i = 0; i <= count; i++)
            {
                if (!firewall.ContainsKey(i))
                {
                    firewall.Add(i, new FirewallLayer
                    {
                        IsEmpty = true,
                        Range = 0,
                        ScannerPosition = -1
                    });
                }
            }
        }
    }

    internal class FirewallLayer
    {
        public bool IsEmpty { get; set; }
        public int ScannerPosition { get; set; }
        public int Range { get; set; }

        public void SetScanner(int delay)
        {
            if (!IsEmpty)
            {
                var length = Range * 2 - 2;
                ScannerPosition = delay % length;
                if (ScannerPosition >= Range)
                {
                    ScannerPosition = Range - 2 - ScannerPosition % Range;
                }
            }
        }
    }
}
