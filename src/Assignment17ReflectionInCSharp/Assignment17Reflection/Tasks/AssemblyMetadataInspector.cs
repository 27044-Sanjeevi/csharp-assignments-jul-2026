using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Assignment17Reflection.Utilities;

namespace Assignment17Reflection.Tasks
{
    internal class AssemblyMetadataInspector
    {
        public void RunInspection(string assemblyFilePath)
        {
            try
            {
                ConsoleHelpers.DisplayStatus("Loading an assembly using Assembly.LoadFile()");
                Assembly targetAssembly = Assembly.LoadFile(assemblyFilePath);
                ConsoleHelpers.DisplaySuccess($"Loaded assembly ({assemblyFilePath}) successfully.");

                Type[] definedTypes = targetAssembly.GetTypes();
                BindingFlags searchCriteria = BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.Instance |
                                                  BindingFlags.Static;

                ConsoleHelpers.DisplayStatus($"{definedTypes.Length} types found in the assembly.");
                ConsoleHelpers.DisplayStatus("Looping through all the defined types in the assembly");
                foreach (Type type in definedTypes)
                {
                    ConsoleHelpers.DisplaySubtitle($"{type.FullName}");
                    this.DisplayTypeFields(type, searchCriteria);
                    this.DisplayTypeMethods(type, searchCriteria);
                    this.DisplayTypeEvents(type, searchCriteria);
                    ConsoleHelpers.PrintLine();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("[ERROR] The provided assembly path is invalid.");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("[ERROR] Failed to locate the file on disk.");
            }
        }

        private void DisplayTypeFields(Type type, BindingFlags searchCriteria)
        {
            FieldInfo[] fields = type.GetFields(searchCriteria);
            ConsoleHelpers.WriteLineColored($"{fields.Length} fields found.", ConsoleColor.Cyan);
            foreach (FieldInfo field in fields)
            {
                string visibility = field.IsPublic ? "Public" : "Private/NonPublic";
                ConsoleHelpers.WriteLineColored($"-> [{visibility}] Type: {field.FieldType.Name} | Identifier: {field.Name}", ConsoleColor.DarkBlue);
            }
        }

        private void DisplayTypeMethods(Type type, BindingFlags searchCriteria)
        {
            MethodInfo[] methods = type.GetMethods(searchCriteria);
            ConsoleHelpers.WriteLineColored($"{methods.Length} methods found.", ConsoleColor.Cyan);
            foreach (MethodInfo method in methods)
            {
                //if (method.IsSpecialName)
                //{
                //    continue;
                //}

                string visibility = method.IsPublic ? "Public" : "Private/NonPublic";
                ConsoleHelpers.WriteLineColored($"-> [{visibility}] Returns: {method.ReturnType.Name} | Signature: {method.Name}()", ConsoleColor.Green);
            }
        }

        private void DisplayTypeEvents(Type type, BindingFlags searchCriteria)
        {
            EventInfo[] events = type.GetEvents(searchCriteria);
            ConsoleHelpers.WriteLineColored($"{events.Length} events found.", ConsoleColor.Cyan);
            foreach (EventInfo ev in events)
            {
                ConsoleHelpers.WriteLineColored($"-> Handler: {ev.EventHandlerType?.Name} | Identifier: {ev.Name}", ConsoleColor.Magenta);
            }
        }
    }
}
