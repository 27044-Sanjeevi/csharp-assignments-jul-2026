using System.Diagnostics;
using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Provides methods to perform and benchmark single-threaded and multi-threaded operations on large integer arrays.
    /// </summary>
    internal class Task3Multithreading
    {
        private const int ArraySize = 100_000_000;
        private readonly int[] _array1 = new int[ArraySize];
        private readonly int[] _array2 = new int[ArraySize];

        /// <summary>
        /// Executes single-threaded and multi-threaded operations, measuring and displaying the execution time for each.
        /// </summary>
        public void PerformOperations()
        {
            ConsoleHelpers.DisplaySubtitle("Performing Single-Threaded Operations...");
            this.PopulateArrays();

            Stopwatch stopwatch1 = Stopwatch.StartNew();
            this.PerformSingleThreadedOperations();
            stopwatch1.Stop();

            ConsoleHelpers.DisplaySubtitle("Performing Multi-Threaded Operations...");
            Stopwatch stopwatch2 = Stopwatch.StartNew();
            this.PerformMultiThreadedOperations();
            stopwatch2.Stop();

            this.PrintTime(stopwatch1.ElapsedMilliseconds, "Single-Threaded Operation");
            this.PrintTime(stopwatch2.ElapsedMilliseconds, "Multi-Threaded Operation");
        }

        private void PerformSingleThreadedOperations()
        {
            this.SortArray(this._array1);
            this.IncrementArray(this._array1);
            this.DecrementArray(this._array1);
        }

        private void PerformMultiThreadedOperations()
        {
            Thread sortThread = new Thread(() => this.SortArray(this._array2));
            Thread incrementThread = new Thread(() => this.IncrementArray(this._array2));
            Thread decrementThread = new Thread(() => this.DecrementArray(this._array2));

            sortThread.Start();
            incrementThread.Start();
            decrementThread.Start();

            sortThread.Join();
            incrementThread.Join();
            decrementThread.Join();
        }

        private void PopulateArrays()
        {
            ConsoleHelpers.DisplayStatus("Populating Arrays");
            Random random = new Random();
            for (int i = 0; i < ArraySize; i++)
            {
                this._array1[i] = random.Next(0, 1000);
                this._array2[i] = random.Next(0, 1000);
            }
        }

        private void SortArray(int[] array)
        {
            ConsoleHelpers.DisplayStatus("Sorting Arrays");
            Array.Sort(array);
        }

        private void IncrementArray(int[] array)
        {
            ConsoleHelpers.DisplayStatus("Incrementing Array elements");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] += 10;
            }
        }

        private void DecrementArray(int[] array)
        {
            ConsoleHelpers.DisplayStatus("Decrementing Array elements");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] -= 10;
            }
        }

        private void PrintTime(double time, string operationName)
        {
            Console.WriteLine($"Time Taken for {operationName} = {time} ms");
        }
    }
}
