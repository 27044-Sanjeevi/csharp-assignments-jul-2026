using System;
using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams
{
    internal class FileProcessor
    {
        private double _fileStreamElapsedTime;
        private double _bufferedStreammTime;

        private string _remainingText = string.Empty;
        private double _minTemperature = double.MaxValue;
        private double _maxTemperature = double.MinValue;
        private double _averageTemperature = 0;
        private int _totalProcessedRows = 0;

        public void FileStreamRead(string sourceFilePath, int bufferSize, string? destinationFilePath = null)
        {
            this.ResetState();

            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            Stopwatch sw = Stopwatch.StartNew();
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

            sw.Stop();

            this._fileStreamElapsedTime = sw.Elapsed.TotalMilliseconds;
            this.PrintTimeElapsed("File Stream", this._fileStreamElapsedTime, bufferSize);

            if (destinationFilePath != null)
            {
                this.SaveProcessedData(destinationFilePath);
            }
        }

        public void FileBufferedStream(string filePath, int bufferSize, string? destinationFilePath = null)
        {
            this.ResetState();

            Stopwatch sw = Stopwatch.StartNew();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
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

            sw.Stop();

            this._bufferedStreammTime = sw.Elapsed.TotalMilliseconds;
            this.PrintTimeElapsed("Buffered Stream", this._bufferedStreammTime, bufferSize);

            if (destinationFilePath != null)
            {
                this.SaveProcessedData(destinationFilePath);
            }
        }

        /// <summary>
        /// Processes the data chunk and returns false if processing should terminate early.
        /// </summary>
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

        public void PrintCurrentElapsedTimeDifference()
        {
            Console.WriteLine($"FileStream Time - BufferedStream Time = {this._fileStreamElapsedTime - this._bufferedStreammTime:F2} ms");
        }

        private void PrintTimeElapsed(string methodName, double timeInMs, int bufferSize)
        {
            Console.WriteLine($"Method: {methodName} | Buffer size: {bufferSize} bytes | Elapsed Time: {timeInMs:F2} ms");
        }
    }
}
