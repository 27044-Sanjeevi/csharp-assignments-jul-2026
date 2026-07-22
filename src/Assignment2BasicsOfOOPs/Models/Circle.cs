namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// defines about the shape Circle
    /// </summary>
    internal class Circle : Shape
    {
        /// <summary>
        /// only reads the radius of the circle
        /// </summary>
        /// <value>radius of the circle</value>
        private readonly double _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class
        /// </summary>
        /// <param name="color">color of the circle</param>
        /// <param name="radius">radius of the Circle</param>
        public Circle(double radius, string color)
        {
            this.ShapeType = "Circle";
            this._radius = radius;
            this.Color = color;
        }

        /// <summary>
        /// calculates the area of the Circle by overriding the parent method
        /// </summary>
        /// <returns>area of the Circle as a double</returns>
        public override double CalculateArea() => Math.PI * this._radius * this._radius;
    }
}
