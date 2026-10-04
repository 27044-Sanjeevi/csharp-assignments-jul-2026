namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Provides examples of asynchronous operations that can result in deadlocks when not properly awaited.
    /// </summary>
    internal class Task5Deadlock
    {
        /// <summary>
        /// Executes an asynchronous operation and writes the result to the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeadlockMethod()
        {
            // var result = this.SomeAsyncOperation().Result; // This can cause a deadlock
            var result = await this.SomeAsyncOperation();
            Console.WriteLine(result);
        }

        private async Task<string> SomeAsyncOperation()
        {
            await Task.Delay(1000);
            return "Hello, World!";
        }
    }
}