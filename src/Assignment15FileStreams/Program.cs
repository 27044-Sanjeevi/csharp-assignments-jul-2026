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

                FileGeneratorAsync generator = new FileGeneratorAsync();

                await generator.GenerateWeatherFileAsync(weatherFilePath1, 1024 * 1024 * 1024);
                await generator.GenerateWeatherFileAsync(weatherFilePath2, 1024 * 1024 * 1024);
                await generator.GenerateWeatherFileAsync(weatherFilePath3, 1024 * 1024 * 1024);

                FileProcessor fileProcessor = new FileProcessor();

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
                            RunTask2();
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
        }

        private static void RunTask2()
        {

        }
    }
}