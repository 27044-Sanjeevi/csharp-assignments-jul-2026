using GarbageCollection;

namespace Assignments
{
    internal class Program
    {
        private const int NumberOfObjects = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine("--- TASK 3 : GARBAGE COLLECTION ---");
            Console.ReadKey();
        }

        private void CreateDummyObjects()
        {
            for (int i = 1; i <= NumberOfObjects; i++)
            {
                var dummyObject = new DummyReceipt(i, "Nil");
            }
        }

        private void DisplayMemoryDetails()
        {
            long memoryInBytes = GC.GetTotalMemory(forceFullCollection: false);
        }
    }
}