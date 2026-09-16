using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Assignment15FileStreams;

namespace Assignments
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            try
            {
                const string weatherFilePath1 = "Weather1.txt";
                const string weatherFilePath2 = "Weather2.txt";
                const string weatherFilePath3 = "Weather3.txt";

                const string processedFilePath = "WeatherStatistics.txt";

                FileGenerator generator = new FileGenerator();

                generator.GenerateWeatherFile(weatherFilePath1, 1024 * 1024 * 1024);
                generator.GenerateWeatherFile(weatherFilePath2, 1024 * 1024 * 1024);
                generator.GenerateWeatherFile(weatherFilePath3, 1024 * 1024 * 1024);

                FileProcessor fileProcessor = new FileProcessor();
                FileProcessorAsync fileProcessorAsync = new FileProcessorAsync();

                MenuView view = new MenuView();
                MenuOptions option = MenuOptions.Task1FileStreams;

                while (option != MenuOptions.Exit)
                {
                    Console.Clear();
                    view.DisplayMenu();
                    option = view.GetMenuChoice();
                    Console.Clear();
                    switch (option)
                    {
                        case MenuOptions.Task1FileStreams:
                            RunTask1(fileProcessor, weatherFilePath1, processedFilePath);
                            break;
                        case MenuOptions.Task2AsyncFileStreams:
                            await RunTask2(fileProcessorAsync, weatherFilePath1, processedFilePath);
                            break;
                        case MenuOptions.Exit:
                            return;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(option));
                    }

                    view.Pause();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
            }

            Console.ReadKey();
        }

        private static void RunTask1(FileProcessor readFile, string sourceFilePath, string destinationFilePath)
        {
            Stopwatch sw1 = Stopwatch.StartNew();
            sw1.Start();
            for (int i = 2; i <= 512; i *= 2)
            {
                readFile.FileStreamRead(sourceFilePath, i * 1024);
                readFile.FileBufferedStream(sourceFilePath, i * 1024);
                readFile.PrintCurrentElapsedTimeDifference();
                ConsoleHelpers.PrintLine();
                Console.WriteLine();
            }

            Console.WriteLine("Process and save weather statistics:");
            readFile.FileBufferedStream(sourceFilePath, 64 * 1024, destinationFilePath);
            sw1.Stop();
            Console.WriteLine($"Total time for task 1: {sw1.ElapsedMilliseconds}");
        }

        private static async Task RunTask2(FileProcessorAsync fileProcessor, string sourceFilePath, string destinationFilePath)
        {
            Stopwatch sw1 = Stopwatch.StartNew();
            for (int i = 2; i <= 512; i *= 2)
            {
                await fileProcessor.FileStreamRead(sourceFilePath, i * 1024);
                await fileProcessor.FileBufferedStream(sourceFilePath, i * 1024);
                fileProcessor.PrintCurrentElapsedTimeDifference();
                ConsoleHelpers.PrintLine();
                Console.WriteLine();
            }

            Console.WriteLine("Process and save weather statistics:");
            await fileProcessor.FileBufferedStream(sourceFilePath, 64 * 1024, destinationFilePath);
            sw1.Stop();
            Console.WriteLine($"Total time for task 1: {sw1.ElapsedMilliseconds}");
        }
    }
}