namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Defines about the shape Rectangle.
    /// </summary>
    internal class Rectangle : Shape
    {
        private readonly double _height;
        private readonly double _width;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="color">Color of the shape.</param>
        /// <param name="height">Height of the Rectangle.</param>
        /// <param name="width">Width of the Rectangle.</param>
        public Rectangle(string color, double height, double width)
        {
            this.ShapeType = "Rectangle";
            this.Color = color;
            this._height = height;
            this._width = width;
        }

        /// <summary>
        /// Calculates the area of the Rectangle by overriding the parent method.
        /// </summary>
        /// <returns>Area of the rectangle as a double.</returns>
        public override double CalculateArea() => this._width * this._height;
    }
}
