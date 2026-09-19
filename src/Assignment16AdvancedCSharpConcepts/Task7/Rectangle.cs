namespace Assignment16AdvancedCSharpConcepts.Task7
{
    /// <summary>
    /// Represents a rectangle shape.
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="color">Color of the rectangle.</param>
        public Rectangle(double width, double height, string color)
        {
            this.Name = "Rectangle";
            this.Color = color;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the horizontal width of the rectangle.
        /// </summary>
        /// <value>A double holding the width of the rectangle.</value>
        public double Width { get; }

        /// <summary>
        /// Gets the vertical height of the rectangle.
        /// </summary>
        /// <value>A double holding the height of the rectangle.</value>
        public double Height { get; }

        /// <summary>
        /// Calculates and returns the area of the rectangle.
        /// </summary>
        /// <returns>A double holding the area of the rectangle.</returns>
        public double CalculateArea()
        {
            return this.Width * this.Height;
        }
    }
}
