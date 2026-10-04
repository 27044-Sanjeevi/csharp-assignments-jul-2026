using System.Diagnostics;
using Assignment18AsynchronousProgramming.Utilities;

namespace Assignment18AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Provides methods to calculate and display the squares of numbers up to a specified iteration count using both sequential and parallel loops.
    /// </summary>
    internal class Task2Tpl
    {
        private const long IterationCount = 1_000_00;
        private double _normalLoopTime;
        private double _concurrentLoopTime;
        private int _cursorTopPosition = 1;

        /// <summary>
        /// Executes both the normal and concurrent loop operations, displaying subtitles for each section.
        /// </summary>
        public void RunTask()
        {
            ConsoleHelpers.DisplaySubtitle("NORMAL LOOP");
            this.RunNormalLoop();

            this._cursorTopPosition += 3; // move cursor down to avoid overwriting previous output

            ConsoleHelpers.DisplaySubtitle("CONCURRENT LOOP");
            this.RunConcurrentLoop();

            this._cursorTopPosition += 2;
            this.PrintTimeDifference();
        }

        /// <summary>
        /// Executes a loop to calculate and print the square of each number from 0 to IterationCount, measuring and displaying the elapsed time.
        /// </summary>
        public void RunNormalLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (long i = 0; i < IterationCount; i++)
            {
                double square = Math.Pow(i, 2);
                this.PrintPowerResult(i, square, 0, this._cursorTopPosition);
            }

            stopwatch.Stop();
            this._normalLoopTime = stopwatch.ElapsedMilliseconds;
            this._cursorTopPosition++;
            this.PrintTime(this._normalLoopTime, 0, this._cursorTopPosition);
        }

        /// <summary>
        /// Executes a parallel loop to calculate and print the square of each number up to the specified iteration count.
        /// </summary>
        public void RunConcurrentLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For(0, IterationCount, i =>
            {
                double square = Math.Pow(i, 2);
                this.PrintPowerResult(i, square, 0, this._cursorTopPosition);
            });

            stopwatch.Stop();
            this._concurrentLoopTime = stopwatch.ElapsedMilliseconds;
            this._cursorTopPosition += 20;
            this.PrintTime(this._concurrentLoopTime, 0, 20);
        }

        /// <summary>
        /// Prints the time taken for the loop execution at the specified console position.
        /// </summary>
        /// <param name="time">Time taken for the operation.</param>
        /// <param name="left">Left cursor position.</param>
        /// <param name="top">Top cursor position.</param>
        public void PrintTime(double time, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine($"Time Taken = {time} ms");
        }

        private void PrintPowerResult(long number, double squaredNumber, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine($"Square of {number} = {squaredNumber}");
        }

        private void PrintTimeDifference()
        {
            double timeDifference = Math.Abs(this._concurrentLoopTime - this._normalLoopTime);

            if (this._concurrentLoopTime < this._normalLoopTime)
            {
                double percentageFaster = ((this._normalLoopTime - this._concurrentLoopTime) / this._normalLoopTime) * 100;
                ConsoleHelpers.WriteColored($"Concurrent loop is faster by {timeDifference:F2} ms ({percentageFaster:F1}% faster).", ConsoleColor.Green);
            }
            else if (this._normalLoopTime < this._concurrentLoopTime)
            {
                double percentageFaster = ((this._concurrentLoopTime - this._normalLoopTime) / this._concurrentLoopTime) * 100;
                ConsoleHelpers.WriteColored($"Normal loop is faster by {timeDifference:F2} ms ({percentageFaster:F1}% faster).", ConsoleColor.Green);
            }
            else
            {
                ConsoleHelpers.WriteColored("Both loops took the exact same time.", ConsoleColor.Green);
            }
        }
    }
}
