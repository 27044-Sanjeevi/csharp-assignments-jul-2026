using Assignment17Reflection.Menu;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Menu View
            MenuView view = new MenuView();
            MenuOptions option = MenuOptions.Task1InspectAssemblyMetadata;

            try
            {
                while (option != MenuOptions.Exit)
                {
                    try
                    {
                        Console.Clear();
                        view.DisplayMenu();
                        option = view.GetMenuChoice();
                        Console.Clear();
                        switch (option)
                        {
                            //case MenuOptions.Task1InspectAssemblyMetadata:
                            //    RunTask1();
                            //    break;
                            //case MenuOptions.Task2DynamicObjectInspector:
                            //    RunTask2(tplTask);
                            //    break;
                            //case MenuOptions.Task3DynamicMethodInvoker:
                            //    RunTask3(multithreadingTask);
                            //    break;
                            //case MenuOptions.Task4DynamicTypeBuilder:
                            //    await RunTask4(cascadingAsyncOperations);
                            //    break;
                            //case MenuOptions.Exit:
                            //    return;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(option));
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("\n[ARGUMENT NULL EXCEPTION] : " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n[EXCEPTION] : " + ex.Message);
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