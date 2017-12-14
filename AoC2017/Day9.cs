using System;
using System.IO;

namespace Aoc2017
{
    internal static class Day9
    {
        internal static void Run()
        {
            PartOne(); //20530
            PartTwo(); //9978
            Console.Read();
        }

        private static void PartOne()
        {
            using (var reader = File.OpenText(@"inputs\day9.txt"))
            {
                var input = reader.ReadToEnd();
                input = RemoveGarbage(input);
                //Remove the garbage to get a more sanitised input.
                
                //Remove the commas - not needed (although can leave them in - doesn't affect the outcome).
                input = input.Replace(",", "");
                
                //Left now with just the {}
                var i = 0;
                var groupScore = 0;
                var totalScore = 0;
                while (i <= input.Length - 1)
                {
                    var c = input[i];
                    switch (c)
                    {
                        case '{':
                            groupScore++;
                            i++;
                            break;
                        case '}':
                            totalScore += groupScore;
                            groupScore--;
                            i++;
                            break;
                        default:
                            i++;
                            break;
                    }
                }
                Console.WriteLine(totalScore);
            }
        }

        private static string RemoveGarbage(string input)
        {
            var garbageStartPos = 0;
            var garbageEndPos = 0;
            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] == '<' && garbageStartPos == 0 && IsValidCharacter(input, i))
                {
                    garbageStartPos = i;
                    garbageEndPos = i;
                }
                if (input[i] == '>' && IsValidCharacter(input, i))
                {
                    garbageEndPos = i;
                }
                if (garbageStartPos > 0 && garbageEndPos > garbageStartPos)
                {
                    input = input.Remove(garbageStartPos, garbageEndPos - garbageStartPos + 1);
                    i = 1; //Start from the beginning again as the string has now shrunk.
                    garbageStartPos = 0;
                    garbageEndPos = 0;
                }
            }
            return input;
        }
        
        private static bool IsValidCharacter(string input, int startPos)
        {
            //Check back for ! characters to work out if it should be ignored or not.
            if (input[startPos] == '!')
            {
                return false;
            }
            if (input[startPos - 1] != '!')
            {
                return true;
            }
            var countOfChars = 1;
            var thisChar = '!';
            while (thisChar == '!')
            {
                thisChar = input[startPos - 1 - countOfChars];
                if (thisChar == '!')
                {
                    countOfChars++;
                }
            }
            return countOfChars % 2 == 0; //If it's divisible by 2, then all the ! cancel each other out.
        }

        private static void PartTwo()
        {
            var garbageCount = 0;
            using (var reader = File.OpenText(@"inputs\day9.txt"))
            {
                var input = reader.ReadToEnd();
                //var input = "<{oli!a,<{i<a>"; //Should be 10 on this test input
                var partOfGarbage = false;
                var skipNextChar = false;

                foreach (var thisChar in input)
                {
                    if (skipNextChar)
                    {
                        skipNextChar = false;
                        continue;
                    }
                    if (thisChar == '!')
                    {
                        skipNextChar = true;
                        continue;
                    }
                    if (thisChar == '<')
                    {
                        if (partOfGarbage == false)
                        {
                            partOfGarbage = true;
                            continue;
                        }
                    }
                    if (thisChar == '>')
                    {
                        partOfGarbage = false;
                        continue;
                    }
                    if (partOfGarbage)
                    {
                        garbageCount++;
                    }
                }
            }
            Console.WriteLine(garbageCount); 
        }
    }
}
