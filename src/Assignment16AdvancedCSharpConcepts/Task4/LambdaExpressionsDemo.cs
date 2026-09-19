namespace Assignment16AdvancedCSharpConcepts.Task4
{
    /// <summary>
    /// Represents the operations to demonstrate task 4.
    /// </summary>
    internal class LambdaExpressionsDemo
    {
        /// <summary>
        /// Runs the operations to demonstrate task 4.
        /// </summary>
        public void RunDemo()
        {
            List<int> integerList = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
            };

            ConsoleHelpers.DisplayStatus("Initialized a new integer list: ");
            this.PrintCollection(integerList);

            ConsoleHelpers.DisplayStatus("Applying WHERE LINQ method to filter out even numbers...");
            IEnumerable<int> filterQuery = integerList
                .Where(number => number % 2 == 0);
            this.PrintCollection(filterQuery);

            ConsoleHelpers.DisplayStatus("Using SELECT LINQ method to square the filtered numbers...");
            IEnumerable<int> squareQuery = filterQuery
                .Select(number => number * number);
            this.PrintCollection(squareQuery);
        }

        private void PrintCollection(IEnumerable<int> items)
        {
            Console.Write("LIST: ");
            foreach (var item in items)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
