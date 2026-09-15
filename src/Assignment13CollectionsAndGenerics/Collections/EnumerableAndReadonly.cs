using Assignment13CollectionsAndGenerics.Data;

namespace Assignment13CollectionsAndGenerics.Collections
{
    /// <summary>
    /// Represents the task 6 of working with IEnumerable and IReadOnly.
    /// </summary>
    internal class EnumerableAndReadonly
    {
        /// <summary>
        /// Performs a series of operations for task 6.
        /// </summary>
        /// <param name="integerList">The integer list elements.</param>
        /// <param name="integerArray">The integer array elements.</param>
        /// <param name="integerQueue">The integer queue elements.</param>
        internal void PerformOperations(List<int> integerList, int[] integerArray, Queue<int> integerQueue)
        {
            ConsoleHelpers.DisplayTitle("TASK 6 : WORKING WITH IENUMERABLE AND IREADONLY");
            ConsoleHelpers.DisplayStatus("Passed a list of integers to SumOfElements method.");
            int sumOfList = this.SumOfElements(integerList);
            ConsoleHelpers.DisplayStatus($"SUM RESULT (LIST) = {sumOfList}");

            ConsoleHelpers.DisplayStatus("Passed an array of integers to SumOfElements method.");
            int sumOfArray = this.SumOfElements(integerArray);
            ConsoleHelpers.DisplayStatus($"SUM RESULT (ARRAY) = {sumOfArray}");

            ConsoleHelpers.DisplayStatus("Passed a queue of integers to SumOfElements method.");
            int sumOfQueue = this.SumOfElements(integerQueue);
            ConsoleHelpers.DisplayStatus($"SUM RESULT (QUEUE) = {sumOfQueue}");
            ConsoleHelpers.PrintLine();

            IReadOnlyDictionary<string,int> keyValuePairs = this.GenerateDictionary();
            ConsoleHelpers.DisplayStatus("Generated a dictionary.");
            this.PrintDictionary(keyValuePairs);
            ConsoleHelpers.DisplayStatus("Trying to modify the IReadOnly Dictionary...");

            // keyValuePairs["Apple"] = 10; This throws a compile-time error.
            ConsoleHelpers.DisplayFailure("Statement: keyValuePairs[\"Apple\"] = 10;\nThis throws a compile time error: \nCS0200: Property or indexer 'IReadOnlyDictionary<string, int>.this[string]' cannot be assigned to -- it is read only.");
        }

        /// <summary>
        /// Calculates the sum of the specified collection of integers.
        /// </summary>
        /// <param name="elements">The collection of integers to sum.</param>
        /// <returns>The sum of the integers in the collection.</returns>
        internal int SumOfElements(IEnumerable<int> elements)
        {
            return elements.Sum();
        }

        /// <summary>
        /// Generates a read-only dictionary containing inventory items and their quantities.
        /// </summary>
        /// <returns>A read-only dictionary with item names as keys and their quantities as values.</returns>
        internal IReadOnlyDictionary<string, int> GenerateDictionary()
        {
            Dictionary<string, int> keyValuePairs = TestData.InventoryDictionary;
            return keyValuePairs;
        }

        /// <summary>
        /// Writes each key and value from the specified dictionary to the console in a tabular format.
        /// </summary>
        /// <param name="keyValuePairs">A read-only dictionary containing string keys and integer values to display.</param>
        internal void PrintDictionary(IReadOnlyDictionary<string, int> keyValuePairs)
        {
            Console.WriteLine("Key      | Value");

            foreach (var pair in keyValuePairs)
            {
                Console.WriteLine($"{pair.Key,-8} | {pair.Value,3}");
            }
        }
    }
}
