using System;
using System.Collections.Generic;

namespace Tinkov
{
    public class Programm
    {
        static int count = 0;
        static int maxStages = 0;
        static Stack<List<int[]>> liftStack = new Stack<List<int[]>>();
        static Stack<int> stagesStack = new Stack<int>();
        static Stack<int> countStack = new Stack<int>();
        static Stack<int> indexStack = new Stack<int>();
        public static void Count(string[] ranges)
        {
            string[] line = new string[2];
            List<int[]> lifts = new List<int[]>();
            Array.Sort(ranges);
            for (int i = 0; i < ranges.Length; i++)
            {
                line = ranges[i].Split(' ');
                lifts.Add(new int[] { int.Parse(line[0]), int.Parse(line[1]) });
            }

            for(int i = 0; i < lifts.Count; i++)
            {
                count = 0;
                CountPaths(lifts[i][0], lifts.GetRange(i, lifts.Count-i), ranges, 0);
            }
            Console.WriteLine(maxStages);
        }

        public static void CountPaths(int stage, List<int[]> lifts, string[] ranges, int index)
        {
            RemoveEquals(stage, lifts);
            int newStage = 0;
            for (int j = index; j < lifts.Count; j++)
            { 
                if (lifts[j][0] == stage)
                {
                    newStage = lifts[j][1];
                    liftStack.Push(lifts.GetRange(0, lifts.Count));
                    stagesStack.Push(stage);
                    countStack.Push(count);
                    lifts.Remove(lifts[j]);
                    count++;
                    indexStack.Push(j);
                    CountPaths(newStage, lifts, ranges, 0);
                }
            }
            if (maxStages < count)
            {
                maxStages = count;
            }
            if (liftStack.Count != 0)
            {
                count = countStack.Pop();
                CountPaths(stagesStack.Pop(), liftStack.Pop(), ranges, indexStack.Pop()+1);
            }
        }

        public static void RemoveEquals(int stage, List<int[]> lifts)
        {
            for (int i = 0; i < lifts.Count; i++)
            {
                if (stage == lifts[i][0] && stage == lifts[i][1])
                {
                    count++;
                    lifts.Remove(lifts[i]);
                    i = -1;
                }
            }
        }
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            if (n < 1 || n > Math.Pow(10, 5))
            {
                throw new ArgumentOutOfRangeException("incorrect lifts number");
            }

            var ranges = new string[n];
            string[] temp = new string[2];
            for (int i = 0; i < n; i++)
            {
                ranges[i] = Console.ReadLine();
                temp = ranges[i].Split(' ');
                if (int.Parse(temp[0]) < 0 || int.Parse(temp[0]) > int.Parse(temp[1]) || int.Parse(temp[1]) > Math.Pow(10, 9))
                {
                    throw new ArgumentOutOfRangeException("incorrect line");
                }
            }
            Count(ranges);
        }
    }
}
