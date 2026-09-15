using System.Runtime.CompilerServices;
using Assignment15FileStreams;

namespace Assignments
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            const string weatherFilePath = "Weather.txt";
            FileGenerator generator = new FileGenerator();
            generator.GenerateWeatherFile(weatherFilePath, 1024 * 1024 * 1024);
            ReadFile readFile = new ReadFile();

            readFile.FileStreamRead(weatherFilePath, 1024 * 1024);
            readFile.FileStreamRead(weatherFilePath, 512 * 1024);
            readFile.FileStreamRead(weatherFilePath, 256 * 1024);
            readFile.FileStreamRead(weatherFilePath, 128 * 1024);
            readFile.FileStreamRead(weatherFilePath, 64 * 1024);
            readFile.FileStreamRead(weatherFilePath, 32 * 1024);
            readFile.FileStreamRead(weatherFilePath, 16 * 1024);
            readFile.FileStreamRead(weatherFilePath, 8 * 1024);

            Console.WriteLine();
            readFile.FileBufferedStream(weatherFilePath, 1024 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 512 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 256 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 128 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 64 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 32 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 16 * 1024);
            readFile.FileBufferedStream(weatherFilePath, 8 * 1024);
            Console.ReadKey();
        }
    }
}