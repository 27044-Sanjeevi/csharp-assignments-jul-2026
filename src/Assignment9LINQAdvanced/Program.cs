namespace Assignments
{
    using System.Runtime.CompilerServices;
    using Assignment9LINQAdvanced;
    using Assignment9LINQAdvanced.Database;
    using Assignment9LINQAdvanced.Models.Enums;
    using Assignment9LINQAdvanced.Tasks;

    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        private static int _maxChoice = 6;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ProductDatabase productDatabase = new ProductDatabase();
            SupplierDatabase supplierDatabase = new SupplierDatabase();

            Task1 task1 = new Task1(productDatabase);
            Task2 task2 = new Task2(productDatabase, supplierDatabase);
            Task3 task3 = new Task3();
            Task4 task4 = new Task4(productDatabase);
            Task5 task5 = new Task5(productDatabase);

            MenuChoice menuChoice;
            do
            {
                Console.WriteLine("ADVANCED LINQ\n");

                DisplayTasksMenu();

                Console.Write("\nEnter your choice of task to run :");
                menuChoice = (MenuChoice)ConsoleHelpers.ReadChoice(_maxChoice);
                Console.Clear();
                switch (menuChoice)
                {
                    case MenuChoice.BasicLinqQueries:
                        task1.RunTask1();
                        break;
                    case MenuChoice.ComplexLinqQueries:
                        task2.RunTask2();
                        break;
                    case MenuChoice.LinqToObjects:
                        task3.RunTask3();
                        break;
                    case MenuChoice.PerformanceConsiderations:
                        task4.RunTask4();
                        break;
                    case MenuChoice.QueryBuilder:
                        task5.RunTask5();
                        break;
                    case MenuChoice.Exit:
                        break;
                }

                if (menuChoice != MenuChoice.Exit)
                {
                    ConsoleHelpers.Pause();
                    Console.Clear();
                }
            }
            while (menuChoice != MenuChoice.Exit);
        }

        private static void DisplayTasksMenu()
        {
            Console.WriteLine("Available Tasks: ");
            var choices = Enum.GetValues<MenuChoice>();
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"Task [{i + 1}] : {choices[i]}");
            }
        }
    }
}