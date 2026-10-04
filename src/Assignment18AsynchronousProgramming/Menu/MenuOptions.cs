namespace Assignment18AsynchronousProgramming.Menu
{
    /// <summary>
    /// Specifies the menu options.
    /// </summary>
    internal enum MenuOptions
    {
        /// <summary>
        /// Specifies task 1 performing operations using HttpClient.
        /// </summary>
        Task1HttpClient = 1,

        /// <summary>
        /// Specifies task 2 performing operations using Task Parallel Library (TPL).
        /// </summary>
        Task2Tpl = 2,

        /// <summary>
        /// Specifies task 3 performing operations using multithreading.
        /// </summary>
        Task3Multithreading = 3,

        /// <summary>
        /// Specifies task 4 performing cascading asynchronous operations.
        /// </summary>
        Task4CascadingAsyncOperations = 4,

        /// <summary>
        /// Specifies task 5 demonstrating a deadlock scenario.
        /// </summary>
        Task5Deadlock = 5,

        /// <summary>
        /// Specifies task 6 demonstrating the use of ConfigureAwait as false in asynchronous programming.
        /// </summary>
        Task6ConfigureAwait = 6,

        /// <summary>
        /// Specifies task 7 demonstrating exception handling in asynchronous programming.
        /// </summary>
        Task7ExceptionHandling = 7,

        /// <summary>
        /// Specifies the option to exit the application.
        /// </summary>
        Exit = 8,
    }
}
