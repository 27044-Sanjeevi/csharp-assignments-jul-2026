namespace Assignments
{
    using Assignment9LINQAdvanced.Database;
    using Assignment9LINQAdvanced.Models;
    using Assignment9LINQAdvanced.Tasks;

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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ProductDatabase productDatabase = new ProductDatabase();
            SupplierDatabase supplierDatabase = new SupplierDatabase();

            Task1 task1 = new Task1(productDatabase);
            Task2 task2 = new Task2(productDatabase, supplierDatabase);
            Task3 task3 = new Task3();
            Task4 task4 = new Task4(productDatabase);

            // task3.RunTask3();
            task2.RunTask2();
            Console.ReadKey();
        }
    }
}