using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams.Task2
{
    /// <summary>
    /// Implements Task 2.
    /// </summary>
    internal class FileProcessorAsync
    {
        /// <summary>
        /// Compares the asynchronous and synchronous operations.
        /// </summary>
        /// <param name="filePaths">Paths of multiple files.</param>
        /// <param name="destinationFolder">The folder for destination.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous file generation operation.</returns>
        public async Task CompareSyncVsAsync(string[] filePaths, string destinationFolder)
        {
            foreach (string path in filePaths)
            {
                if (!File.Exists(path))
                {
                    ConsoleHelpers.DisplayFailure($"Required file not found: {path}.");
                    return;
                }
            }

            Directory.CreateDirectory(destinationFolder);

            ConsoleHelpers.DisplayStatus("Phase 1: Running SEQUENTIAL SYNCHRONOUS processing across files...");
            Stopwatch syncSw = Stopwatch.StartNew();
            List<WeatherFileStats> syncResults = new List<WeatherFileStats>();

            foreach (string file in filePaths)
            {
                string dest = $"Sync_Report_{Path.GetFileName(file)}";
                WeatherFileStats stats = this.ProcessFileSynchronous(file, dest);
                syncResults.Add(stats);
            }

            syncSw.Stop();
            double syncTotalMs = syncSw.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Sequential Synchronous Total Elapsed Time: {syncTotalMs:F1} ms\n");

            ConsoleHelpers.DisplayStatus("Phase 2: Running CONCURRENT ASYNCHRONOUS processing (Task.WhenAll) across files...");
            Stopwatch asyncSw = Stopwatch.StartNew();

            List<Task<WeatherFileStats>> asyncTasks = new List<Task<WeatherFileStats>>();
            foreach (string file in filePaths)
            {
                string dest = $"Async_Report_{Path.GetFileName(file)}";
                asyncTasks.Add(this.ProcessFileAsync(file, dest));
            }

            WeatherFileStats[] asyncResults = await Task.WhenAll(asyncTasks);
            asyncSw.Stop();
            double asyncTotalMs = asyncSw.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Concurrent Asynchronous Total Elapsed Time: {asyncTotalMs:F1} ms\n");

            this.PrintComparisonReport(syncTotalMs, asyncTotalMs, filePaths.Length, asyncResults);
        }

        /// <summary>
        /// Process a file asynchronously.
        /// </summary>
        /// <param name="sourceFilePath">The file to read.</param>
        /// <param name="destinationFilePath">The destination path to store the processed text.</param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <returns>A Task that represents the asynchronous file processing.</returns>
        public async Task<WeatherFileStats> ProcessFileAsync(string sourceFilePath, string destinationFilePath, int bufferSize = 64 * 1024)
        {
            WeatherFileStats stats = new WeatherFileStats { FilePath = sourceFilePath };
            Stopwatch sw = Stopwatch.StartNew();

            FileStreamOptions options = new FileStreamOptions
            {
                Mode = FileMode.Open,
                Access = FileAccess.Read,
                Share = FileShare.Read,
                BufferSize = bufferSize,
                Options = FileOptions.Asynchronous | FileOptions.SequentialScan,
            };

            using (FileStream fs = new FileStream(sourceFilePath, options))
            {
                using (BufferedStream bs = new BufferedStream(fs, bufferSize))
                {
                    using (StreamReader reader = new StreamReader(bs, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: bufferSize))
                    {
                        string? line;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            this.UpdateStats(stats, line);
                        }
                    }
                }
            }

            sw.Stop();
            stats.ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds;

            await this.SaveReportAsync(stats, destinationFilePath);
            return stats;
        }

        /// <summary>
        /// Synchronous processing for a single file.
        /// </summary>
        /// <param name="sourceFilePath">The file to read.</param>
        /// <param name="destinationFilePath">The destination path to store the processed text.</param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <returns>A Task that represents the asynchronous file processing.</returns>
        public WeatherFileStats ProcessFileSynchronous(string sourceFilePath, string destinationFilePath, int bufferSize = 64 * 1024)
        {
            WeatherFileStats stats = new WeatherFileStats { FilePath = sourceFilePath };
            Stopwatch sw = Stopwatch.StartNew();

            using (FileStream fs = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
            {
                using (BufferedStream bs = new BufferedStream(fs, bufferSize))
                {
                    using (StreamReader reader = new StreamReader(bs, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: bufferSize))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            this.UpdateStats(stats, line);
                        }
                    }
                }
            }

            sw.Stop();
            stats.ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds;

            this.SaveReportSynchronous(stats, destinationFilePath);
            return stats;
        }

        private async Task SaveReportAsync(WeatherFileStats stats, string destinationFilePath)
        {
            string reportText = this.BuildReportString(stats);
            byte[] reportBytes = Encoding.UTF8.GetBytes(reportText);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await memoryStream.WriteAsync(reportBytes, 0, reportBytes.Length);
                memoryStream.Position = 0;

                FileStreamOptions writeOptions = new FileStreamOptions
                {
                    Mode = FileMode.Create,
                    Access = FileAccess.Write,
                    Share = FileShare.None,
                    BufferSize = 64 * 1024,
                    Options = FileOptions.Asynchronous,
                };

                using (FileStream destinationStream = new FileStream(destinationFilePath, writeOptions))
                {
                    await memoryStream.CopyToAsync(destinationStream);
                }
            }
        }

        private void SaveReportSynchronous(WeatherFileStats stats, string destinationFilePath)
        {
            string reportText = this.BuildReportString(stats);
            byte[] reportBytes = Encoding.UTF8.GetBytes(reportText);

            using (MemoryStream memoryStream = new MemoryStream(reportBytes))
            {
                using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 64 * 1024))
                {
                    memoryStream.CopyTo(destinationStream);
                }
            }
        }

        private void UpdateStats(WeatherFileStats stats, string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            string[] components = line.Split(',');
            if (components.Length < 3)
            {
                return;
            }

            if (double.TryParse(components[2].Trim(), out double temperature))
            {
                if (temperature < stats.MinTemperature)
                {
                    stats.MinTemperature = temperature;
                }

                if (temperature > stats.MaxTemperature)
                {
                    stats.MaxTemperature = temperature;
                }

                long previousTotal = stats.TotalProcessedRows;
                stats.TotalProcessedRows++;

                stats.AverageTemperature = ((stats.AverageTemperature * previousTotal) + temperature) / stats.TotalProcessedRows;
            }
        }

        private string BuildReportString(WeatherFileStats stats)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Source File            : {Path.GetFileName(stats.FilePath)}");
            sb.AppendLine($"Total Processed Rows   : {stats.TotalProcessedRows}");
            sb.AppendLine($"Maximum Temperature    : {stats.MaxTemperature:F2} C");
            sb.AppendLine($"Minimum Temperature    : {stats.MinTemperature:F2} C");
            sb.AppendLine($"Weighted Average Temp  : {stats.AverageTemperature:F2} C");
            sb.AppendLine($"Processing Time        : {stats.ElapsedMilliseconds:F2} ms");
            return sb.ToString();
        }

        private void PrintComparisonReport(double syncMs, double asyncMs, int fileCount, WeatherFileStats[] results)
        {
            Console.WriteLine($"Total Files Processed     : {fileCount}");
            Console.WriteLine($"Sequential Sync Duration  : {syncMs:F2} ms");
            Console.WriteLine($"Concurrent Async Duration : {asyncMs:F2} ms");
            Console.WriteLine($"Performance Variance      : {syncMs - asyncMs:F2} ms");
        }
    }
}