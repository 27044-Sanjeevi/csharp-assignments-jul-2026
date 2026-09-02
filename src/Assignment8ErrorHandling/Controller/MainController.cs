namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.Models.Enum;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Routes the main application menu choices to the respective task controllers.
    /// </summary>
    internal class MainController
    {
        private readonly ConsoleView _view;
        private readonly ITaskController _task1Controller;
        private readonly ITaskController _task2Controller;
        private readonly ITaskController _task3Controller;
        private readonly ITaskController _task4Controller;
        private readonly ITaskController _task5Controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="task1Controller">The controller for task 1.</param>
        /// <param name="task2Controller">The controller for task 2.</param>
        /// <param name="task3Controller">The controller for task 3.</param>
        /// <param name="task4Controller">The controller for task 4.</param>
        /// <param name="task5Controller">The controller for task 5.</param>
        public MainController(
            ConsoleView view,
            ITaskController task1Controller,
            ITaskController task2Controller,
            ITaskController task3Controller,
            ITaskController task4Controller,
            ITaskController task5Controller)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._task1Controller = task1Controller ?? throw new ArgumentNullException(nameof(task1Controller));
            this._task2Controller = task2Controller ?? throw new ArgumentNullException(nameof(task2Controller));
            this._task3Controller = task3Controller ?? throw new ArgumentNullException(nameof(task3Controller));
            this._task4Controller = task4Controller ?? throw new ArgumentNullException(nameof(task4Controller));
            this._task5Controller = task5Controller ?? throw new ArgumentNullException(nameof(task5Controller));
        }

        /// <summary>
        /// Routes choice inputs to corresponding tasks.
        /// </summary>
        /// <param name="choice">The menu choice.</param>
        /// <returns>True if the program should exit; otherwise false.</returns>
        public bool HandleMenu(int choice)
        {
            if (!Enum.IsDefined(typeof(MenuChoice), choice))
            {
                this._view.WriteColored("Invalid option!", ConsoleColor.Red);
            }

            MenuChoice menuChoice = (MenuChoice)choice;

            switch (menuChoice)
            {
                case MenuChoice.Task1:
                    this._task1Controller.RunTask();
                    break;
                case MenuChoice.Task2:
                    this._task2Controller.RunTask();
                    break;
                case MenuChoice.Task3:
                    this._task3Controller.RunTask();
                    break;
                case MenuChoice.Task4:
                    this._task4Controller.RunTask();
                    break;
                case MenuChoice.Task5:
                    this._task5Controller.RunTask();
                    break;
                case MenuChoice.Exit:
                    return true;
            }

            return false;
        }
    }
}
