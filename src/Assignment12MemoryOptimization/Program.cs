using System.ComponentModel.Design;
using System.Reflection.Metadata;
using Assignment12MemoryOptimization;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            View view = new View();
            MenuOptions option = MenuOptions.OriginalUnoptimizedCode;

            while (option != MenuOptions.Exit)
            {
                Console.Clear();
                view.DisplayMenu();
                option = view.GetMenuChoice();
                switch (option)
                {
                    case MenuOptions.OriginalUnoptimizedCode:
                        RunTask1();
                        break;
                    case MenuOptions.FixedSizedList:
                        RunTask2();
                        break;
                    case MenuOptions.BoundedMemoryWithMaxListCount:
                        RunTask3();
                        break;
                    case MenuOptions.Exit:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option));
                }
            }

            Console.ReadKey();
        }

        // internal static 
        internal static void RunTask1()
        {
            var eater = new MemoryEater();
            eater.Allocate();
        }

        internal static void RunTask2()
        {
            var eater = new BoundedMemoryEater();
            eater.Allocate();
        }

        internal static void RunTask3()
        {
            var eater = new FixedSizeMemoryEater();
            eater.Allocate();
        }
    }
}