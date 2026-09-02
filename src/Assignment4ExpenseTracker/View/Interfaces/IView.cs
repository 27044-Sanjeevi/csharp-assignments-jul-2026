namespace Assignment4ExpenseTracker.View.Interfaces
{
    /// <summary>
    /// Represents a contract for view layer in the Expense Tracker application.
    /// </summary>
    internal interface IView : IDataVisualizationView, IDisplayView, IInputView, IHeaderView
    {
        // This interface acts as a Composite Interface.
        // Its primary purpose is to aggregate the split, role-specific interfaces (IDataVisualizationView, IDisplayView, IInputView, and IHeaderView) into a single, unified contract for the view layer.
        // This allow us to follow the Interface Segregation Principle, while still allowing controllers or dependency injection to inject the entire view via a single reference (IView).
    }
}
