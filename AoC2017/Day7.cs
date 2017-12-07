using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace Aoc2017
{
    internal static class Day7
    {
        internal static void Run()
        {
            PartOne(); //azqje, 646
            Console.Read();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"inputs\day7.txt"))
            {
                var tree = new List<TreeNode>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var weight = int.Parse(Regex.Match(line, @"(?<=\().+?(?=\))").Value); // in the brackets
                    var name = line.Split(' ')[0]; //before the first space
                    var children = new List<string>();
                    if (line.Contains("->"))
                    {
                        var childrenLine = line.Split("->")[1].Trim(); //Everything to the right of the arrow, trimmed.
                        childrenLine = childrenLine.Replace(" ", ""); //Remove the spaces
                        children.AddRange(childrenLine.Split(",").ToList()); //Split on the commas to get the child names
                    }
                    tree.Add(new TreeNode
                    {
                        Children = children,
                        Name = name,
                        Weight = weight
                    });
                }
                //Now we have parsed all the input. Recursively fill in the parent details to get the Root node.
                var rootNodeName = FillParents(tree.First(), tree);
                Console.WriteLine(rootNodeName);
                var rootNode = tree.First(n => n.Name == rootNodeName);
                //For Part 2, populate the Child nodes.
                PopulateChilden(tree, rootNode);
                //Then get the unbalanced one.
                FindUnbalancedTree(tree, rootNode);
            }
        }

        public static void PopulateChilden(List<TreeNode> nodes, TreeNode node)
        {
            var children = nodes.Where(n => node.Children.Contains(n.Name)).ToList();
            foreach (var child in children)
            {
                node.ChildNodes = children;
                PopulateChilden(nodes,child);
            }
        }

        private static void FindUnbalancedTree (List<TreeNode> nodes, TreeNode startNode)
        {
            foreach (var node in startNode.ChildNodes)
            {
                Console.WriteLine($"Examining: {node.Name}");
                var groupOfWeights = node.ChildNodes.GroupBy(c => c.GetChildSumWeight());

                //Only continue if there are more than one set of weight values from the child nodes.
                if (groupOfWeights.Count() <= 1) continue; 
                
                var minWeight = node.ChildNodes.Min(c => c.GetChildSumWeight());
                var maxWeight = node.ChildNodes.Max(c => c.GetChildSumWeight());
                var thisNode = node.ChildNodes.First(c => c.GetChildSumWeight() == maxWeight);
                var thisWeight = thisNode.Weight;
                thisNode.Weight -= (maxWeight - minWeight); //Adjust the weight by the difference and attept to balance the tower.
                if (IsBalanced(nodes))
                {
                    Console.WriteLine($"Correct weight: {thisNode.Weight}");
                    break;
                }
                thisNode.Weight = thisWeight; //Didn't work, so put it back how it was and move up the tower.
                FindUnbalancedTree(node.ChildNodes, node);
            }
        }

        private static bool IsBalanced(IEnumerable<TreeNode> nodes)
        {
            foreach (var node in nodes.Where(n => n.ChildNodes.Count > 1))
            {
                var minWeight = node.ChildNodes.Min(c => c.GetChildSumWeight());
                var maxWeight = node.ChildNodes.Max(c => c.GetChildSumWeight());
                if (maxWeight - minWeight != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static string FillParents(TreeNode node, List<TreeNode> nodes)
        {
            var parent = nodes.FirstOrDefault(p => p.Children.Contains(node.Name));
            if (parent == null)
            {
                return node.Name;
            }
            node.Parent = parent;
            return FillParents(parent, nodes);
        }
    }
}

internal class TreeNode
{
    public string Name;
    public int Weight;
    public TreeNode Parent;
    public List<string> Children;
    public List<TreeNode> ChildNodes = new List<TreeNode>();

    public int GetChildSumWeight()
    {
        var sumOfChildren = ChildNodes.Sum(s => s.GetChildSumWeight());
        return Weight + sumOfChildren;
    }
}