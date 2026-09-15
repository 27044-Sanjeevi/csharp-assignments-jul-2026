using Assignment13CollectionsAndGenerics;
using Assignment13CollectionsAndGenerics.Collections;
using Assignment13CollectionsAndGenerics.Data;

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
            try
            {
                BookList<string> bookList = new BookList<string>();

                StringStack<char> stringStack = new StringStack<char>();

                PersonQueue<string> personQueue = new PersonQueue<string>();

                StudentDictionary<string, int> studentDictionary = new StudentDictionary<string, int>();

                EnumerableAndReadonly enumerableAndReadonlyTask = new EnumerableAndReadonly();

                MenuView view = new MenuView();
                StringStack<char> stack = new StringStack<char>();
                stack.PerformOperations(TestData.InitialStack);

                MenuOptions option = MenuOptions.Task1List;

                while (option != MenuOptions.Exit)
                {
                    Console.Clear();
                    view.DisplayMenu();
                    option = view.GetMenuChoice();
                    Console.Clear();
                    switch (option)
                    {
                        case MenuOptions.Task1List:
                            bookList.PerformOperations(TestData.Books, TestData.BookToRemove, TestData.BookToCheckExistence);
                            break;
                        case MenuOptions.Task2Stack:
                            stringStack.PerformOperations(TestData.InitialStack);
                            break;
                        case MenuOptions.Task3Queue:
                            personQueue.PerformOperations(TestData.PersonQueue);
                            break;
                        case MenuOptions.Task4Dictionary:
                            studentDictionary.PerformOperations(TestData.StudentGrades, TestData.StudentToRemove);
                            break;
                        case MenuOptions.Task5Generics:
                            Program.Task5Description();
                            break;
                        case MenuOptions.Task6EnumerableAndReadonly:
                            enumerableAndReadonlyTask.PerformOperations(TestData.integerList, TestData.integerArray, TestData.integerQueue);
                            break;
                        case MenuOptions.Exit:
                            return;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(option));
                    }

                    view.Pause();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Describes the changes in Task 5 where collections were updated to use generic types for improved type safety.
        /// and reusability.
        /// </summary>
        internal static void Task5Description()
        {
            ConsoleHelpers.DisplayTitle("TASK 5 : GENERICS");
            Console.WriteLine("- The commit `32519cb` contains the version of Task 1 to 4 where generics is not used." +
                "\n- For task 5, the collections are modified to use generic types:\n" +
                "\t- List<T>" +
                "\n\t- Stack<T>" +
                "\n\t- Queue<T>" +
                "\n\t- Dictionary<TKey, TValue>" +
                "\n- Instead of creating separate classes for specific data types, generic type parameters were used so the same collection logic can be reused." +
                "\n- Generics provide type safety, reusability, and reduced code duplication while allowing the compiler to enforce the expected data types.");
        }
    }
}