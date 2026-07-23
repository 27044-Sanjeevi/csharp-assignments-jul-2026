namespace Assignment2BasicsOfOOPs.Services
{
    using Assignment2BasicsOfOOPs.Models;

    /// <summary>
    /// Provides services for calculating and operating on shapes.
    /// </summary>
    internal class ShapeService
    {
        /// <summary>
        /// Calculates the area of the given shape.
        /// </summary>
        /// <param name="shape">The shape object.</param>
        /// <returns>The calculated area, or 0.0 if shape is null.</returns>
        public double GetArea(Shape shape)
        {
            if (shape == null)
            {
                return 0.0;
            }

            return shape.CalculateArea();
        }
    }
}