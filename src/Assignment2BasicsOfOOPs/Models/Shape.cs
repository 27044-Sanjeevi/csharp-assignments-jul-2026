namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Holds the properties and parent classes for shapes
    /// </summary>
    internal abstract class Shape
    {
        /// <summary>
        /// Gets or sets the color of the shape
        /// </summary>
        /// <value>Describes the color of the shape</value>
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets the name of the shape
        /// </summary>
        /// <value>Type of the shape</value>
        public string? ShapeType { get; set; }

        /// <summary>
        /// calculates the area of the shape
        /// </summary>
        /// <returns>area of the shape</returns>
        public abstract double CalculateArea();

        /// <summary>
        /// prints the color and area of the shape
        /// </summary>
        /// <param name="area">holds the area of the shape</param>
        /// <returns>A string containing the details of the shape</returns>
        public string PrintDetails(double area) => $"Shape Type: {this.ShapeType}\nColor: {this.Color}\nArea: {area}\n";
    }
}
