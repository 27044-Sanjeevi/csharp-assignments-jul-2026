namespace Assignment16AdvancedCSharpConcepts.Task2
{
    /// <summary>
    /// Represents the operations to demonstrate the dynamic keyword in task 6.
    /// </summary>
    internal class DynamicKeywordDemo
    {
        /// <summary>
        /// Runs the operations to demonstrate the dynamic keyword in task 6.
        /// </summary>
        public void RunDemo()
        {
            ConsoleHelpers.DisplaySubtitle("Dynamic Keyword Demo");
            ConsoleHelpers.DisplayStatus("1. Initializing a dynamic variable with dynamic keyword..");
            dynamic stringValue = "String value";
            ConsoleHelpers.PrintCodeSnippet("dynamic stringValue = \"String value\";");

            ConsoleHelpers.DisplayStatus("2. Trying to assign a different type value to the variable..");

            stringValue = 10;
            ConsoleHelpers.PrintCodeSnippet("stringValue = 10;");
            ConsoleHelpers.DisplaySuccessMessage("This runs fine with no errors because the type if inferred at runtime by the DLR (Dynamic Language Runtime).");

            ConsoleHelpers.DisplayStatus("3. Trying to create a variable with var keyword with no initialization...");

            dynamic value;
            value = "String";
            ConsoleHelpers.PrintCodeSnippet("dynamic value;");
            ConsoleHelpers.DisplaySuccessMessage("This is legal because dynamic is a real, concrete data type at compile-time, whereas var is not.");

            dynamic listObject = new List<int>();

            // listObject.UnknownMethod(); this throws Microsoft.CSharp.RuntimeBinder.RuntimeBinderException at runtime since the binding is not successful
            ConsoleHelpers.DisplayStatus("4. Calling a method which doesn't exist in the class definition..");
            ConsoleHelpers.PrintCodeSnippet("dynamic listObject = new List<int>();\nlistObject.ToString()");
            ConsoleHelpers.DisplaySuccessMessage("This doesn't cause any compile time error because the compiler assumes that the unknown method exits..");
            ConsoleHelpers.DisplayFailure("But this throws Microsoft.CSharp.RuntimeBinder.RuntimeBinderException at runtime since the binding is not successful.");
        }
    }
}
