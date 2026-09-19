namespace Assignment16AdvancedCSharpConcepts.Task7
{
    /// <summary>
    /// Represents the base abstract representation of a geometric shape container.
    /// </summary>
    internal class Shape
    {
        /// <summary>
        /// Gets or sets the name of the shape.
        /// </summary>
        /// <value>The string value representing the name of the shape.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color of the shape.
        /// </summary>
        /// <value>The string value representing the color.</value>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Gets a summary describing the attributes of the shape.
        /// </summary>
        /// <value>A formatted string combining the name and color.</value>
        public string Description => $"This is a {this.Name} of {this.Color} color.";
    }
}
