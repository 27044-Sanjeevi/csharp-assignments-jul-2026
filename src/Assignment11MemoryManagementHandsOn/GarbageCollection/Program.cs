using GarbageCollection;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int NumberOfObjects = 1000000;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.WriteLine("--- TASK 3 : GARBAGE COLLECTION ---");
            Console.WriteLine("\n[STATUS] Initial State at the entry of the program.");
            DisplayMemoryDetails();

            CreateDummyObjects();

            Console.WriteLine($"\n[STATUS] After the creation of {NumberOfObjects} Dummy Objects.");
            DisplayMemoryDetails();

            GC.Collect();
            Console.WriteLine("\n[STATUS] After garbage collection.");
            DisplayMemoryDetails();
            Console.ReadKey();
        }

        private static void CreateDummyObjects()
        {
            var dummyObject = new DummyReceipt(1, "Nil");
            for (int i = 1; i <= NumberOfObjects; i++)
            {
                dummyObject = new DummyReceipt(i, "Nil");
            }
        }

        private static void DisplayMemoryDetails()
        {
            long memoryInBytes = GC.GetTotalMemory(forceFullCollection: false);
            long momoryInKB = memoryInBytes / 1024;
            Console.WriteLine($"Memory Usage: {momoryInKB} KB");
        }
    }
}