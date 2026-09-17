namespace Assignment15FileStreams.Task4
{
    /// <summary>
    /// Represents the load test result.
    /// </summary>
    internal class LoadTestResult
    {
        /// <summary>
        /// Gets or sets the name of the logging strategy .
        /// </summary>
        /// <value>The string name of the logging strategy.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total elapsed execution duration of the load test track in milliseconds.
        /// </summary>
        /// <value>The time taken to execute the test sequence in milliseconds.</value>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the total number of logging operations that completed successfully.
        /// </summary>
        /// <value>The integer count of successful file write operations.</value>
        public int SuccessCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of logging operations that threw exceptions due to file access lock contentions.
        /// </summary>
        /// <value>The integer count of failed write operations.</value>
        public int FailureCount { get; set; }

        /// <summary>
        /// Gets or sets the calculated throughput capacity rate of the logger.
        /// </summary>
        /// <value>The processing throughput as logs per single second.</value>
        public double ThroughputPerSec { get; set; }
    }
}
