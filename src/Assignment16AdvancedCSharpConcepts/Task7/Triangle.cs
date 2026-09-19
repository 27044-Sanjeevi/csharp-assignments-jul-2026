namespace Assignment16AdvancedCSharpConcepts.Task7
{
    /// <summary>
    /// Represents a triangular geometric shape.
    /// </summary>
    internal class Triangle : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// /// <param name="sideA">The length of the first side (Side A) of the triangle.</param>
        /// <param name="sideB">The length of the second side (Side B) of the triangle.</param>
        /// <param name="sideC">The length of the third side (Side C) of the triangle.</param>
        /// <param name="color">The visual color characteristic applied to the shape instance surface.</param>
        public Triangle(double sideA, double sideB, double sideC, string color)
        {
            this.Name = "Triangle";
            this.Color = color;
            this.SideA = sideA;
            this.SideB = sideB;
            this.SideC = sideC;
        }

        /// <summary>
        /// Gets the length of the first side of the triangle.
        /// </summary>
        /// <value>The double value representing the side A length dimension.</value>
        public double SideA { get; }

        /// <summary>
        /// Gets the length of the second side of the triangle.
        /// </summary>
        /// <value>The double value representing the side B length dimension.</value>
        public double SideB { get; }

        /// <summary>
        /// Gets the length of the third side of the triangle.
        /// </summary>
        /// <value>The double value representing the side C length dimension.</value>
        public double SideC { get; }

        /// <summary>
        /// Calculates and returns the area of the triangle using Heron's Formula.
        /// </summary>
        /// <returns>The calculated area of the triangle as a double value.</returns>
        public double CalculateArea()
        {
            double s = (this.SideA + this.SideB + this.SideC) / 2;
            return Math.Sqrt(s * (s - this.SideA) * (s - this.SideB) * (s - this.SideC));
        }
    }
}
