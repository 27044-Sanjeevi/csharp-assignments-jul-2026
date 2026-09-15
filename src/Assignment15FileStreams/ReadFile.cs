using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment15FileStreams
{
    internal class ReadFile
    {
        public void FileStreamRead(string filePath, int bufferSize)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            byte[] buffer = new byte[bufferSize];
            Stopwatch sw = Stopwatch.StartNew();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize))
            {
                while (fs.Read(buffer, 0, buffer.Length) != 0)
                {
                    continue;
                }
            }

            sw.Stop();
            this.PrintTimeElapsed("File Stream", sw.Elapsed.TotalMilliseconds, bufferSize);
        }

        public void FileBufferedStream(string filePath, int bufferSize)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize))
            {
                byte[] buffer = new byte[bufferSize];
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
                {
                    while (bufferedStream.Read(buffer, 0, bufferSize) != 0)
                    {
                        continue;
                    }
                }
            }

            sw.Stop();
            this.PrintTimeElapsed("Buffered Stream", sw.Elapsed.TotalMilliseconds, bufferSize);
        }

        private void PrintTimeElapsed(string methodName, double timeInMs, int bufferSize)
        {
            Console.WriteLine($"Method: {methodName} | Buffer size: {bufferSize} bytes | Elapsed Time: {timeInMs} ms");
            //Console.WriteLine($"Elapsed Time using {methodName} of buffer size {bufferSize} bytes = {timeInMs} ms");
        }
    }
}
