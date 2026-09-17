using System.Text;

namespace Assignment15FileStreams.Task4
{
    /// <summary>
    /// Represents independent user logger.
    /// </summary>
    internal class IndependentUserLogger
    {
        private static readonly string LogDirectory = "user_logs";

        static IndependentUserLogger()
        {
            Directory.CreateDirectory(LogDirectory);
        }

        /// <summary>
        /// Logs an error message to a dedicated user-specific file.
        /// </summary>
        /// <param name="userId">The current user Id.</param>
        /// <param name="errorMessage">The error message to be displayed.</param>
        public static void LogError(int userId, string errorMessage)
        {
            string userLogPath = Path.Combine(LogDirectory, $"user_{userId}.log");
            byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage + Environment.NewLine);

            using (FileStream fileStream = new FileStream(userLogPath, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                fileStream.Write(errorBytes, 0, errorBytes.Length);
            }
        }
    }
}