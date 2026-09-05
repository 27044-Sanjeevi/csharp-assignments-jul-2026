namespace ValueAndReferenceTypes
{
    /// <summary>
    /// Represents a temperature metric.
    /// </summary>
    internal class TemperatureClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureClass"/> class.
        /// </summary>
        /// <param name="temperature">A double holding the temperature value.</param>
        internal TemperatureClass(double temperature)
        {
            this.Temperature = temperature;
        }

        /// <summary>
        /// Gets or sets the temperature value.
        /// </summary>
        /// <value>A double holding the temperature value.</value>
        public double Temperature { get; set; }

        /// <summary>
        /// Prints the temperature value.
        /// </summary>
        public void PrintTemperature()
        {
            Console.WriteLine($"Temperature (class) = {this.Temperature}");
        }
    }
}
