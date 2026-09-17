using System.Text;

namespace Assignment15FileStreams.Task4
{
    /// <summary>
    /// Represents thread-safe logging.
    /// </summary>
    internal class ThreadSafeLogger
    {
        private static readonly string LogFilePath = "thread_safe_log.txt";
        private static readonly object FileLock = new object();

        /// <summary>
        /// Logs an error message safely across multiple threads using a synchronization lock.
        /// </summary>
        /// <param name="errorMessage">The error message to be logged.</param>
        public static void LogError(string errorMessage)
        {
            byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage + Environment.NewLine);

            lock (FileLock)
            {
                using (FileStream fileStream = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    fileStream.Write(errorBytes, 0, errorBytes.Length);
                }
            }
        }
    }
}
