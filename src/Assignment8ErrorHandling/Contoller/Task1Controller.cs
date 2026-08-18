namespace Assignment8ErrorHandling.Contoller
{
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Provides the methods for orchestrating Task 1.
    /// </summary>
    internal class Task1Controller
    {
        private readonly ConsoleView _consoleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task1Controller"/> class.
        /// </summary>
        /// <param name="view">The view renderer object.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object passed is null.</exception>
        internal Task1Controller(ConsoleView view)
        {
            this._consoleView = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Task1()
        {
            this._consoleView.PrintTask1Header();
            int a = this._consoleView.ReadInt();
        }
    }
}
