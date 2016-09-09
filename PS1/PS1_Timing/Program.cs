using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS1
{
    class Program
    {
        public const int DURATION = 1000;
        public const int STARTING_SIZE = 32;

        static void Main(string[] args)
        {
            // Report the average time required to do a linear search for various sizes
            // of arrays.
            int size = STARTING_SIZE;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 17; i++)
            {
                size = size * 2;
                double currentTime = TimeCGT(size);
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

        public static double TimeCGT(int size)
        {
            // Get the process
            Process p = Process.GetCurrentProcess();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            string[] arr = null;
            do
            {
                arr = CreateRandomArray(size);
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        Calculate.CalculateGrowTime(arr);
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
                arr = CreateRandomArray(size);
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                       // Calculate.CalculateGrowTime(arr);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            }
            while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;
        }

        public static string[] CreateRandomArray(int size)
        {
            Random r = new Random();
            string[] arr = new string[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = r.Next(1000000).ToString();
            }
            return arr;
        }
    }
}

