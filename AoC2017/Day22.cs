using System;
using System.Collections.Generic;
using System.IO;

namespace Aoc2017
{
    internal static class Day22
    {
        internal static void Run()
        {
            PartOne(); //5339
            PartTwo(); //2512380
            Console.Read();
        }

        private static void PartOne()
        {
            var input = new List<string>();
            using (var reader = File.OpenText(@"inputs\day22.txt"))
            {
                while (!reader.EndOfStream)
                {
                    input.Add(reader.ReadLine());
                }
            }
            var grid = BuildGrid(input);
            var node = grid["0,0"];
            var dir = Direction.Up;
            var infectionCount = 0;
            for (var i = 0; i < 10000; i++)
            {
                dir = GetDirection(dir, grid[node.Pos].State);
                if (grid[node.Pos].State == NodeState.Infected)
                {
                    grid[node.Pos].State = NodeState.Clean;
                }
                else
                {
                    grid[node.Pos].State = NodeState.Infected;
                    infectionCount++;
                }
                node = MoveNode(node, dir, grid);
            }
            Console.WriteLine(infectionCount);
        }

        private static void PartTwo()
        {
            var input = new List<string>();
            using (var reader = File.OpenText(@"inputs\day22.txt"))
            {
                while (!reader.EndOfStream)
                {
                    input.Add(reader.ReadLine());
                }
            }
            var grid = BuildGrid(input);
            var node = grid["0,0"];
            var dir = Direction.Up;
            var infectionCount = 0;
            for (int i = 0; i < 10000000; i++)
            {

                dir = GetDirection(dir, grid[node.Pos].State);
                grid[node.Pos].State = GetNodeState(grid[node.Pos].State);
                if (grid[node.Pos].State == NodeState.Infected)
                {
                    infectionCount++;
                }
                node = MoveNode(node, dir, grid);
            }
            Console.WriteLine(infectionCount);
        }

        private static Direction GetDirection(Direction currentDirection, NodeState state)
        {
            switch (state)
            {
                case NodeState.Clean:
                    return TurnLeft(currentDirection);
                case NodeState.Infected:
                    return TurnRight(currentDirection);
                case NodeState.Flagged:
                    return ReverseDirection(currentDirection);
                case NodeState.Weakend:
                    return currentDirection;
            }
            throw new Exception("Invalid direction");
        }

        private static Direction TurnLeft(Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Up;
            }
            throw new Exception("Invalid Direction");
        }

        private static Direction TurnRight(Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
            }
            throw new Exception("Invalid Direction");
        }

        private static Direction ReverseDirection(Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
            }
            throw new Exception("Invalid Direction");
        }

        private static NodeState GetNodeState(NodeState state)
        {
            switch (state)
            {
                case NodeState.Clean:
                    return NodeState.Weakend;
                case NodeState.Weakend:
                     return NodeState.Infected;
                case NodeState.Infected:
                    return NodeState.Flagged;
                case NodeState.Flagged:
                    return NodeState.Clean;
            }
            throw new Exception("Invalid State");
        }

        private static Node MoveNode(Node node, Direction dir, Dictionary<string, Node> grid)
        {
            switch (dir)
            {
                case Direction.Up:
                    node.Y--;
                    break;
                case Direction.Left:
                    node.X--;
                    break;
                case Direction.Down:
                    node.Y++;
                    break;
                case Direction.Right:
                    node.X++;
                    break;
            }
            if (!grid.ContainsKey(node.Pos))
            {
                var newNode = new Node {X = node.X, Y = node.Y, State = NodeState.Clean};
                grid.Add(newNode.Pos, newNode);
            }
            return node;
        }

        private static Dictionary<string, Node> BuildGrid(List<string> input)
        {
            var grid = new Dictionary<string, Node>();

            var height = -12; //-12, 25 deep
            foreach (var line in input)
            {
                var width = -12; //-12, 25 deep    
                foreach (var node in line)
                {
                    var thisNode = new Node
                    {
                        X = width,
                        Y = height,
                        State = node == '.' ? NodeState.Clean : NodeState.Infected
                    };
                    grid.Add(thisNode.Pos, thisNode);
                    width++;
                }
                height++;
            }
            return grid;
        }
    }

    internal class Node
    {
        public long X { get; set; }
        public long Y { get; set; }
        public string Pos => X + "," + Y;
        public NodeState State; 
    }

    internal enum NodeState
    {
        Clean,
        Weakend,
        Infected,
        Flagged
    }
}

