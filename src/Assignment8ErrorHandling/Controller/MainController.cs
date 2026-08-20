namespace Assignment8ErrorHandling.Controller
{
    using System;

    /// <summary>
    /// Routes the main application menu choices to the respective task controllers.
    /// </summary>
    internal class MainController
    {
        private const int Task1Choice = 1;
        private const int Task2Choice = 2;
        private const int Task3Choice = 3;
        private const int Task4Choice = 4;
        private const int Task5Choice = 5;
        private const int ExitChoice = 6;

        private readonly ITaskController _task1Controller;
        private readonly ITaskController _task2Controller;
        private readonly ITaskController _task3Controller;
        private readonly ITaskController _task4Controller;
        private readonly ITaskController _task5Controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="task1Controller">The controller for task 1.</param>
        /// <param name="task2Controller">The controller for task 2.</param>
        /// <param name="task3Controller">The controller for task 3.</param>
        /// <param name="task4Controller">The controller for task 4.</param>
        /// <param name="task5Controller">The controller for task 5.</param>
        public MainController(
            ITaskController task1Controller,
            ITaskController task2Controller,
            ITaskController task3Controller,
            ITaskController task4Controller,
            ITaskController task5Controller)
        {
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
            switch (choice)
            {
                case Task1Choice:
                    this._task1Controller.RunTask();
                    break;
                case Task2Choice:
                    this._task2Controller.RunTask();
                    break;
                case Task3Choice:
                    this._task3Controller.RunTask();
                    break;
                case Task4Choice:
                    this._task4Controller.RunTask();
                    break;
                case Task5Choice:
                    this._task5Controller.RunTask();
                    break;
                case ExitChoice:
                    return true;
                default:
                    break;
            }

            return false;
        }
    }
}
