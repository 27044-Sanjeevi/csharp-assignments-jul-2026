using Assignment12MemoryOptimization;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            MemoryEater eater = new MemoryEater();
            BoundedMemoryEater boundedMemoryEater = new BoundedMemoryEater();
            // eater.Allocate();
            boundedMemoryEater.Allocate();

            Console.ReadLine();
        }
    }
}