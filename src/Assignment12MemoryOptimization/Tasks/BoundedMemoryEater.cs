using System.Diagnostics;

namespace Assignment12MemoryOptimization.Tasks
{
    /// <summary>
    /// Optimizes memory allocation by maintaining a list of integer arrays with a maximum count.
    /// </summary>
    internal class BoundedMemoryEater
    {
        private const int MaxListCount = 100000;
        private const int IntegerArraySize = 1000;
        private const int SleepTimeForAdditionInMs = 1;

        private readonly List<int[]> _memAlloc = new List<int[]>();

        /// <summary>
        /// Continuously allocates a new integer array and adds it to the memory allocation list, removes the first element if the maximum count is reached.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                if (this._memAlloc.Count >= MaxListCount)
                {
                    this._memAlloc.RemoveAt(0);
                    Console.WriteLine("Max list count reached. Removed the first element.");
                }

                this._memAlloc.Add(new int[IntegerArraySize]);
                Console.WriteLine($"Added an array of size {IntegerArraySize} to the list.");

                // operations with memAlloc
                Thread.Sleep(SleepTimeForAdditionInMs);
            }
        }

        /// <summary>
        /// Analyzes and displays memory inferences of the current code execution.
        /// </summary>
        public void DisplayInferences()
        {
            long bytesPerArray = IntegerArraySize * sizeof(int);
            double maxTheoreticalCapMb = (MaxListCount * bytesPerArray) / (1024.0 * 1024.0);

            Console.WriteLine("================== BOUNDED MEMORY EATER INFERENCES ==================");
            Console.WriteLine("\n1. CRASH PREVENTION: The original 'MemoryEater' leaks memory infinitely until an OutOfMemoryException crashes the app.");
            Console.WriteLine($"2. This version enforces a ceiling at {maxTheoreticalCapMb:F2} MB.");
            Console.WriteLine("3. This optimized version uses a sliding window boundary, making it stable.");
            Console.WriteLine("4. Once capacity hits its max ceiling, memory stops growing.");

            Console.WriteLine("\nO(N) operation: List.RemoveAt(0) forces an internal memory copy array-shift operation.");
            Console.WriteLine("For 100,000 pointers, this forces the CPU to shift 99,999 memory references on every loop iteration.");

            Console.WriteLine("\nGENERATIONAL CHURN: Continuously calling 'new int[]' pushes active objects into Gen 1/2.");
            Console.WriteLine("Even though the count is capped, the GC must work constantly to clean up arrays.");
            Console.WriteLine();
        }
    }
}
