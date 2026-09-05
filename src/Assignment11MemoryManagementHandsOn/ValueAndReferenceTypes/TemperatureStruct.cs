namespace ValueAndReferenceTypes
{
    /// <summary>
    /// Represents a temperature metric.
    /// </summary>
    internal readonly struct TemperatureStruct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureStruct"/> struct.
        /// </summary>
        /// <param name="temperature">A double holding the temperature value.</param>
        internal TemperatureStruct(double temperature)
        {
            this.Temperature = temperature;
        }

        /// <summary>
        /// Gets the temperature value.
        /// </summary>
        /// <value>A double holding the temperature value.</value>
        public readonly double Temperature { get; }

        /// <summary>
        /// Prints the temperature value.
        /// </summary>
        public void PrintTemperature()
        {
            Console.WriteLine($"Temperature (struct) = {this.Temperature}");
        }
    }
}
