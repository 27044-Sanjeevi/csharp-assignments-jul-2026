namespace Assignment2BasicsOfOOPs.Services
{
    using Assignment2BasicsOfOOPs.Models;

    /// <summary>
    /// Provides the service to calculate the area of the shapes and print the details of the shapes.
    /// </summary>
    internal class ShapeService
    {

        public bool ValidateShape(Shape shape)
        {
            if(shape is null)
            {
                throw new ArgumentNullException(nameof(shape), "Shape cannot be null.");
            }
        }
        /// <summary>
        /// Calculates the area of the shape by calling the CalculateArea method of the shape.
        /// </summary>
        /// <param name="shape">The shape object holding the data of the shape.</param>
        /// <returns>Area of the shape.</returns>
        public static double CalculateArea(Shape shape) => shape.CalculateArea();



    }
}
