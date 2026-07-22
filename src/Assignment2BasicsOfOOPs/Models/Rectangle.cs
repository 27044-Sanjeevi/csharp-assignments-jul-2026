namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// defines about the shape Rectangle
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// only reads the height of the rectangle
        /// </summary>
        /// <value>height of the rectangle</value>
        private readonly double _height;

        /// <summary>
        /// only reads the width of the rectangle
        /// </summary>
        /// <value>height of the rectangle</value>
        private readonly double _width;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class
        /// </summary>
        /// <param name="color">color of the shape</param>
        /// <param name="height">height of the Reactangle</param>
        /// <param name="width">width of the Rectangle</param>
        public Rectangle(string color, double height, double width)
        {
            this.ShapeType = "Rectangle";
            this.Color = color;
            this._height = height;
            this._width = width;
        }

        /// <summary>
        /// calculates the area of the Rectangle by overriding the parent method
        /// </summary>
        /// <returns>area of the rectangle as a double</returns>
        public override double CalculateArea() => this._width * this._height;
    }
}
