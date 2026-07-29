namespace Assignment2BasicsOfOOPs.Validation
{
    /// <summary>
    /// Represents the validaion of shapes
    /// </summary>
    internal class ShapeValidation
    {
        /// <summary>
        /// Validates if the color has only alphabets in it
        /// </summary>
        /// <param name="color">The string representing the color of the shape.</param>
        /// <returns>True if color contains only alphabets, false otherwise.</returns>
        public bool IsValidColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                return false;
            }

            return color.All(char.IsLetter);
        }
    }
}
