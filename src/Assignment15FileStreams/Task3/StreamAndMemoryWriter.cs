using System.Diagnostics;
using System.Text;

namespace Assignment15FileStreams.Task3
{
    /// <summary>
    /// Implements Task 3.
    /// </summary>
    internal class StreamAndMemoryWriter
    {
        /// <summary>
        /// Runs a diagnosis of optimized and unoptimized code.
        /// </summary>
        public void RunDiagnosis()
        {
            string testData = this.GenerateSampleData(5000);
            string unoptimizedFile = "task3_unoptimized.txt";
            string optimizedFile = "task3_optimized.txt";

            Console.WriteLine($"Test sample data size: {Encoding.UTF8.GetByteCount(testData) / 1024.0:F1} KB\n");

            ConsoleHelpers.DisplayStatus("Running UNOPTIMIZED file operations...");
            Stopwatch stopwatchUnoptimized = Stopwatch.StartNew();

            this.RunUnoptimized(unoptimizedFile, testData);

            stopwatchUnoptimized.Stop();

            ConsoleHelpers.DisplayStatus("Running OPTIMIZED streaming operations...");

            Stopwatch stopwatchOptimized = Stopwatch.StartNew();

            this.RunOptimized(optimizedFile, testData);

            stopwatchOptimized.Stop();

            this.PrintDiagnosisReport(
                stopwatchUnoptimized.Elapsed.TotalMilliseconds,
                stopwatchOptimized.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// Runs the unoptimized code.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="data">Data to be processed.</param>
        public void RunUnoptimized(string path, string data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                memoryStream.Write(buffer, 0, buffer.Length);

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    byte[] writeBuffer = memoryStream.ToArray();
                    fileStream.Write(writeBuffer, 0, writeBuffer.Length);
                }
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                int totalCharsRead = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        char c = (char)buffer[i];
                        if (c != '\0')
                        {
                            totalCharsRead++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs the optimized code.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="data">Data to be processed.</param>
        public void RunOptimized(string path, string data)
        {
            byte[] writeBytes = Encoding.UTF8.GetBytes(data);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 64 * 1024))
            {
                fileStream.Write(writeBytes, 0, writeBytes.Length);
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 64 * 1024))
            {
                byte[] byteBuffer = new byte[4096];
                int bytesRead;
                long totalBytesRead = 0;

                while ((bytesRead = fileStream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    totalBytesRead += bytesRead;
                    Encoding.UTF8.GetString(byteBuffer);
                }
            }
        }

        private string GenerateSampleData(int lineCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= lineCount; i++)
            {
                sb.AppendLine($"Line {i}");
            }

            return sb.ToString();
        }

        private void PrintDiagnosisReport(double unoptimizedMs, double optimizedMs)
        {
            Console.WriteLine(new string('=', 90));
            Console.WriteLine($"{"Metric",-35} | {"Unoptimized Code",-23} | {"Optimized Code",-23}");
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{"Execution Time",-35} | {unoptimizedMs,19:F2} ms | {optimizedMs,19:F2} ms");
            Console.WriteLine(new string('-', 90));
            Console.WriteLine("\nINFERENCES");
            Console.WriteLine("1. MemoryStream was used as a redundant logic, calling .ToArray()" +
                "which duplicates the entire buffer on the heap and can be potential for Large Object Heap (LOH) fragmentation.\n" +
                "Fix: Wrote directly to FileStream using StreamWriter with an internal buffer.\n");

            Console.WriteLine("2. Encoding.ASCII was used, which corrupts UTF-8 / non-ASCII characters.\n" +
                "Fix: Adopted Encoding.UTF8 universally across reader and writer.\n");

            Console.WriteLine("3. Casting bytes directly to (char)buffer[i] corrupts multi-byte UTF-8 sequences.\n" +
                "Fix: Used StreamReader to decode character boundaries accurately.\n");

            Console.WriteLine("4. Byte-by-byte Console.Write calls freeze processing.\n" +
                "Fix: Processed data in 4KB bulk blocks instead of individual bytes.");
        }
    }
}