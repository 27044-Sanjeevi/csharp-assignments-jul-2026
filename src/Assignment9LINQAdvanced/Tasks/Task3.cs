namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents Task 3 of the application.
    /// </summary>
    internal class Task3
    {
        /// <summary>
        /// Runs Task 3 of the application.
        /// </summary>
        public void RunTask3()
        {
            int[] numbers = { 12, 3, 5, 8, -2, 5, 10, 0, 7, 3, 14, 2, 8, -2, 6, 4 };

            int targetSum = 10;

            List<int> distinctSortedNumbers = numbers
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            int secondHighestNumber;

            if (distinctSortedNumbers.Count > 1)
            {
                secondHighestNumber = distinctSortedNumbers[1];
            }
            else
            {
                Console.WriteLine("There is only one unique element present in the list.");
                return;
            }

            IEnumerable<string> pairsAddingToTarget = numbers
                .SelectMany((number1, index1) => numbers.Select((number2, index2) => new { num1= number1, num2=number2, index1, index2 }))
                .Where(pair => pair.index1 < pair.index2 && pair.num1 + pair.num2 == targetSum)
                .Select(applicablePair => $"({applicablePair.num1}, {applicablePair.num2})")
                .Distinct();

            Console.WriteLine("Task 3\n");
            Console.Write("Array elements: ");
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine($"\n\nSecond Highest number in the array: {secondHighestNumber}");
            if (!pairsAddingToTarget.Any())
            {
                Console.WriteLine($"\nThere is no pair which add upto the target sum ({targetSum}).");
            }
            else
            {
                Console.WriteLine($"\nUnique Pairs adding upto the target ({targetSum}) : ");
                Console.WriteLine(string.Join(", ", pairsAddingToTarget));
            }
        }
    }
}
