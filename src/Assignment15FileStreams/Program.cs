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
                    try
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
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine($"\n[FILE NOT FOUND] : {ex.Message}");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"\n[IO EXCEPTION] : {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"\n[ARGUMENT OUT OF RANGE] : {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[EXCEPTION] : {ex.Message}");
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
            const int LowerBufferLimitKB = 4;
            const int UpperBufferLimitKB = 256;
            const string processedFilePath = "WeatherStatistics.txt";
            const string weatherFilePath1 = "Weather1.txt";

            ConsoleHelpers.DisplayTitle("Task 1: Synchronous file operations.");

            FileGenerator generator = new FileGenerator();
            generator.GenerateWeatherFile(weatherFilePath1, OneGBInBytes);

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = LowerBufferLimitKB; i <= UpperBufferLimitKB; i *= 2)
            {
                readFile.FileStreamRead(weatherFilePath1, i * OneKBInBytes);
                readFile.FileBufferedStream(weatherFilePath1, i * OneKBInBytes);
                readFile.PrintCurrentElapsedTimeDifference();
                ConsoleHelpers.PrintLine();
            }

            Console.WriteLine("Process and save weather statistics:");
            readFile.FileBufferedStream(weatherFilePath1, 64 * OneKBInBytes, processedFilePath);
            stopwatch.Stop();
            Console.WriteLine($"Total time for task 1: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static async Task RunTask2(FileProcessorAsync fileProcessorAsync)
        {
            ConsoleHelpers.DisplayTitle("Task 2: Synchronous Sequential vs Asynchronous Concurrent File Processing");

            Stopwatch stopwatch = Stopwatch.StartNew();

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

            stopwatch.Stop();
            Console.WriteLine($"Total time for task 2: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void RunTask3(StreamAndMemoryWriter streamAndMemoryWriter)
        {
            ConsoleHelpers.DisplayTitle("Task 3: Identifying issues in Basic File usage");
            streamAndMemoryWriter.RunDiagnosis();
        }

        private static void RunTask4(LoadTester loadTester)
        {
            ConsoleHelpers.DisplayTitle($"Task 4: Multi-User Logging Load Test");

            loadTester.RunLoadTest();
        }
    }
}