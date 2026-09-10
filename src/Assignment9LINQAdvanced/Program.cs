using Assignment9LINQAdvanced;
using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;
using Assignment9LINQAdvanced.Repository;
using Assignment9LINQAdvanced.Tasks;
using ConsoleTables;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        private static readonly int _maxChoice = 7;

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ProductRepository productRepository = new ProductRepository();
            SupplierRepository supplierRepository = new SupplierRepository();

            Task1 task1 = new Task1(productRepository);
            Task2 task2 = new Task2(productRepository, supplierRepository);
            Task3 task3 = new Task3();
            Task4 task4 = new Task4(productRepository);
            Task5 task5 = new Task5(productRepository, supplierRepository);

            MenuChoice menuChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("ADVANCED LINQ\n");

                DisplayTasksMenu();

                Console.Write("\nEnter your choice of task to run :");
                menuChoice = (MenuChoice)ConsoleHelpers.ReadChoice(_maxChoice);
                Console.Clear();
                switch (menuChoice)
                {
                    case MenuChoice.BasicLinqQueries:
                        task1.RunTask1();
                        break;
                    case MenuChoice.ComplexLinqQueries:
                        task2.RunTask2();
                        break;
                    case MenuChoice.LinqToObjects:
                        task3.RunTask3();
                        break;
                    case MenuChoice.PerformanceConsiderations:
                        task4.RunTask4();
                        break;
                    case MenuChoice.QueryBuilder:
                        task5.RunTask5();
                        break;
                    case MenuChoice.ViewData:
                        DisplayAllData(productRepository.GetAllProducts(), supplierRepository.GetAllSuppliers());
                        break;
                    case MenuChoice.Exit:
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                if (menuChoice != MenuChoice.Exit)
                {
                    ConsoleHelpers.Pause();
                }
            }
            while (menuChoice != MenuChoice.Exit);
        }

        private static void DisplayTasksMenu()
        {
            Console.WriteLine("Available Tasks: ");
            var choices = Enum.GetValues<MenuChoice>();
            foreach (MenuChoice choice in choices)
            {
                Console.WriteLine($"Task [{(int)choice}] : {choice}");
            }
        }

        private static void DisplayAllData(List<Product> products, List<Supplier> suppliers)
        {
            ConsoleTable productsTable = new ConsoleTable("Id", "Name", "Price", "Category");
            ConsoleTable suppliersTable = new ConsoleTable("Id", "Supplier Name", "Product Id");

            foreach (var product in products)
            {
                productsTable.AddRow(product.Id, product.Name, product.Price, product.Category);
            }

            foreach (var supplier in suppliers)
            {
                suppliersTable.AddRow(supplier.SupplierId, supplier.SupplierName, supplier.ProductId);
            }

            Console.WriteLine("----- Products -----");
            productsTable.Write();

            Console.WriteLine("\n----- Supplier and Product Mapping -----");
            suppliersTable.Write();
        }
    }
}