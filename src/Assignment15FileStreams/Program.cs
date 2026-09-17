using System.Diagnostics;
using Assignment15FileStreams;
using Assignment15FileStreams.Menu;
using Assignment15FileStreams.Task1;
using Assignment15FileStreams.Task2;
using Assignment15FileStreams.Task3;
using Assignment15FileStreams.Task4;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int OneKBInBytes = 1024;
        private const int OneGBInBytes = OneKBInBytes * OneKBInBytes * OneKBInBytes;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous file generation operation.</returns>
        internal static async Task Main()
        {
            try
            {
                FileProcessor fileProcessor = new FileProcessor();
                FileProcessorAsync fileProcessorAsync = new FileProcessorAsync();
                LoadTester loadTester = new LoadTester();
                StreamAndMemoryWriter streamAndMemoryWriter = new StreamAndMemoryWriter();

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
                            RunTask1(fileProcessor);
                            break;
                        case MenuOptions.Task2AsyncFileStreams:
                            await RunTask2(fileProcessorAsync);
                            break;
                        case MenuOptions.Task3StreamMemoryWriterOptimization:
                            RunTask3(streamAndMemoryWriter);
                            break;
                        case MenuOptions.Task4Logger:
                            RunTask4(loadTester);
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

        private static void RunTask1(FileProcessor readFile)
        {
            ConsoleHelpers.DisplayTitle("Task 1: Synchronous file operations.");
            string weatherFilePath1 = "Weather1.txt";

            const string processedFilePath = "WeatherStatistics.txt";

            FileGenerator generator = new FileGenerator();

            generator.GenerateWeatherFile(weatherFilePath1, OneGBInBytes);

            Stopwatch sw1 = Stopwatch.StartNew();
            sw1.Start();
            for (int i = 4; i <= 256; i *= 2)
            {
                readFile.FileStreamRead(weatherFilePath1, i * OneKBInBytes);
                readFile.FileBufferedStream(weatherFilePath1, i * OneKBInBytes);
                readFile.PrintCurrentElapsedTimeDifference();
                ConsoleHelpers.PrintLine();
            }

            Console.WriteLine("Process and save weather statistics:");
            readFile.FileBufferedStream(weatherFilePath1, 64 * OneKBInBytes, processedFilePath);
            sw1.Stop();
            Console.WriteLine($"Total time for task 1: {sw1.ElapsedMilliseconds}");
        }

        private static async Task RunTask2(FileProcessorAsync fileProcessorAsync)
        {
            ConsoleHelpers.DisplayTitle("Task 2: Synchronous Sequential vs Asynchronous Concurrent File Processing");

            Stopwatch sw1 = Stopwatch.StartNew();

            string weatherFilePath1 = "Weather1.txt";
            string weatherFilePath2 = "Weather2.txt";
            string weatherFilePath3 = "Weather3.txt";

            const string processedFolderPath = "WeatherStatisticsAsync";

            FileGeneratorAsync generator = new FileGeneratorAsync();

            await Task.WhenAll(
                generator.GenerateWeatherFileAsync(weatherFilePath1, OneGBInBytes),
                generator.GenerateWeatherFileAsync(weatherFilePath2, OneGBInBytes),
                generator.GenerateWeatherFileAsync(weatherFilePath3, OneGBInBytes));

            string[] sourceFiles = new string[] { weatherFilePath1, weatherFilePath2, weatherFilePath3 };

            await fileProcessorAsync.CompareSyncVsAsync(sourceFiles, processedFolderPath);

            sw1.Stop();
            Console.WriteLine($"Total time for task 2: {sw1.ElapsedMilliseconds}");
        }

        private static void RunTask3(StreamAndMemoryWriter streamAndMemoryWriter)
        {
            ConsoleHelpers.DisplayTitle("Task 3: Identifying issues in Basic File usage");
            streamAndMemoryWriter.RunDiagnosis();
        }

        private static void RunTask4(LoadTester loadTester)
        {
            loadTester.RunLoadTest();
        }
    }
}