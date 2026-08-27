using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9LINQAdvanced.Tasks
{
    internal class Task3
    {
        public void RunTask3()
        {
            int[] numbers = { 12, 3, 5, 8, -2, 5, 10, 0, 7, 3, 14, 2, 8, -2, 6, 4 };
            int targetSum = 10;

            var secondHighestNumber = numbers
                .OrderByDescending(x => x)
                .Skip(1)
                .First()
                .ToString();

            var pairsAddingToTarget = numbers
                .SelectMany((number1, index1) => numbers.Select((int number2, int index2) => new { num1= number1, num2=number2, index1, index2 }))
                .Where(pair => pair.index1 < pair.index2 && pair.num1 + pair.num2 == targetSum)
                .Select(applicablePair => $"({applicablePair.num1}, {applicablePair.num2})")
                .Distinct();

            Console.WriteLine("Task 3\n");
            Console.Write("Array elements: ");
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine($"\n\nSecond Highest number in the array: {int.Parse(secondHighestNumber)}");
            Console.WriteLine($"\nUnique Pairs adding upto the target ({targetSum}) : ");
            if (pairsAddingToTarget == null)
            {
                Console.WriteLine($"There is no pair which add upto the target sum ({targetSum}).");
            }
            else
            {
                Console.WriteLine(string.Join(", ", pairsAddingToTarget));
            }
        }
    }
}
