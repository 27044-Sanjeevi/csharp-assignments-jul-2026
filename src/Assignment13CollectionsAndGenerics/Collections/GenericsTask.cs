namespace Assignment13CollectionsAndGenerics
{
    /// <summary>
    /// Represents task 5 demonstrating the use of generic collections.
    /// </summary>
    internal class GenericsTask
    {
        /// <summary>
        /// Displays the generic collection implementation details.
        /// </summary>
        public void PerformOperations()
        {
            ConsoleHelpers.DisplayTitle("TASK 5 : APPLYING GENERICS");
            ConsoleHelpers.DisplayStatus("Task 1 to Task 4 originally used concrete collection types.");
            Console.WriteLine("Previous implementation : Commit 32519cb");
            Console.WriteLine("\nCollections refactored to use generic types:\n");
            Console.WriteLine("List<string> to List<T>");
            Console.WriteLine("Stack<char> to Stack<T>");
            Console.WriteLine("Queue<string> to Queue<T>");
            Console.WriteLine("Dictionary<string, int> to Dictionary<TKey, TValue>");

            Console.WriteLine();

            ConsoleHelpers.DisplayStatus(
                "Generics improve code reusability and type safety by allowing the same collection implementation " +
                "to work with different data types.");
        }
    }
}