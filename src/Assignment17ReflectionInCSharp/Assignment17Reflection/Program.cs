using System;
using System.Diagnostics.Contracts;
using Assignment17Reflection.Menu;
using Assignment17Reflection.Tasks;
using Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Assignments
{
    internal class Program
    {
        internal static void Main(string[] args)
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
                            case MenuOptions.Task1InspectAssemblyMetadata:
                                RunTask1();
                                break;
                            case MenuOptions.Task2DynamicObjectInspector:
                                RunTask2();
                                break;
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

        private static void RunTask1()
        {
            string assemblyFilePath = @"C:\Users\Sanjeevi.senivasan\Documents\SANJEEVI\csharp_assignments_pod5\csharp-assignments-jul-2026\src\Assignment17ReflectionInCSharp\Contracts\bin\Debug\net6.0\Contracts.dll";
            var assemblyMetadataInspector = new AssemblyMetadataInspector();
            assemblyMetadataInspector.RunInspection(assemblyFilePath);
        }

        private static void RunTask2()
        {
            var inspector = new DynamicObjectInspector();
            byte[,] samplePixels = new byte[2, 2]
            {
                { 255, 128 }, // Row 0
                { 64,  0 }, // Row 1
            };

            ImageData sampleImage = new ImageData(
                height: 1080,
                width: 1920,
                format: "PNG",
                pixels: samplePixels);

            inspector.RunInspection(sampleImage);
        }
    }
}