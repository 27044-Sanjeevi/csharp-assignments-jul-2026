namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Defines about the shape Circle.
    /// </summary>
    internal class Circle : Shape
    {
        private readonly double _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="color">Color of the circle.</param>
        /// <param name="radius">Radius of the Circle.</param>
        public Circle(string color, double radius)
        {
            this.ShapeType = "Circle";
            this.Color = color;
            this._radius = radius;
        }

        /// <summary>
        /// Calculates the area of the Circle by overriding the parent method.
        /// </summary>
        /// <returns>Area of the Circle as a double.</returns>
        public override double CalculateArea() => Math.PI * this._radius * this._radius;
    }
}
