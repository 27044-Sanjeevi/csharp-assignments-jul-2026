using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment15FileStreams
{
    internal class FileGenerator
    {
        public void GenerateWeatherFile(string filePath, long targertByteSize)
        {
            if (this.CheckFileExistence(filePath, targertByteSize))
            {
                return;
            }

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
