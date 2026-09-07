namespace Assignment12MemoryOptimization
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
    }
}
