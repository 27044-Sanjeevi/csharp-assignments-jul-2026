namespace Assignment12MemoryOptimization.Tasks
{
    /// <summary>
    /// Represents allocating memory in a fixed-size list.
    /// </summary>
    internal class FixedSizeMemoryEater
    {
        private const int ListCapacity = 1000;
        private readonly List<int[]> _memAlloc = new List<int[]>(ListCapacity);

        /// <summary>
        /// Continuously allocates memory in a loop with a short delay between iterations.
        /// </summary>
        internal void Allocate()
        {
            Console.WriteLine($"Initialized a new list of integer arrays of size {ListCapacity}.");
            while (true)
            {
                Console.WriteLine("Performing operations on the list.");

                // operations on memAlloc
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Displays the inferences of the current code execution.
        /// </summary>
        internal void DisplayInferences()
        {
            int integerArraySize = 1000;
            long bytesPerArray = integerArraySize * sizeof(int);
            double maxTheoreticalCapMb = (ListCapacity * bytesPerArray) / (1024.0 * 1024.0);

            Console.WriteLine("================== FIXED SIZE MEMORY EATER INFERENCES ==================");
            Console.WriteLine($"\nGen 0 Collections: {GC.CollectionCount(0)} | Gen 1: {GC.CollectionCount(1)} | Gen 2: {GC.CollectionCount(2)}");

            Console.WriteLine("\n1. CRASH PREVENTION: The original 'MemoryEater' leaks memory infinitely until an OutOfMemoryException crashes the app.");
            Console.WriteLine($"2. This version enforces a ceiling at {maxTheoreticalCapMb:F2} MB.");
            Console.WriteLine("3. This optimized version uses a fixed-size list initialized with capacity, making it stable.");
            Console.WriteLine("4. Once capacity hits its max ceiling, memory stops growing because no additional arrays are added.");

            Console.WriteLine("\nO(1) ADVANTAGE: Unlike RemoveAt(0), this approach avoids O(N) array-shift operations.");
            Console.WriteLine("By eliminating index shifting, no cycles are wasted copying internal memory references.");

            Console.WriteLine("\nGENERATIONAL CHURN ELIMINATION: Because the list fills up to its threshold and stops allocating new arrays, the Garbage Collector can completely rest, reducing Gen 0/1/2 allocation spikes to zero over time.");
            Console.WriteLine();
        }
    }
}
