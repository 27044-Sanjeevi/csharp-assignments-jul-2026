namespace Assignment16AdvancedCSharpConcepts.Task3
{
    /// <summary>
    /// Provides functionality to sort arrays using anonymous function.
    /// </summary>
    internal class SortArray
    {
        /// <summary>
        /// Executes the array sorting demonstration using a lambda expression.
        /// </summary>
        public void Run()
        {
            // usage of the delegates throws style cop warnings since the analyzer expects the lambda expressions here.
            // Comparison<int> ascendingOrderComparator = delegate (int x, int y)
            // {
            //     return x.CompareTo(y);
            // };
            Comparison<int> ascendingOrderComparator = (int x, int y) =>
            {
                return x.CompareTo(y);
            };

            int[] integerArray = { 23, 13, -12, 90, -77, 38, 28, 57, 0, 89 };
            ConsoleHelpers.DisplayStatus("Array before sorting: ");
            this.PrintArray(integerArray);
            ConsoleHelpers.DisplayStatus("Sorting arrays using the custom anonymous method...");
            Array.Sort(integerArray, ascendingOrderComparator);
            ConsoleHelpers.DisplayStatus("Array after sorting: ");
            this.PrintArray(integerArray);
        }

        private void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
