using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Demonstrates exception handling patterns in asynchronous methods.
    /// </summary>
    internal class Task7ExceptionHandlingDemo
    {
        /// <summary>
        /// Intentionally throws an exception to demonstrate exception handling in asynchronous methods returning Task.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task TaskMethod()
        {
            ConsoleHelpers.DisplayStatus("Throwing an error intentionally in asynchronous methods returning Task.");
            await Task.Delay(1);
            throw new Exception("An error occurred in the TaskMethod.");
        }

        /// <summary>
        /// Intentionally throws an exception to demonstrate exception handling in asynchronous methods returning void.
        /// </summary>
        public async void VoidMethod()
        {
            ConsoleHelpers.DisplayStatus("Throwing an error intentionally in asynchronous methods returning void.");
            await Task.Delay(1);
            throw new Exception("An error occurred in the VoidMethod.");
        }
    }
}
