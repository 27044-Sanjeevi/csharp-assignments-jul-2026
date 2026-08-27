using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9LINQAdvanced.Models.Enums
{
    /// <summary>
    /// Specifies the menu choices.
    /// </summary>
    internal enum MenuChoice
    {
        /// <summary>
        /// Specifies the task 1 (Basic Linq Queries) of the application.
        /// </summary>
        BasicLinqQueries = 1,

        /// <summary>
        /// Specifies the task 2 (Complex Linq Queries) of the application.
        /// </summary>
        ComplexLinqQueries = 2,

        /// <summary>
        /// Specifies the task 3 (Linq To Objects) of the application.
        /// </summary>
        LinqToObjects = 3,

        /// <summary>
        /// Specifies the task 4 (Performance Considerations) of the application.
        /// </summary>
        PerformanceConsiderations = 4,

        /// <summary>
        /// Specifies the task 5 (Query Builder) of the application.
        /// </summary>
        QueryBuilder = 5,

        /// <summary>
        /// Exits the application.
        /// </summary>
        Exit = 6,
    }
}
