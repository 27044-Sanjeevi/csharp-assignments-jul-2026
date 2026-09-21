namespace Assignment16AdvancedCSharpConcepts.Task7
{
    /// <summary>
    /// Represents the operations to demonstrate the task 7.
    /// </summary>
    internal class ShapesDemo
    {
        /// <summary>
        /// Runs the demonstration for task 7.
        /// </summary>
        public void RunDemo()
        {
            List<Shape> shapes = new List<Shape>
            {
                new Circle(5.0, "Red"),
                new Rectangle(4.0, 6.0, "Blue"),
                new Triangle(3.0, 4.0, 5.0, "Green"),
            };

            ConsoleHelpers.DisplayStatus("Created three shapes (circle, rectangle, triangle).");
            ConsoleHelpers.DisplayStatus("Displaying the shape details...");
            foreach (var shape in shapes)
            {
                this.DisplayShapeDetails(shape);
            }

            ConsoleHelpers.DisplayStatus("Passing null reference explicitly...");
            this.DisplayShapeDetails(null);
        }

        private void DisplayShapeDetails(Shape? shape)
        {
            if (shape != null)
            {
                Console.WriteLine(shape.Description);
            }

            string shapeDetails = shape switch
            {
                Circle circle => $"Radius = {circle.Radius:F2}\nArea = {circle.CalculateArea():F2}",
                Rectangle rectangle => $"Width = {rectangle.Width:F2} | Height = {rectangle.Height:F2}\nArea = {rectangle.CalculateArea():F2}",
                Triangle triangle => $"Side A = {triangle.SideA:F2} | Side B = {triangle.SideB:F2} |Side C = {triangle.SideC:F2}\nArea = {triangle.CalculateArea():F2}",
                null => "The shape reference is null.",
                _ => "Unknown shape type.",
            };

            Console.WriteLine(shapeDetails + Environment.NewLine);
        }
    }
}
