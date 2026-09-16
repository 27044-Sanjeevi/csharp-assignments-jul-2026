using System;
using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams.Task1
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
            ResetState();

            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            Stopwatch sw = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
            {
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (!ProcessChunk(buffer, bytesRead, destinationFilePath))
                    {
                        continue;
                    }
                }

                if (destinationFilePath != null)
                {
                    ProcessFinalRemainingLine();
                }
            }

            sw.Stop();

            _fileStreamElapsedTime = sw.Elapsed.TotalMilliseconds;
            PrintTimeElapsed("File Stream", _fileStreamElapsedTime, bufferSize);

            if (destinationFilePath != null)
            {
                SaveProcessedData(destinationFilePath);
            }
        }

        public void FileBufferedStream(string filePath, int bufferSize, string? destinationFilePath = null)
        {
            ResetState();

            Stopwatch sw = Stopwatch.StartNew();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, 64 * 1024))
                {
                    while ((bytesRead = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (!ProcessChunk(buffer, bytesRead, destinationFilePath))
                        {
                            continue;
                        }
                    }

                    if (destinationFilePath != null)
                    {
                        ProcessFinalRemainingLine();
                    }
                }
            }

            sw.Stop();

            _bufferedStreammTime = sw.Elapsed.TotalMilliseconds;
            PrintTimeElapsed("Buffered Stream", _bufferedStreammTime, bufferSize);

            if (destinationFilePath != null)
            {
                SaveProcessedData(destinationFilePath);
            }
        }

        /// <summary>
        /// Processes the data chunk and returns false if processing should terminate early.
        /// </summary>
        private bool ProcessChunk(byte[] chunk, int bytesRead, string? destinationFilePath = null)
        {
            string text = _remainingText + Encoding.UTF8.GetString(chunk, 0, bytesRead);

            if (destinationFilePath == null)
            {
                return false;
            }

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int processLimit = lines.Length;

            if (text.EndsWith("\n") || text.EndsWith("\r"))
            {
                _remainingText = string.Empty;
            }
            else
            {
                _remainingText = lines[lines.Length - 1];
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
                    if (temperature < _minTemperature)
                    {
                        _minTemperature = temperature;
                    }

                    if (temperature > _maxTemperature)
                    {
                        _maxTemperature = temperature;
                    }

                    chunkTotalTemperature += temperature;
                    processedRows++;
                }
            }

            if (processedRows > 0)
            {
                double currentChunkAverage = chunkTotalTemperature / processedRows;
                long previousTotalRows = _totalProcessedRows;
                _totalProcessedRows += processedRows;

                _averageTemperature = (_averageTemperature * previousTotalRows + currentChunkAverage * processedRows) / _totalProcessedRows;
            }

            return true;
        }

        private void ProcessFinalRemainingLine()
        {
            if (string.IsNullOrWhiteSpace(_remainingText))
            {
                return;
            }

            string[] components = _remainingText.Split(',');
            if (components.Length >= 3 && double.TryParse(components[2].Trim(), out double temperature))
            {
                if (temperature < _minTemperature)
                {
                    _minTemperature = temperature;
                }

                if (temperature > _maxTemperature)
                {
                    _maxTemperature = temperature;
                }

                long previousTotalRows = _totalProcessedRows;
                _totalProcessedRows++;
                _averageTemperature = (_averageTemperature * previousTotalRows + temperature) / _totalProcessedRows;
            }

            _remainingText = string.Empty;
        }

        private void SaveProcessedData(string destinationFilePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("WEATHER DATA STATISTICS SUMMARY REPORT");
            stringBuilder.AppendLine($"Total Processed Entries: {_totalProcessedRows}");
            stringBuilder.AppendLine($"Maximum Temperature    : {_maxTemperature:F2}°C");
            stringBuilder.AppendLine($"Minimum Temperature    : {_minTemperature:F2}°C");
            stringBuilder.AppendLine($"Weighted Average Temp  : {_averageTemperature:F2}°C");
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
            _minTemperature = double.MaxValue;
            _maxTemperature = double.MinValue;
            _averageTemperature = 0;
            _totalProcessedRows = 0;
            _remainingText = string.Empty;
        }

        public void PrintCurrentElapsedTimeDifference()
        {
            Console.WriteLine($"FileStream Time - BufferedStream Time = {_fileStreamElapsedTime - _bufferedStreammTime:F2} ms");
        }

        private void PrintTimeElapsed(string methodName, double timeInMs, int bufferSize)
        {
            Console.WriteLine($"Method: {methodName} | Buffer size: {bufferSize} bytes | Elapsed Time: {timeInMs:F2} ms");
        }
    }
}
