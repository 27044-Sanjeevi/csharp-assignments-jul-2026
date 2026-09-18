using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Task2
{
    internal class VarKeywordDemo
    {
        public void RunDemo()
        {
            ConsoleHelpers.DisplaySubtitle("Var Keyword Demo");
            ConsoleHelpers.DisplayStatus("1. Initializing a new varible with var keyword..");
            ConsoleHelpers.PrintCodeSnippet("var stringValue = \"String value\";");
            var stringValue = "String value";

            ConsoleHelpers.DisplayStatus("2. Trying to assign a different type value to the variable..");

            // stringValue = 10; Compiler Error CS0029: Cannot implicitly convert type 'type' to 'type'
            ConsoleHelpers.PrintCodeSnippet("stringValue = 10;");
            ConsoleHelpers.DisplayFailure("This causes a Compiler Error CS0029: Cannot implicitly convert type 'type' to 'type'");

            ConsoleHelpers.DisplayStatus("3. Trying to create a varible with var keyword with no initialization...");

            // var value;
            ConsoleHelpers.PrintCodeSnippet("var value;");
            ConsoleHelpers.DisplayFailure("This causes a Compiler Error CS0818: Implicitly typed locals must be initialized");

            ConsoleHelpers.DisplayStatus("4. Trying to assign null to a variable with var keyword...");

            // var value = null;
            ConsoleHelpers.PrintCodeSnippet("var value = null;");
            ConsoleHelpers.DisplayFailure("This causes a Compiler Error: CS0815: Cannot assign 'expression' to an implicitly-typed variable.");
        }
    }
}
