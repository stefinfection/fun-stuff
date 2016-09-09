using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    class TimingPlantingTrees
    {        
        public const int DURATION = 1000;

        public static void Main(string[] args)
        {
            int size = 32;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 17; i++)
            {
                size = size * 2;
                double currentTime = TimePlantingTrees(size - 1);
                Console.Write((size - 1) + "\t" + currentTime.ToString("G3"));
                if (i > 0)
                {
                    Console.WriteLine("   \t" + (currentTime / previousTime).ToString("G3"));
                }
                else
                {
                    Console.WriteLine();
                }
                previousTime = currentTime;
            }
        }

        public static double TimePlantingTrees(int size)
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
                        CalculateGrowTime(arr);
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
