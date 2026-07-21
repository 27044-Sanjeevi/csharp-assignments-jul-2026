namespace Assignment2BasicsOfOOPs.Task1ShapeHierarchy
{
    /// <summary>
    /// defines about the shape Circle
    /// </summary>
    internal class Circle : Shape
    {
        /// <summary>
        /// only reads the radius of the rectangle
        /// </summary>
        /// <value>radius of the circle</value>
        private readonly double _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class
        /// </summary>
        /// <param name="radius">radius of the Circle</param>
        /// <param name="color">color of the circle</param>
        public Circle(string color, double radius)
        {
            this.ShapeType = "Circle";
            this.Color = color;
            this._radius = radius;
        }

        /// <summary>
        /// calculates the area of the Circle by overriding the parent method
        /// </summary>
        /// <returns>area of the Cicle as a double</returns>
        public override double CalculateArea() => Math.PI * this._radius * this._radius;
    }
}
