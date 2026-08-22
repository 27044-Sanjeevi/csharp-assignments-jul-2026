using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4ExpenseTracker.Models.Enums
{
    /// <summary>
    /// Specifies the Main Menu options.
    /// </summary>
    internal enum MainMenuOption
    {
        /// <summary>
        /// Specifies add operation.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Specifies view operation.
        /// </summary>
        ViewAll = 2,

        /// <summary>
        /// Specifies update operation.
        /// </summary>
        Update = 3,

        /// <summary>
        /// Specifies delete operation.
        /// </summary>
        Delete = 4,

        /// <summary>
        /// Specifies report generation operation.
        /// </summary>
        GenerateReport = 5,

        /// <summary>
        /// Specifies exit operation.
        /// </summary>
        Exit = 6,
    }
}
