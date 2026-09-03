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
        private const int ArrayLength = 20000;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            TemperatureClass temperatureClass = new TemperatureClass(OriginalTemperature);
            TemperatureStruct temperatureStruct = new TemperatureStruct(OriginalTemperature);

            Console.WriteLine("--- VALUE TYPE VS REFERNCE TYPE ---\n");
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
        }

        private static void CreateIntegersOnStack()
        {
            int v1 = 1, v2 = 2, v3 = 3, v4 = 4, v5 = 5;
        }
    }
}