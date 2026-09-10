using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment13CollectionsAndGenerics
{
    internal static class ConsoleHelpers
    {
        public static void DisplayTitle(string title)
        {
            WriteColored($"----- {title} -----\n", ConsoleColor.Cyan);
        }

        public static void DisplayStatus(string message)
        {
            WriteColored($"{message}\n", ConsoleColor.Yellow);
        }

        public static void DisplayFailure(string message)
        {
            WriteColored($"{message}\n", ConsoleColor.Red);
        }

        public static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
