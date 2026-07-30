namespace Assignment2BasicsOfOOPs.Controller
{
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;
    using Assignment2BasicsOfOOPs.Services;
    using Assignment2BasicsOfOOPs.Validation;
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Controller class for handling shape-related operations and interactions between the view and the service layer.
    /// </summary>
    internal class ShapeController
    {
        private readonly ConsoleView _view;
        private readonly ShapeService _shapeService;
        private readonly ShapeValidation _shapeValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeController"/> class with the specified view.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="shapeservice">The employee payroll service.</param>
        /// <param name="shapeValidator">Validates the shape.</param>
        public ShapeController(ConsoleView view, ShapeService shapeservice, ShapeValidation shapeValidator)
        {
            this._view = view;
            this._shapeService = shapeservice;
            this._shapeValidator = shapeValidator;
        }

        /// <summary>
        /// Prompts the user and runs Task 1 (Shape Hierarchy).
        /// </summary>
        public void RunShapeTask()
        {
            this._view.PrintHeader("Task 1: Shape Hierarchy");

            // 1. Rectangle
            this._view.PrintSubHeader("Rectangle Creation");
            string rectColor = this.GetShapeColor();
            double rectHeight = this._view.ReadDouble("Enter Rectangle Height: ");
            double rectWidth = this._view.ReadDouble("Enter Rectangle Width: ");
            Shape rectangle = this._shapeService.CreateShape(ShapeType.Rectangle, rectColor, rectHeight, rectWidth);

            // 2. Circle
            this._view.PrintDivider();
            this._view.PrintSubHeader("Circle Creation");
            string circleColor = this.GetShapeColor();
            double circleRadius = this._view.ReadDouble("Enter Circle Radius: ");
            Shape circle = this._shapeService.CreateShape(ShapeType.Circle, circleColor, circleRadius);

            // 3. Print Output
            this._view.PrintDivider();
            this._view.PrintSubHeader("Shape Details Outcomes");
            double rectArea = this._shapeService.GetArea(rectangle);
            double circleArea = this._shapeService.GetArea(circle);

            this._view.PrintShapeDetails(rectangle, rectArea);
            this._view.PrintShapeDetails(circle, circleArea);
        }

        /// <summary>
        /// Recursively prompts user until color contains only alphabets.
        /// </summary>
        /// <returns>The color containing only alphabets.</returns>
        public string GetShapeColor()
        {
            string color;

            while (true)
            {
                color = this._view.ReadString("\nEnter the Color: ");

                if (this._shapeValidator.IsValidColor(color))
                {
                    return color;
                }

                this._view.PrintInvalidShapeColor();
            }
        }
    }
}
