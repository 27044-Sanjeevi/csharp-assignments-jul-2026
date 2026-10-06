using System;
using System.Dynamic;
using System.IO;
using DUtilityApp;

namespace Assignments
{
    internal static class Program
    {
        private const string LogFileName = "math_history_log.txt";
        private static readonly string SolutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
        private static readonly string LogFilePath = Path.Combine(SolutionDir, LogFileName);

        public static void Main(string[] args)
        {
            Console.WriteLine("Project E: Reading Math History");

            if (File.Exists(LogFilePath))
            {
                string[] logLines = File.ReadAllLines(LogFilePath);

                Console.WriteLine($"Found {logLines.Length} record(s):\n");

                foreach (string line in logLines)
                {
                    Console.WriteLine($"  -> {line}");
                }
            }
            else
            {
                ConsoleHelpers.WriteColored($"[Warning] The file '{LogFilePath}' was not found.", ConsoleColor.Yellow);
            }

            Console.WriteLine("\nPress any key to exit Project E...");
            Console.ReadKey();
        }
    }
}
