namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Holds the properties and parent classes for shapes.
    /// </summary>
    internal abstract class Shape
    {
        /// <summary>
        /// Gets or sets the color of the shape.
        /// </summary>
        /// <value>Describes the color of the shape.</value>
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets the name of the shape.
        /// </summary>
        /// <value>Type of the shape.</value>
        public string? ShapeType { get; set; }

        /// <summary>
        /// Calculates the area of the shape.
        /// </summary>
        /// <returns>Area of the shape.</returns>
        public abstract double CalculateArea();
    }
}
