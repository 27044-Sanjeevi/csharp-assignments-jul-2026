using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment8ErrorHandling.IO;
using Assignment8ErrorHandling.Utilities;

namespace Assignment8ErrorHandling.View
{
    internal class View
    {
        private readonly IConsoleIO _consoleIO;
        private readonly ConsoleHelper _consoleHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        /// <param name="consoleIO">The console input and output renderer.</param>
        /// <param name="consoleHelper">The console Helper object.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object passed is null.</exception>
        public View(IConsoleIO consoleIO, ConsoleHelper consoleHelper)
        {
            this._consoleIO = consoleIO ?? throw new ArgumentNullException(nameof(consoleIO));
            this._consoleHelper = consoleHelper ?? throw new ArgumentNullException(nameof(_consoleHelper));
        }

        /// <summary>
        /// Dsiplays the header for Task 1.
        /// </summary>
        public void PrintTask1Header()
        {
            this._consoleHelper.PrintHeader("TASK 1 (DIVISON BY ZERO)");
        }
    }
}
