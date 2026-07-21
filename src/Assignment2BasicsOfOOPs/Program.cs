namespace Assignments
{
    using Assignment2BasicsOfOOPs;
    using Assignment2BasicsOfOOPs.Task1ShapeHierarchy;
    using Assignment2BasicsOfOOPs.Task2EmployeeHierarchy;
    using Assignment2BasicsOfOOPs.Task3BankSystem;

    /// <summary>
    /// Contains the Main method of the projcet
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Displays all 3 tasks output.
        /// </summary>
        /// <param name="args">optional args from the CLI</param>
        internal static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Clear();

                choice = ConsoleIO.ReadInt("Available Tasks:\n" +
                      "1. Task 1 - Shape Heirarchy\n" +
                      "2. Task 2 - Employee Hierarchy\n" +
                      "3. Task 3 - Bank System\n" +
                      "4. Exit Application\n" +
                      "\nChoose the Task to run: ");

                while (choice < 1 || choice > 4)
                {
                    choice = ConsoleIO.ReadInt("Invalid Choice, Choose an integer between 1 to 4: ");
                }

                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case 1:
                            RunTask1();
                            break;
                        case 2:
                            RunTask2();
                            break;
                        case 3:
                            RunTask3();
                            break;
                        case 4:
                            break;
                    }
                }
                catch (BankSystemException ex)
                {
                    ConsoleIO.Write(ex.Message);
                    ConsoleIO.Write("\nPress any key to return to Main Page...");
                    Console.ReadKey();
                }
            }
            while (choice != 4);

            ConsoleIO.Write("Press any key to exit the application...");
            Console.ReadKey();
        }

        private static void RunTask1()
        {
            ConsoleIO.WriteColored("Task 1: Shape Hierarchy\n\n", ConsoleColor.Blue);

            Shape rectangleShape = new Rectangle("Blue", 5, 10);
            Shape circleShape = new Circle("Red", 5);

            rectangleShape.PrintDetails(rectangleShape.CalculateArea());
            circleShape.PrintDetails(circleShape.CalculateArea());

            ConsoleIO.Write("\nPress any key to return to Main Page...");
            Console.ReadKey();
        }

        private static void RunTask2()
        {
            ConsoleIO.WriteColored("Task 2: Employee Hierarchy\n\n", ConsoleColor.Blue);

            Employee manager = new Manager("Suresh", 50000.00M);
            Employee developer = new Developer("Ramesh", 30000.00M);

            manager.PrintDetails();
            developer.PrintDetails();

            ConsoleIO.Write("\nPress any key to return to Main Page...");
            Console.ReadKey();
        }

        private static void RunTask3()
        {
            ConsoleIO.WriteColored("Task 3: Bank System\n\n", ConsoleColor.Blue);

            BankAccount savingsAccount = new SavingsAccount("10001", 1000M);
            BankAccount checkingAccount = new CheckingAccount("10002", 2000M);

            ConsoleIO.WriteColored("Savings Account\n", ConsoleColor.Yellow);

            savingsAccount.PrintAllDetails();

            savingsAccount.Deposit(200M);
            ConsoleIO.Write("Deposited: Rs. 200.00\n");

            savingsAccount.PrintCurrentBalance();

            try
            {
                ConsoleIO.Write("Attempting to withdraw: Rs. 5,000.00\n");
                savingsAccount.Withdraw(5000M);

                savingsAccount.PrintCurrentBalance();
            }
            catch (BankSystemException ex)
            {
                ConsoleIO.WriteColored($"[TRANSACTION BLOCKED] {ex.Message}\n", ConsoleColor.Red);
                ConsoleIO.Write("Current status is not affected.\n");
                savingsAccount.PrintCurrentBalance();
            }

            ConsoleIO.Write("\n" + new string('-', 40) + "\n\n");

            ConsoleIO.WriteColored("Checking Account\n", ConsoleColor.Yellow);
            checkingAccount.PrintAllDetails();

            checkingAccount.Deposit(200M);
            ConsoleIO.Write("Deposited: Rs. 200.00\n");
            checkingAccount.PrintCurrentBalance();

            try
            {
                ConsoleIO.Write("Attempting to withdraw: Rs. 5,000.00 :\n");
                checkingAccount.Withdraw(5000M);

                ConsoleIO.Write("Withdrawn successfully.\n");
                checkingAccount.PrintCurrentBalance();
            }
            catch (BankSystemException ex)
            {
                ConsoleIO.WriteColored($"[TRANSACTION BLOCKED] {ex.Message}\n", ConsoleColor.Red);
                checkingAccount.PrintCurrentBalance();
            }

            ConsoleIO.Write("\nPress any key to return to Main Page...");
            Console.ReadKey();
        }
    }
}