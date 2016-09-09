using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS1
{
    public class Calculate
    {
        static void Main(string[] args)
        {
            //CalculateGrowTime(args);
        }

        public static void CalculateGrowTime(string[] inputArray)
        {
            // Take in input from console, parse as int
            //int numSeedlings;
            //Int32.TryParse(Console.ReadLine(), out numSeedlings);

            //// Take in next line and populate array with those inputs
            //List<string> stringArr = Console.ReadLine().Split(' ').ToList();
            //List<int> arr = new List<int>(stringArr.Count);

            List<string> stringArr = new List<String>(inputArray);
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
            //Console.WriteLine(totalTime + 2);
        }
    }
}
