namespace Assignment15FileStreams.Task2
{
    /// <summary>
    /// Represents the statistics for the weather file.
    /// </summary>
    internal class WeatherFileStats
    {
        /// <summary>
        /// Gets or sets the path of the file.
        /// </summary>
        /// <value>A string holding the path of the file.</value>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the minimum temperature.
        /// </summary>
        /// <value>A double holding the minimum temperature.</value>
        public double MinTemperature { get; set; } = double.MaxValue;

        /// <summary>
        /// Gets or sets the maximum temperature.
        /// </summary>
        /// <value>A double holding the maximum temperature.</value>
        public double MaxTemperature { get; set; } = double.MinValue;

        /// <summary>
        /// Gets or sets the average temperature.
        /// </summary>
        /// <value>A double holding the average temperature.</value>
        public double AverageTemperature { get; set; } = 0;

        /// <summary>
        /// Gets or sets the total processed rows.
        /// </summary>
        /// <value>A long holding the total processed rows.</value>
        public long TotalProcessedRows { get; set; } = 0;

        /// <summary>
        /// Gets or sets the elapsed time in ms.
        /// </summary>
        /// <value>A double holding the elapsed time in ms.</value>
        public double ElapsedMilliseconds { get; set; }
    }
}