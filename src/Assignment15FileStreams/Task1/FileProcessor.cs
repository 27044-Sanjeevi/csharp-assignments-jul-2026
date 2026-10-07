using System.Diagnostics;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Assignment15FileStreams.Task1
{
    /// <summary>
    /// Implements the file processing for task 1.
    /// </summary>
    internal class FileProcessor
    {
        private double _fileStreamElapsedTime;
        private double _bufferedStreamTime;

        private string _remainingText = string.Empty;
        private double _minTemperature = double.MaxValue;
        private double _maxTemperature = double.MinValue;
        private double _averageTemperature = 0;
        private int _totalProcessedRows = 0;

        /// <summary>
        /// Reads the file <paramref name="sourceFilePath"/> using FileStream.
        /// </summary>
        /// <param name="sourceFilePath">The file to read.</param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <param name="destinationFilePath">The destination path to store the processed text.</param>
        public void FileStreamRead(string sourceFilePath, int bufferSize, string? destinationFilePath = null)
        {
            this.ResetState();

            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            Stopwatch stopwatch = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
            {
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (!this.ProcessChunk(buffer, bytesRead, destinationFilePath))
                    {
                        continue;
                    }
                }

                if (destinationFilePath != null)
                {
                    this.ProcessFinalRemainingLine();
                }
            }

            stopwatch.Stop();

            this._fileStreamElapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            this.PrintTimeElapsed("File Stream", this._fileStreamElapsedTime, bufferSize);

            if (destinationFilePath != null)
            {
                this.SaveProcessedData(destinationFilePath);
            }
        }

        /// <summary>
        /// Reads the file <paramref name="sourceFilePath"/> using BufferedStream.
        /// </summary>
        /// <param name="sourceFilePath">The file to read.</param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <param name="destinationFilePath">The destination path to store the processed text.</param>
        public void FileBufferedStream(string sourceFilePath, int bufferSize, string? destinationFilePath = null)
        {
            this.ResetState();

            Stopwatch stopwatch = Stopwatch.StartNew();
            using (FileStream fileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, 64 * 1024))
                {
                    while ((bytesRead = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (!this.ProcessChunk(buffer, bytesRead, destinationFilePath))
                        {
                            continue;
                        }
                    }

                    if (destinationFilePath != null)
                    {
                        this.ProcessFinalRemainingLine();
                    }
                }
            }

            stopwatch.Stop();

            this._bufferedStreamTime = stopwatch.Elapsed.TotalMilliseconds;
            this.PrintTimeElapsed("Buffered Stream", this._bufferedStreamTime, bufferSize);

            if (destinationFilePath != null)
            {
                this.SaveProcessedData(destinationFilePath);
            }
        }

        /// <summary>
        /// Prints the current elapsed time difference of file stream and buffered stream methods.
        /// </summary>
        public void PrintCurrentElapsedTimeDifference()
        {
            double elapsedDifference = this._fileStreamElapsedTime - this._bufferedStreamTime;

            if (elapsedDifference > 0)
            {
                double percentageFaster = ((this._fileStreamElapsedTime - this._bufferedStreamTime) / this._fileStreamElapsedTime) * 100;
                ConsoleHelpers.DisplayResult($"BufferedStream is faster than FileStream by {percentageFaster:F2}% ({elapsedDifference:F2} ms)");
            }
            else if (this._bufferedStreamTime > this._fileStreamElapsedTime)
            {
                double percentageFaster = ((this._bufferedStreamTime - this._fileStreamElapsedTime) / this._bufferedStreamTime) * 100;
                ConsoleHelpers.DisplayResult($"FileStream is faster than BufferedStream by {percentageFaster:F2}% ({-elapsedDifference:F2}) ms");
            }
            else
            {
                ConsoleHelpers.DisplayResult("Both streams performed at exactly the same speed.");
            }
        }

        private bool ProcessChunk(byte[] chunk, int bytesRead, string? destinationFilePath = null)
        {
            string text = this._remainingText + Encoding.UTF8.GetString(chunk, 0, bytesRead);

            if (destinationFilePath == null)
            {
                return false;
            }

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int processLimit = lines.Length;

            if (text.EndsWith("\n") || text.EndsWith("\r"))
            {
                this._remainingText = string.Empty;
            }
            else
            {
                this._remainingText = lines[lines.Length - 1];
                processLimit -= 1;
            }

            double chunkTotalTemperature = 0;
            int processedRows = 0;

            for (int i = 0; i < processLimit; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] components = line.Split(',');
                if (components.Length < 3)
                {
                    continue;
                }

                if (double.TryParse(components[2].Trim(), out double temperature))
                {
                    if (temperature < this._minTemperature)
                    {
                        this._minTemperature = temperature;
                    }

                    if (temperature > this._maxTemperature)
                    {
                        this._maxTemperature = temperature;
                    }

                    chunkTotalTemperature += temperature;
                    processedRows++;
                }
            }

            if (processedRows > 0)
            {
                double currentChunkAverage = chunkTotalTemperature / processedRows;
                long previousTotalRows = this._totalProcessedRows;
                this._totalProcessedRows += processedRows;

                this._averageTemperature = ((this._averageTemperature * previousTotalRows) + (currentChunkAverage * processedRows)) / this._totalProcessedRows;
            }

            return true;
        }

        private void ProcessFinalRemainingLine()
        {
            if (string.IsNullOrWhiteSpace(this._remainingText))
            {
                return;
            }

            string[] components = this._remainingText.Split(',');
            if (components.Length >= 3 && double.TryParse(components[2].Trim(), out double temperature))
            {
                if (temperature < this._minTemperature)
                {
                    this._minTemperature = temperature;
                }

                if (temperature > this._maxTemperature)
                {
                    this._maxTemperature = temperature;
                }

                long previousTotalRows = this._totalProcessedRows;
                this._totalProcessedRows++;
                this._averageTemperature = ((this._averageTemperature * previousTotalRows) + temperature) / this._totalProcessedRows;
            }

            this._remainingText = string.Empty;
        }

        private void SaveProcessedData(string destinationFilePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("WEATHER DATA STATISTICS SUMMARY REPORT");
            stringBuilder.AppendLine($"Total Processed Entries: {this._totalProcessedRows}");
            stringBuilder.AppendLine($"Maximum Temperature    : {this._maxTemperature:F2}°C");
            stringBuilder.AppendLine($"Minimum Temperature    : {this._minTemperature:F2}°C");
            stringBuilder.AppendLine($"Weighted Average Temp  : {this._averageTemperature:F2}°C");
            byte[] processedDataBytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            using (MemoryStream memoryStream = new MemoryStream(processedDataBytes))
            {
                using (FileStream fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }

            Console.WriteLine($"Analysis summary written to: {destinationFilePath}");
        }

        private void ResetState()
        {
            this._minTemperature = double.MaxValue;
            this._maxTemperature = double.MinValue;
            this._averageTemperature = 0;
            this._totalProcessedRows = 0;
            this._remainingText = string.Empty;
        }

        private void PrintTimeElapsed(string methodName, double timeInMs, int bufferSize)
        {
            Console.WriteLine($"Method: {methodName} | Buffer size: {bufferSize} bytes | Elapsed Time: {timeInMs:F2} ms");
        }
    }
}
