using DUtilityApp;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of Project E - File Reader App.
    /// </summary>
    internal static class Program
    {
        private const string LogFileName = "math_history_log.txt";
        private static readonly string SolutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
        private static readonly string LogFilePath = Path.Combine(SolutionDir, LogFileName);

        private static void Main(string[] args)
        {
            Console.WriteLine("Project E: Reading Math History");
            DisplayHistory();
            Console.WriteLine("\nPress any key to exit Project E...");
            Console.ReadKey();
        }

        private static void DisplayHistory()
        {
            try
            {
                if (File.Exists(LogFilePath))
                {
                    string[] logEntries = File.ReadAllLines(LogFilePath);
                    Console.WriteLine("\nMath History Log:");
                    foreach (string entry in logEntries)
                    {
                        Console.WriteLine(entry);
                    }
                }
                else
                {
                    Console.WriteLine("No math history log found.");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayFailure($"An error occurred while reading the log file: {ex.Message}");
            }
        }
    }
}
