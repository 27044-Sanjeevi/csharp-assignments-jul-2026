namespace Assignment2BasicsOfOOPs.Services
{
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;

    /// <summary>
    /// Provides services for calculating and operating on shapes.
    /// </summary>
    internal class ShapeService
    {
        /// <summary>
        /// Creates a shape of the specified type, color, and dimensions.
        /// </summary>
        /// <param name="shapeType">The type of shape to create.</param>
        /// <param name="color">The color of the shape.</param>
        /// <param name="dimensions">The dimensions required to construct the shape.</param>
        /// <returns>An instance of the specified shape.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dimensions are null or not provided.</exception>
        /// <exception cref="ArgumentException">Thrown when the shape type is unknown or the dimensions are invalid for the specified shape type.</exception>
        public Shape CreateShape(ShapeType shapeType, string color, params double[] dimensions)
        {
            if (dimensions == null || dimensions.Length == 0)
            {
                throw new ArgumentNullException("Dimensions must be provided", nameof(dimensions));
            }

            return shapeType switch
            {
                ShapeType.Rectangle => dimensions.Length == 2
                                       ? new Rectangle(color, dimensions[0], dimensions[1])
                                       : throw new ArgumentException("Rectangle must reqire length and width to create."),
                ShapeType.Circle => dimensions.Length == 1
                                    ? new Circle(color, dimensions[0])
                                    : throw new ArgumentException("Circle must require radius to create."),
                _ => throw new ArgumentException("Unknown Shape type", nameof(shapeType))

            };
        }

        /// <summary>
        /// Calculates the area of the given shape.
        /// </summary>
        /// <param name="shape">The shape object.</param>
        /// <returns>The calculated area, or 0.0 if shape is null.</returns>
        public double GetArea(Shape shape)
        {
            ArgumentNullException.ThrowIfNull(shape);

            return shape.CalculateArea();
        }
    }
}