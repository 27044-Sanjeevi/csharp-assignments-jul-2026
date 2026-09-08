namespace Assignment12MemoryOptimization.Tasks
{
    /// <summary>
    /// Continuously allocates memory by adding integer arrays to a collection, leading to high memory usage.usage.
    /// </summary>
    public class MemoryEater
    {
        private readonly List<int[]> _memAlloc = new List<int[]>();

        /// <summary>
        /// Continuously allocates memory by adding arrays of integers to the list.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                this._memAlloc.Add(new int[1000]);
                Console.WriteLine("Added new 1000-element array to the List");

                // operations on memAlloc
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Displays the inferences of the current code execution.
        /// </summary>
        public void DisplayInferences()
        {
            int integerArraySize = 1000;
            long bytesPerArray = integerArraySize * sizeof(int);
            double allocationRatePerSecondMb = (100 * bytesPerArray) / (1024.0 * 1024.0);

            Console.WriteLine("======================= ORIGINAL MEMORY EATER INFERENCES =======================");
            Console.WriteLine($"\nGen 0 Collections: {GC.CollectionCount(0)} | Gen 1: {GC.CollectionCount(1)} | Gen 2: {GC.CollectionCount(2)}");

            Console.WriteLine("\n1. This base code lacks a element limit or upper boundary check.");
            Console.WriteLine($"2. The list is growing at ~{allocationRatePerSecondMb:F2} MB per second.");
            Console.WriteLine("3. Because memory never stops growing, an OutOfMemoryException occurs eventually.");
            Console.WriteLine("4. The List layout is constantly forced to double its internal pointer memory");

            Console.WriteLine("\nBecause the list reference is never cleared, every single 'new int[]' is permanently locked on the managed heap and cannot be collected by the runtime.");
            Console.WriteLine();
        }
    }
}
