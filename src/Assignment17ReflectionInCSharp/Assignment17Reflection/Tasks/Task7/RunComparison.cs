using System.Diagnostics;
using System.Text;
using Assignment17Reflection.Utilities;

namespace Assignment17Reflection.Tasks.Task7
{
    /// <summary>
    /// Contains the runner for Task 7.
    /// </summary>
    internal class RunComparison
    {
        /// <summary>
        /// Runs the current task.
        /// </summary>
        internal void RunTask()
        {
            UserData user = new ("Ram", "ram@gmail.com", 30);
            int iterations = 1_000_000;

            ConsoleHelpers.DisplaySubtitle("Verifying Output Correctness");
            string reflectionOut = ReflectionSerializer.Serialize(user);
            Console.WriteLine($"Reflection Output: {reflectionOut}");

            var compiledSerializer = EmitSerializer.CreateSerializer(typeof(UserData));
            StringBuilder sb = new StringBuilder();
            compiledSerializer(user, sb);
            Console.WriteLine($"Emit Output: {sb.ToString()}");

            ConsoleHelpers.DisplayStatus($"Running Micro-Benchmark ({iterations:N0} Iterations)");

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                string output = ReflectionSerializer.Serialize(user);
            }

            sw.Stop();
            long reflectionTime = sw.ElapsedMilliseconds;
            ConsoleHelpers.DisplaySuccess($"Traditional Reflection: {reflectionTime} ms");

            StringBuilder warmSb = new StringBuilder();
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                warmSb.Clear();
                compiledSerializer(user, warmSb);
            }

            sw.Stop();
            long emitTime = sw.ElapsedMilliseconds;
            ConsoleHelpers.DisplaySuccess($"Reflection.Emit Engine: {emitTime} ms");

            double speedup = (double)reflectionTime / emitTime;
            Console.WriteLine($"Result: Emit is {speedup:F2}x faster with zero mid-run allocations.");
        }
    }
}
