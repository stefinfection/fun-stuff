using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    public static class PlantingTrees
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started Program woohoo.");
        }

        public static void CalculateGrowTime(string[] array)
        {
            // Take in input from console, parse as int
            int numSeedlings;
            Int32.TryParse(Console.ReadLine(), out numSeedlings);

            // Take in next line and populate array with those inputs
            List<string> stringArr = Console.ReadLine().Split(' ').ToList();
            List<int> arr = new List<int>(stringArr.Count);

            // Copy string array to int array to allow for sorting
            int temp = 0;
            foreach (string s in stringArr)
            {
                Int32.TryParse(s, out temp);
                arr.Add(temp);
            }

            // Sort list via library fxn (ascending)
            arr.Sort();

            // Initialize total time at longest time period
            int totalTime = arr[arr.Count - 1];

            // Iterate backwards through list and compute total days          
            for (int i = arr.Count - 2; i >= 0; i--)
            {
                int prev = arr[i + 1];
                int curr = arr[i];
                int diff = prev - (curr + 1);

                // If our current time period extends beyond the shadow of the previous, add those days
                if (diff <= 0)
                {
                    totalTime += Math.Abs(diff);
                }
                // Otherwise, add shadow to the current value to continue overhang
                else
                {
                    arr[i] += diff;
                }
            }

            // Output total to console
            Console.WriteLine(totalTime + 2);
        }
    }

    class PlantingTreesTimer
    {
        public const int DURATION = 1000;

        private static double TimePlantingTrees(int size)
        {
            // Get the process
            Process p = Process.GetCurrentProcess();

            // Create random array of strings to feed in
            string[] arr = CreateRandomArray(size);

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            }
            while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / size;

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        //LinearSearch(data, d);

                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;

            return 0;
        }

        public static string[] CreateRandomArray(int size)
        {
            return null;
        }
    }
}
