namespace Assignment8ErrorHandling
{
    using System;
    using Assignment8ErrorHandling.Controller;
    using Assignment8ErrorHandling.IO;
    using Assignment8ErrorHandling.Utilities;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Contains the Main entry point of the project.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Initializes dependencies and runs the controller.
        /// </summary>
        internal static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionHandler;

            // View
            ConsoleIO consoleIo = new ConsoleIO();
            ConsoleHelper consoleHelper = new ConsoleHelper(consoleIo);
            ConsoleView view = new ConsoleView(consoleIo, consoleHelper);

            // Controllers
            ITaskController task1Controller = new Task1Controller(view);
            ITaskController task2Controller = new Task2Controller(view);
            ITaskController task3Controller = new Task3Controller(view);
            ITaskController task4Controller = new Task4Controller(view);
            ITaskController task5Controller = new Task5Controller(view, task4Controller);

            MainController mainController = new MainController(
                view,
                task1Controller,
                task2Controller,
                task3Controller,
                task4Controller,
                task5Controller);

            // Application Runner
            ApplicationRunner applicationRunner = new ApplicationRunner(view, mainController);

            applicationRunner.RunApplication();
        }

        private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception? exception = e.ExceptionObject as Exception;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[GLOBAL UNHANDLED EXCEPTION HANDLER] A global unhandled exception occurred: {exception?.Message}");
            Console.WriteLine("Application is shutting down..");
            Console.ResetColor();
        }
    }
}