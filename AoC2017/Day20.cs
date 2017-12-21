using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc2017
{
    internal static class Day20
    {
        internal static void Run()
        {
            PartOne(); //344
            PartTwo(); //404
            Console.Read();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"inputs\day20.txt"))
            {
                var particles = new List<Particle>();
                var index = 0;
                while (!reader.EndOfStream)
                {
                    particles.Add(new Particle(index, reader.ReadLine()));
                    index++;
                }
                var closestParticle = particles.OrderBy(p => p.Acceleration).ThenBy(p => p.Velocity).First();
                Console.WriteLine(closestParticle.Id);
            }
        }

        private static void PartTwo()
        {
            using (var reader = File.OpenText(@"inputs\day20.txt"))
            {
                var particles = new List<Particle>();
                var index = 0;
                while (!reader.EndOfStream)
                {
                    particles.Add(new Particle(index, reader.ReadLine()));
                    index++;
                }

                for (var i = 0; i <= 10000; i++) //guess that 10,000 ticks is enough. Seems to be.
                {
                    foreach (var particle in particles)
                    {
                        particle.Tick();
                    }
                    //group them by position
                    var collisions = particles
                        .GroupBy(g => g.Position)
                        .Where(w => w.Count() > 1)
                        .ToDictionary(d => d.Key, d => d.ToList());
                    if (collisions.Any())
                    {
                        foreach (var collision in collisions)
                        {
                            foreach (var match in collision.Value)
                            {
                                particles.Remove(match);
                            }
                        }
                    }
                }
                Console.WriteLine(particles.Count);
            }
        }
    }

    internal class Particle
    {
        public int Id { get; set; }
        public Coord P { get; set; }
        public Coord V { get; set; }
        public Coord A { get; set; }

        public Particle(int id, string input)
        {
            var coords = input.Split("=");
            var pInput = coords[1].Substring(coords[1].IndexOf('<') + 1, coords[1].IndexOf('>') - coords[1].IndexOf('<') - 1);
            var vInput = coords[2].Substring(coords[2].IndexOf('<') + 1, coords[2].IndexOf('>') - coords[2].IndexOf('<') -1 );
            var aInput = coords[3].Substring(coords[3].IndexOf('<') + 1, coords[3].IndexOf('>') - coords[3].IndexOf('<') - 1);
            var pCoords = pInput.Split(',');
            var vCoords = vInput.Split(',');
            var aCoords = aInput.Split(',');
            Id = id;
            P = new Coord
            {
                X = long.Parse(pCoords[0]),
                Y = long.Parse(pCoords[1]),
                Z = long.Parse(pCoords[2]),
            };
            V = new Coord
            {
                X = long.Parse(vCoords[0]),
                Y = long.Parse(vCoords[1]),
                Z = long.Parse(vCoords[2]),
            };
            A = new Coord
            {
                X = long.Parse(aCoords[0]),
                Y = long.Parse(aCoords[1]),
                Z = long.Parse(aCoords[2]),
            };
        }

        public void Tick()
        {
            V.X += A.X;
            V.Y += A.Y;
            V.Z += A.Z;
            P.X += V.X;
            P.Y += V.Y;
            P.Z += V.Z;
        }

        public long Acceleration => (Math.Abs(A.X) + Math.Abs(A.Y) + Math.Abs(A.Z));

        public long Velocity => (Math.Abs(V.X) + Math.Abs(V.Y) + Math.Abs(V.Z));

        public string Position => $"{P.X},{P.Y},{P.Z}";

    }
    
    internal class Coord
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
    }
}
