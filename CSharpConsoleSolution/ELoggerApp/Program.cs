using System;
using System.IO;
using DUtilityApp;

namespace Assignments
{
    internal static class Program
    {
        private static readonly string LogFilePath = "math_history_log.txt";

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
