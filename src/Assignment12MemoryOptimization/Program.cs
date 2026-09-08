using Assignment12MemoryOptimization;
using Assignment12MemoryOptimization.Tasks;

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
                view.DisplayExitInstruction();
                option = view.GetMenuChoice();
                Console.Clear();
                switch (option)
                {
                    case MenuOptions.OriginalUnoptimizedCode:
                        RunTask1();
                        break;
                    case MenuOptions.FixedSizedList:
                        RunTask2Technique1();
                        break;
                    case MenuOptions.BoundedMemoryWithMaxListCount:
                        RunTask2Technique2();
                        break;
                    case MenuOptions.DisplayInferences:
                        RunTask3();
                        break;
                    case MenuOptions.Exit:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option));
                }

                view.Pause();
            }
        }

        /// <summary>
        /// Runs task 1 of the appication.
        /// </summary>
        internal static void RunTask1()
        {
            var eater = new MemoryEater();
            eater.Allocate();
        }

        /// <summary>
        /// Runs task 2 with first technique of the appication.
        /// </summary>
        internal static void RunTask2Technique1()
        {
            var eater = new BoundedMemoryEater();
            eater.Allocate();
        }

        /// <summary>
        /// Runs task 2 with second technique of the appication.
        /// </summary>
        internal static void RunTask2Technique2()
        {
            var eater = new FixedSizeMemoryEater();
            eater.Allocate();
        }

        /// <summary>
        /// Runs task 3 of the application.
        /// </summary>
        internal static void RunTask3()
        {
            var eater = new MemoryEater();
            var boundedMemoryEater = new BoundedMemoryEater();
            var fixedSizeMemoryEater = new FixedSizeMemoryEater();
            eater.DisplayInferences();
            fixedSizeMemoryEater.DisplayInferences();
            boundedMemoryEater.DisplayInferences();
        }
    }
}