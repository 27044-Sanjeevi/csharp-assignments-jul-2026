using Assignment18AsynchronousProgramming;
using Assignment18AsynchronousProgramming.Menu;
using Assignment18AsynchronousProgramming.Tasks;
using Assignment18AsynchronousProgramming.Utilities;

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
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task Main()
        {
            // Task 1
            HttpClient httpClient = new HttpClient();
            Task1HttpClient httpClientTask = new Task1HttpClient(httpClient);

            // Task 2
            Task2Tpl tplTask = new Task2Tpl();

            // Task 3
            Task3Multithreading multithreadingTask = new Task3Multithreading();

            // Task 4
            Task4CascadingAsyncOperations cascadingAsyncOperations = new Task4CascadingAsyncOperations(httpClient);

            // Task 5
            Task5Deadlock task5Deadlock = new Task5Deadlock();

            // Task 6
            Task6ConfigureAwaitDemo task6ConfigureAwaitDemo = new Task6ConfigureAwaitDemo();

            // Task 7
            Task7ExceptionHandlingDemo task7ExceptionHandlingDemo = new Task7ExceptionHandlingDemo();

            // Menu View
            MenuView view = new MenuView();
            MenuOptions option = MenuOptions.Task1HttpClient;

            try
            {
                while (option != MenuOptions.Exit)
                {
                    try
                    {
                        Console.Clear();
                        view.DisplayMenu();
                        option = view.GetMenuChoice();
                        Console.Clear();
                        switch (option)
                        {
                            case MenuOptions.Task1HttpClient:
                                await RunTask1(httpClientTask);
                                break;
                            case MenuOptions.Task2Tpl:
                                RunTask2(tplTask);
                                break;
                            case MenuOptions.Task3Multithreading:
                                RunTask3(multithreadingTask);
                                break;
                            case MenuOptions.Task4CascadingAsyncOperations:
                                await RunTask4(cascadingAsyncOperations);
                                break;
                            case MenuOptions.Task5Deadlock:
                                await RunTask5(task5Deadlock);
                                break;
                            case MenuOptions.Task6ConfigureAwait:
                                await RunTask6(task6ConfigureAwaitDemo);
                                break;
                            case MenuOptions.Task7ExceptionHandling:
                                await RunTask7(view, task7ExceptionHandlingDemo);
                                break;
                            case MenuOptions.Exit:
                                return;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(option));
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("\n[ARGUMENT NULL EXCEPTION] : " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
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
        /// Executes an asynchronous request using the specified HTTP client.
        /// </summary>
        /// <param name="task1">The HTTP client task used to execute the request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task RunTask1(Task1HttpClient task1)
        {
            await task1.ExecuteRequestAsync();
        }

        /// <summary>
        /// Executes the specified Task 2 using Task Parallel Library.
        /// </summary>
        /// <param name="task2">The Task parallel library task to execute task 2.</param>
        internal static void RunTask2(Task2Tpl task2)
        {
            task2.RunTask();
        }

        /// <summary>
        /// Executes the specified Task 3 using multithreading.
        /// </summary>
        /// <param name="task3">The multithreading operations to execute task 3.</param>
        internal static void RunTask3(Task3Multithreading task3)
        {
            task3.PerformOperations();
        }

        /// <summary>
        /// Executes the specified Task 4 for performing cascading asynchronous operations.
        /// </summary>
        /// <param name="task4">The cascading async operations to execute task 4.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task RunTask4(Task4CascadingAsyncOperations task4)
        {
            await task4.RunCascadingOperationsAsync();
        }

        /// <summary>
        /// Executes the specified Task 5 demonstrating the deadlock conditions.
        /// </summary>
        /// <param name="task5">The deadlock demo to execute task 5.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task RunTask5(Task5Deadlock task5)
        {
            await task5.DeadlockMethod();
        }

        /// <summary>
        /// Executes the specified Task 6 using configure await as false.
        /// </summary>
        /// <param name="task6">The configure await configuration demo to execute task 6.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task RunTask6(Task6ConfigureAwaitDemo task6)
        {
            await task6.MethodB();
        }

        /// <summary>
        /// Executes the specified Task 7 using exception handling.
        /// </summary>
        /// <param name="menuView">An instance of the menu view.</param>
        /// <param name="task7">The exception handling demonstration to execute task 7.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal static async Task RunTask7(MenuView menuView, Task7ExceptionHandlingDemo task7)
        {
            ConsoleHelpers.DisplayFailure("[WARNING] Running method 2 causes unhandled exception.");
            int choice = menuView.ReadChoice(2, "Choose method to run (1 for async Task, 2 for async Void): ");

            switch (choice)
            {
                case 1:
                    try
                    {
                        await task7.TaskMethod();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
                    }

                    break;
                case 2:
                    try
                    {
                        task7.VoidMethod();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), "Invalid choice. Please select 1 or 2.");
            }
        }
    }
}