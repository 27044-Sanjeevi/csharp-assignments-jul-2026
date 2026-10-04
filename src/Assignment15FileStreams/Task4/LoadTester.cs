using System.Diagnostics;

namespace Assignment15FileStreams.Task4
{
    /// <summary>
    /// Represents the Task 4.
    /// </summary>
    internal class LoadTester
    {
        private const int TotalUsers = 20;
        private const int LogsPerUser = 50;

        /// <summary>
        /// Executes the load test for Task 4.
        /// </summary>
        public void RunLoadTest()
        {
            ConsoleHelpers.DisplayStatus($"{TotalUsers} Users x {LogsPerUser} Logs = {TotalUsers * LogsPerUser} Operations..");

            LoadTestResult threadSafeResult = this.TestLogger(
                "Thread-Safe Single Shared File",
                (userId, msg) => ThreadSafeLogger.LogError(msg));

            LoadTestResult independentResult = this.TestLogger(
                "Independent Isolated Files",
                (userId, msg) => IndependentUserLogger.LogError(userId, msg));

            this.PrintReport(threadSafeResult);
            this.PrintReport(independentResult);
        }

        private LoadTestResult TestLogger(string strategyName, Action<int, string> logAction)
        {
            ConsoleHelpers.DisplayStatus($"Running strategy: {strategyName}...");
            int successCount = 0;
            int failureCount = 0;

            Stopwatch sw = Stopwatch.StartNew();

            Parallel.For(1, TotalUsers + 1, new ParallelOptions { MaxDegreeOfParallelism = TotalUsers }, userId =>
            {
                for (int logIndex = 1; logIndex <= LogsPerUser; logIndex++)
                {
                    try
                    {
                        string message = $"[ERROR] User {userId:D2} at {DateTime.UtcNow:HH:mm:ss.fff}";
                        logAction(userId, message);
                        Interlocked.Increment(ref successCount);
                    }
                    catch (Exception)
                    {
                        Interlocked.Increment(ref failureCount);
                    }
                }
            });

            sw.Stop();
            double totalSeconds = sw.Elapsed.TotalSeconds > 0 ? sw.Elapsed.TotalSeconds : 0.001;
            double throughput = successCount / totalSeconds;

            return new LoadTestResult
            {
                Name = strategyName,
                ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds,
                SuccessCount = successCount,
                FailureCount = failureCount,
                ThroughputPerSec = throughput,
            };
        }

        private void PrintReport(LoadTestResult result)
        {
            Console.WriteLine(new string('=', 110));
            Console.WriteLine($"{"Strategy",-45} | {"Time (ms)",-10} | {"Success",-8} | {"Failed (Locks)",-15} | {"Throughput",-15}");
            Console.WriteLine(new string('-', 110));

            string status = result.FailureCount > 0 ? $"{result.FailureCount} Errors" : "0 (No Errors)";
            Console.WriteLine($"{result.Name,-45} | {result.ElapsedMilliseconds,10:F1} | {result.SuccessCount,8} | {status,-15} | {result.ThroughputPerSec,11:N0} logs/s");
            Console.WriteLine(new string('=', 110) + "\n");
        }
    }
}
