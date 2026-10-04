using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Demonstrates asynchronous programming patterns using ConfigureAwait to control context capturing and simulate time-consuming operations.
    /// </summary>
    internal class Task6ConfigureAwaitDemo
    {
        /// <summary>
        /// Executes Method A asynchronously, displays execution status, and logs the result.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task MethodB()
        {
            ConsoleHelpers.DisplayStatus($"[MethodB, Thread: {Thread.CurrentThread.ManagedThreadId}] Started Method B execution");
            int result = await this.MethodA();
            ConsoleHelpers.DisplaySuccess($"[MethodB, Thread: {Thread.CurrentThread.ManagedThreadId}] Method B received result from Method A: {result}");
            ConsoleHelpers.DisplayStatus($"[MethodB, Thread: {Thread.CurrentThread.ManagedThreadId}] Method B ended with result: {result}");
        }

        private async Task<int> MethodA()
        {
            Random random = new Random();
            ConsoleHelpers.DisplayStatus($"[MethodA, Thread: {Thread.CurrentThread.ManagedThreadId}] Started Method A execution");
            ConsoleHelpers.DisplayStatus("Simulating a time-consuming operation (3 seconds) in Method A with .ConfigureAwait(false)");
            await Task.Delay(3000).ConfigureAwait(false);
            ConsoleHelpers.DisplayStatus($"[MethodA, Thread: {Thread.CurrentThread.ManagedThreadId}] Method A ended");
            return random.Next(1, 100);
        }
    }
}
