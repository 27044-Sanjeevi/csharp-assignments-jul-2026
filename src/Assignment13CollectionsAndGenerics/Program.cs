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
    }
}