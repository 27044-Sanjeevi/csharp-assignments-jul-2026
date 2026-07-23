namespace Assignment2BasicsOfOOPs.Controller
{
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Services;
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Controller class for handling shape-related operations and interactions between the view and the service layer.
    /// </summary>
    internal class ShapeController
    {
        private readonly ConsoleView _view;
        private readonly ShapeService _shapeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeController"/> class with the specified view.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="shapeservice">The employee payroll service.</param>
        public ShapeController(ConsoleView view, ShapeService shapeservice)
        {
            this._view = view;
            this._shapeService = shapeservice;
        }

        /// <summary>
        /// Prompts the user and runs Task 1 (Shape Hierarchy).
        /// </summary>
        public void RunShapeTask()
        {
            this._view.PrintHeader("Task 1: Shape Hierarchy");

            // 1. Get Rectangle Details
            this._view.PrintSubHeader("Rectangle Creation");
            string rectColor = this._view.ReadString("Enter Rectangle Color: ");
            double rectHeight = this._view.ReadDouble("Enter Rectangle Height: ");
            double rectWidth = this._view.ReadDouble("Enter Rectangle Width: ");
            Shape rectangle = new Rectangle(rectColor, rectHeight, rectWidth);

            // 2. Get Circle Details
            this._view.PrintDivider();
            this._view.PrintSubHeader("Circle Creation");
            string circleColor = this._view.ReadString("Enter Circle Color: ");
            double circleRadius = this._view.ReadDouble("Enter Circle Radius: ");
            Shape circle = new Circle(circleColor, circleRadius);

            // 3. Print Output Details
            this._view.PrintDivider();
            this._view.PrintSubHeader("Shape Details Outcomes");
            double rectArea = this._shapeService.GetArea(rectangle);
            double circleArea = this._shapeService.GetArea(circle);

            this._view.PrintShapeDetails(rectangle, rectArea);
            this._view.PrintShapeDetails(circle, circleArea);
        }
    }
}
