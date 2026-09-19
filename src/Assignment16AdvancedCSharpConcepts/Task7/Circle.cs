namespace Assignment16AdvancedCSharpConcepts.Task7
{
    /// <summary>
    /// Represents a circle shape.
    /// </summary>
    internal class Circle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="radius">A double holding the radius of the circle.</param>
        /// <param name="color">A string representing the color of the circle.</param>
        public Circle(double radius, string color)
        {
            this.Name = "Circle";
            this.Color = color;
            this.Radius = radius;
        }

        /// <summary>
        /// Gets the radius of the circle.
        /// </summary>
        /// <value>A double holding the radius of the circle.</value>
        public double Radius { get; }

        /// <summary>
        /// Calculates and returns the area of the circle.
        /// </summary>
        /// <returns>A double holding the area of the circle.</returns>
        public double CalculateArea()
        {
            return Math.PI * this.Radius * this.Radius;
        }
    }
}
