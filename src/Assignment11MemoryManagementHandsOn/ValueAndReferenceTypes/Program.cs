using ValueAndReferenceTypes;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        private const int OriginalTemperature = 10;
        private const int ModifiedTemperature = 20;
        private const int ArrayLength = 2000000;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            TemperatureClass temperatureClass = new TemperatureClass(OriginalTemperature);
            TemperatureStruct temperatureStruct = new TemperatureStruct(OriginalTemperature);

            Console.WriteLine("--- TASK 1: VALUE TYPE VS REFERNCE TYPE ---\n");
            Console.WriteLine("Before Calling Modify Method:");
            temperatureClass.PrintTemperature();
            temperatureStruct.PrintTemperature();

            Modify(temperatureClass, temperatureStruct);

            Console.WriteLine("\nAfter Calling Modify Method:");
            temperatureClass.PrintTemperature();
            temperatureStruct.PrintTemperature();

            Console.WriteLine("\nInference:" +
                "\n- The changes made to the value type (struct) variable in the Modify method does not affect the original variable in the main method." +
                "\n- Whereas the changes made to the reference type (class) object in the Modify method affected the original instance in the main method.");

            Console.WriteLine("\n--- TASK 2: STACK AND HEAP ---\n");
            CreateIntegersOnHeap();
            CreateIntegersOnStack();
            Console.ReadKey();
        }

        private static void Modify(TemperatureClass temperatureClass, TemperatureStruct temperatureStruct)
        {
            temperatureClass.Temperature = ModifiedTemperature;
            temperatureStruct = new TemperatureStruct(ModifiedTemperature);

            Console.WriteLine("\nInside the Modify Method:");
            temperatureClass.PrintTemperature();
            temperatureStruct.PrintTemperature();
        }

        private static void CreateIntegersOnHeap()
        {
            int[] integerArray = new int[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                integerArray[i] = i;
            }

            Console.WriteLine($"Created an integer array of size {ArrayLength}. Since this is reference type, it gets stored in heap.\n");
        }

        private static void CreateIntegersOnStack()
        {
            int number1 = 1,
                number2 = 2,
                number3 = 3,
                number4 = 4,
                number5 = 5,
                number6 = 6,
                number7 = 7,
                number8 = 8,
                number9 = 9,
                number10 = 10,
                number11 = 11,
                number12 = 12;

            int result = number1 + number2 + number3 + number4 + number5 + number6 + number7 + number8 + number9 + number10 + number11 + number12;

            Console.WriteLine("Created multiple value type integers. This occupies the space in stack.");
        }
    }
}