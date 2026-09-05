using System;
using IDisposableDemo;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            const string filePath = "sample.txt";

            Console.WriteLine("--- TASk 4: EXPLORING IDISPOSABLE ---\n");

            WriteIntoFile(filePath);

            Console.WriteLine("Trying to access the file after the write operation:");
            string content = File.ReadAllText(filePath);
            PrintLine();
            Console.WriteLine(filePath);
            PrintLine();
            Console.Write(content);
            PrintLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Writes into the file using custom write class.
        /// </summary>
        /// <param name="filePath">The path of the file to write into.</param>
        internal static void WriteIntoFile(string filePath)
        {
            Console.WriteLine("Initializing a custom writer instance.");
            using var writer = new CustomFileWriter(filePath);
            writer.Write("This is the text written to the file.");
            Console.WriteLine("Wrote a sample content into the given file with the usage of using keyword.\n");

            // The dispose method is not called manually here.
            // Since using keyword is used, it automatically calls the dispose() method in the instance once the scope of using statement gets over.
        }

        /// <summary>
        /// Prints dashed line into the console.
        /// </summary>
        private static void PrintLine()
        {
            Console.WriteLine(new string('-', 50));
        }
    }
}