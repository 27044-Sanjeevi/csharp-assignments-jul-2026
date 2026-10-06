using System.ComponentModel;
using System.Runtime.CompilerServices;
using BMathApp;
using DUtilityApp;

namespace Assignments
{
    internal class Program
    {
        private const string LogFileName = "math_history_log.txt";
        private static readonly string SolutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
        private static readonly string LogFilePath = Path.Combine(SolutionDir, LogFileName);

        private static void Main(string[] args)
        {
            try
            {
                PerformCalculation();
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayFailure($"An unexpected error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void PerformCalculation()
        {
            int leftOperand = ConsoleHelpers.ReadInteger("Enter first number: ");
            int rightOperand = ConsoleHelpers.ReadInteger("Enter second number: ");
            char operatorSymbol = ConsoleHelpers.ReadChar("Enter operator (+, -, *, /): ");
            double result = 0;
            try
            {
                result = operatorSymbol switch
                {
                    '+' => MathOperations.Add(leftOperand, rightOperand),
                    '-' => MathOperations.Subtract(leftOperand, rightOperand),
                    '*' => MathOperations.Multiply(leftOperand, rightOperand),
                    '/' => MathOperations.Divide(leftOperand, rightOperand),
                    _ => throw new InvalidOperationException("Invalid operator. Please use +, -, *, or /."),
                };
            }
            catch (DivideByZeroException ex)
            {
                ConsoleHelpers.DisplayFailure(ex.Message);
            }

            ConsoleHelpers.DisplaySuccess($"Result: {leftOperand} {operatorSymbol} {rightOperand} = {result:F2}");
            LogToFile($"{leftOperand} {operatorSymbol} {rightOperand} = {result:F2}");
        }

        private static void LogToFile(string calculationDetails)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {calculationDetails}";

                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);

                ConsoleHelpers.WriteColored($"[Calculation saved to {LogFilePath}]", ConsoleColor.DarkGray);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log file: {ex.Message}");
            }
        }
    }
}