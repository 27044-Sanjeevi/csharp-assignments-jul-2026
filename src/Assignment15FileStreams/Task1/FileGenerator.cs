using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams.Task1
{
    /// <summary>
    /// Represents the synchronous file generator.
    /// </summary>
    internal class FileGenerator
    {
        /// <summary>
        /// Generates a weather file of given target size.
        /// </summary>
        /// <param name="filePath">Path of the file to be stored.</param>
        /// <param name="targertByteSize">Size of the file.</param>
        public void GenerateWeatherFile(string filePath, long targertByteSize)
        {
            if (this.CheckFileExistence(filePath, targertByteSize))
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

                        streamWriter.Write(dataLine);

                        currentSizeBytes += Encoding.UTF8.GetByteCount(dataLine);
                    }

                    streamWriter.Flush();
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"File generated Successfully: {filePath}.");
            Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.TotalMilliseconds}");
        }

        /// <summary>
        /// Checks if the file already exists.
        /// </summary>
        /// <param name="filePath">Path of the file for existence check.</param>
        /// <param name="targertByteSize">Target size of the file.</param>
        /// <returns>true if the file exists; otherwise false.</returns>
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
