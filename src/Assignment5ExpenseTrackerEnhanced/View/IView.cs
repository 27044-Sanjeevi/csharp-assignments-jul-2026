using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;
using Assignment5ExpenseTrackerEnhanced.Services.Validation;
using Assignment5ExpenseTrackerEnhanced.View.Interfaces;

namespace Assignment5ExpenseTrackerEnhanced.View
{
    /// <summary>
    /// Represents a contract for view layer in the Expense Tracker application.
    /// </summary>
    internal interface IView : IDataVisualizationView, IDisplayView, IInputView, IHeaderView
    {
    }
}
