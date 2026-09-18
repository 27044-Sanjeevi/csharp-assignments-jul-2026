using System.Net.Http.Headers;
using Assignment16AdvancedCSharpConcepts;
using Assignment16AdvancedCSharpConcepts.Menu;
using Assignment16AdvancedCSharpConcepts.Task1;
using Assignment16AdvancedCSharpConcepts.Task2;
using Assignment16AdvancedCSharpConcepts.Task3;
using Assignment16AdvancedCSharpConcepts.Task4;
using Assignment16AdvancedCSharpConcepts.Task5;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Task 1
                Notifier notifier = new Notifier();

                // Task 2
                VarKeywordDemo varKeywordDemo = new VarKeywordDemo();
                DynamicKeywordDemo dynamicKeywordDemo = new DynamicKeywordDemo();

                // Task 3
                SortArray sortArrayDemo = new SortArray();

                // Task 4
                LambdaExpressionsDemo lambdaExpressionsDemo = new LambdaExpressionsDemo();

                // Task 5
                ProductRepository productRepository = new ProductRepository();
                SortStrategy sortStrategy = new SortStrategy();
                ProductSorter productSorter = new ProductSorter();
                List<Product> products = productRepository
                .GetProducts()
                .ToList();

                // Menu view
                MenuView view = new MenuView();
                MenuOptions option = MenuOptions.Task1Notifier;

                while (option != MenuOptions.Exit)
                {
                    Console.Clear();
                    view.DisplayMenu();
                    option = view.GetMenuChoice();
                    Console.Clear();
                    switch (option)
                    {
                        case MenuOptions.Task1Notifier:
                            RunTask1(notifier);
                            break;
                        case MenuOptions.Task2VarAndDynamic:
                            RunTask2(varKeywordDemo, dynamicKeywordDemo);
                            break;
                        case MenuOptions.Task3AnonymousMethods:
                            RunTask3(sortArrayDemo);
                            break;
                        case MenuOptions.Task4LambdaExpressionsAndStatements:
                            RunTask4(lambdaExpressionsDemo);
                            break;
                        case MenuOptions.Task5DelegatesForSorting:
                            RunTask5(products, sortStrategy, productSorter);
                            break;
                        case MenuOptions.Task6Records:
                            break;
                        case MenuOptions.Task7PatternMatching:
                            break;
                        case MenuOptions.Exit:
                            return;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(option));
                    }

                    view.Pause();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
            }

            Console.ReadKey();
        }

        public static void RunTask1(Notifier notifier)
        {
            ConsoleHelpers.DisplayTitle("Task 1: Events and Delegates");

            notifier.OnAction += PrintConsoleNotification; // Subscriber
            notifier.SendNotification("This is a sample notification."); // Publishing to the subscriber
        }

        public static void RunTask2(VarKeywordDemo varKeywordDemo, DynamicKeywordDemo dynamicKeywordDemo)
        {
            ConsoleHelpers.DisplayTitle("Task 2: var vs dynamic keywords");
            varKeywordDemo.RunDemo();
            dynamicKeywordDemo.RunDemo();
        }

        public static void RunTask3(SortArray sortArray)
        {
            ConsoleHelpers.DisplayTitle("Task 3: Sorting Array using anonymous method");
            sortArray.Run();
        }

        public static void RunTask4(LambdaExpressionsDemo lambdaExpressionsDemo)
        {
            ConsoleHelpers.DisplayTitle("Task 4: Lambda Expressions and Statements");
            lambdaExpressionsDemo.RunDemo();
        }

        public static void RunTask5(List<Product> products, SortStrategy sortStrategy, ProductSorter productSorter)
        {
            ConsoleHelpers.DisplayTitle("Task 5: Lambda Expressions and Statements");

            ConsoleHelpers.DisplayStatus("Sorting by Name...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByName);

            ConsoleHelpers.DisplayStatus("Sorting by Category...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByCategory);

            ConsoleHelpers.DisplayStatus("Sorting by Price...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByPrice);
        }

        private static void PrintConsoleNotification(string message)
        {
            ConsoleHelpers.DisplaySuccessMessage($"Notification Received: {message}");
        }
    }
}