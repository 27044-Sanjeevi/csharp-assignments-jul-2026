using System.Text;
using Assignment16AdvancedCSharpConcepts;
using Assignment16AdvancedCSharpConcepts.Menu;
using Assignment16AdvancedCSharpConcepts.Task1;
using Assignment16AdvancedCSharpConcepts.Task2;
using Assignment16AdvancedCSharpConcepts.Task3;
using Assignment16AdvancedCSharpConcepts.Task4;
using Assignment16AdvancedCSharpConcepts.Task5;
using Assignment16AdvancedCSharpConcepts.Task6;
using Assignment16AdvancedCSharpConcepts.Task7;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        internal static void Main()
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

                // Task 6
                BookRecordDemo bookRecordDemo = new BookRecordDemo();

                // Task 7
                ShapesDemo shapesDemo = new ShapesDemo();

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
                            RunTask6(bookRecordDemo);
                            break;
                        case MenuOptions.Task7PatternMatching:
                            RunTask7(shapesDemo);
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

        /// <summary>
        /// Runs task 1.
        /// </summary>
        /// <param name="notifier">The notifier class instance driving the event broadcast.</param>
        internal static void RunTask1(Notifier notifier)
        {
            ConsoleHelpers.DisplayTitle("Task 1: Events and Delegates");

            notifier.OnAction += PrintConsoleNotification; // Subscriber
            notifier.SendNotification("This is a sample notification."); // Publishing to the subscriber
            notifier.OnAction -= PrintConsoleNotification;
        }

        /// <summary>
        /// Runs task 2.
        /// </summary>
        /// <param name="varKeywordDemo">The instance to run task 2 var keyword operations.</param>
        /// <param name="dynamicKeywordDemo">The instance to run the task 2 dynamic keyword operations.</param>
        internal static void RunTask2(VarKeywordDemo varKeywordDemo, DynamicKeywordDemo dynamicKeywordDemo)
        {
            ConsoleHelpers.DisplayTitle("Task 2: var vs dynamic keywords");
            varKeywordDemo.RunDemo();
            dynamicKeywordDemo.RunDemo();
        }

        /// <summary>
        /// Runs task 3.
        /// </summary>
        /// <param name="sortArray">The instance to perform the task 3 operations.</param>
        internal static void RunTask3(SortArray sortArray)
        {
            ConsoleHelpers.DisplayTitle("Task 3: Sorting Array using anonymous method");
            sortArray.Run();
        }

        /// <summary>
        /// Runs task 4.
        /// </summary>
        /// <param name="lambdaExpressionsDemo">The instance to perform the task 4 operations.</param>
        internal static void RunTask4(LambdaExpressionsDemo lambdaExpressionsDemo)
        {
            ConsoleHelpers.DisplayTitle("Task 4: Lambda Expressions and Statements");
            lambdaExpressionsDemo.RunDemo();
        }

        /// <summary>
        /// Runs task 5.
        /// </summary>
        /// <param name="products">The list of operations to perform the operations.</param>
        /// <param name="sortStrategy">The instance to inject the sorting strategy.</param>
        /// <param name="productSorter">The instance to perform the sorting operations.</param>
        internal static void RunTask5(List<Product> products, SortStrategy sortStrategy, ProductSorter productSorter)
        {
            ConsoleHelpers.DisplayTitle("Task 5: Lambda Expressions and Statements");

            ConsoleHelpers.DisplayStatus("Sorting by Name...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByName);

            ConsoleHelpers.DisplayStatus("Sorting by Category...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByCategory);

            ConsoleHelpers.DisplayStatus("Sorting by Price...");
            productSorter.SortAndDisplay(products, sortStrategy.SortByPrice);
        }

        /// <summary>
        /// Runs task 6.
        /// </summary>
        /// <param name="demo">The instance to run the task 6 operations.</param>
        internal static void RunTask6(BookRecordDemo demo)
        {
            ConsoleHelpers.DisplayTitle("Task 6: Book Records");
            demo.RunDemo();
        }

        /// <summary>
        /// Runs task 7.
        /// </summary>
        /// <param name="demo">The instance to run the task 7 operations.</param>
        internal static void RunTask7(ShapesDemo demo)
        {
            ConsoleHelpers.DisplayTitle("Task 7: Pattern Matching");
            demo.RunDemo();
        }

        private static void PrintConsoleNotification(string message)
        {
            ConsoleHelpers.DisplaySuccessMessage($"Notification Received: {message}");
        }
    }
}