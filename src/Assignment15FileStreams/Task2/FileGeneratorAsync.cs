using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment15FileStreams.Task2
{
    internal class FileGeneratorAsync
    {
        public async Task GenerateWeatherFileAsync(string filePath, long targertByteSize)
        {
            if (CheckFileExistence(filePath, targertByteSize))
            {
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            int fileStreamBufferSize = 64 * 1024; // 64KB

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, fileStreamBufferSize))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    long currentSizeBytes = 0;
                    Random random = new Random();
                    string[] cities = { "Coimbatore", "Chennai", "Tiruppur", "Erode", "Palani", "Madurai" };

                    while (currentSizeBytes < targertByteSize)
                    {
                        double temperature = random.NextDouble() * 40.0;
                        string city = cities[random.Next(0, cities.Length)];
                        DateTime dateTime = DateTime.Now;
                        string dataLine = $"{dateTime:dd-MM-yyyy HH:mm:ss}, {city}, {temperature:F2}\n";

                        await streamWriter.WriteAsync(dataLine);

                        currentSizeBytes += Encoding.UTF8.GetByteCount(dataLine);
                    }

                    await streamWriter.FlushAsync();
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"File generated Successfully: {filePath}.");
            Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.TotalMilliseconds}");
        }

        public bool CheckFileExistence(string filePath, long targertByteSize)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} already exists.");
                return true;
            }

            return false;
        }
    }
}
