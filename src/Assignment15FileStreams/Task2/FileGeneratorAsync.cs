using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams.Task2
{
    /// <summary>
    /// Represents the asynchronous file generator.
    /// </summary>
    internal class FileGeneratorAsync
    {
        /// <summary>
        /// Generates a weather file asynchronously.
        /// </summary>
        /// <param name="filePath">The path of the file to generate the weather data.</param>
        /// <param name="targetByteSize">The target size of the file to be generated in bytes.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous file generation operation.</returns>
        public async Task GenerateWeatherFileAsync(string filePath, long targetByteSize)
        {
            if (this.CheckFileExistence(filePath, targetByteSize))
            {
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            int fileStreamBufferSize = 64 * 1024;

            using (FileStream fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                fileStreamBufferSize,
                FileOptions.Asynchronous))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8, fileStreamBufferSize))
                {
                    long currentSizeBytes = 0;
                    Random random = new Random();
                    string[] cities = { "Coimbatore", "Chennai", "Tiruppur", "Erode", "Palani", "Madurai" };

                    while (currentSizeBytes < targetByteSize)
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

            Console.WriteLine($"Asynchronous File generated Successfully: {filePath}.");
            Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.TotalMilliseconds:F2} ms");
        }

        /// <summary>
        /// Verifies whether the specified file exists and matches the required byte size target.
        /// </summary>
        /// <param name="filePath">The path of the file to generate the weather data.</param>
        /// <param name="targetByteSize">The target size of the file to be generated in bytes.</param>
        /// <returns>true if the file exists; otherwise false.</returns>
        public bool CheckFileExistence(string filePath, long targetByteSize)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length >= targetByteSize)
                {
                    Console.WriteLine($"File {filePath} already exists and is complete ({fileInfo.Length / (1024.0 * 1024.0 * 1024.0):F2} GB). Skipping generation.");
                    return true;
                }

                Console.WriteLine($"File {filePath} exists but is incomplete or truncated. Re-generating...");
                return false;
            }

            return false;
        }
    }
}
