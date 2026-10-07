using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment17Reflection.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment17Reflection.Tasks
{
    internal class DynamicObjectInspector
    {
        public void RunInspection(object targetObject)
        {
            this.InspectObject(targetObject);
            this.TryUpdatePropertyValue(targetObject);
        }

        /// <summary>
        /// Scans an object instance dynamically and displays all properties.
        /// </summary>
        public void InspectObject(object targetObject)
        {
            if (targetObject == null)
            {
                ConsoleHelpers.DisplayFailure("[ERROR] Cannot inspect a null object reference.");
                return;
            }

            Type type = targetObject.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ConsoleHelpers.DisplayStatus($"INSPECTING OBJECT TYPE: {type.FullName}");

            foreach (PropertyInfo prop in properties)
            {
                object? rawValue = prop.GetValue(targetObject, null);
                string? stringValue = rawValue != null ? rawValue.ToString() : "null";

                ConsoleHelpers.WriteLineColored($"-> {prop.Name} ({prop.PropertyType.Name}) = {stringValue}", ConsoleColor.Cyan);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Step 5: Modifies a target property string key dynamically using defensive runtime type conversion.
        /// </summary>
        public bool TryUpdatePropertyValue(object targetObject)
        {
            if (targetObject == null)
            {
                return false;
            }

            string propertyName = ConsoleHelpers.ReadString("Enter the exact Property Name you wish to modify: ");
            string rawInputValue = ConsoleHelpers.ReadString("Enter the New Value for this property: ");

            Type type = targetObject.GetType();
            PropertyInfo? prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
            {
                ConsoleHelpers.DisplayFailure($"Property '{propertyName}' was not found on Type '{type.Name}'.");
                return false;
            }

            if (!prop.CanWrite)
            {
                ConsoleHelpers.DisplayFailure($"\nProperty '{propertyName}' is Read-Only (Cannot perform SetValue).");
                return false;
            }

            try
            {
                object? value;

                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (string.IsNullOrEmpty(rawInputValue))
                    {
                        value = null;
                    }
                    else
                    {
                        Type? underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);

                        if (underlyingType == null)
                        {
                            throw new InvalidOperationException("Could not determine the type of the nullable property.");
                        }

                        value = Convert.ChangeType(rawInputValue, underlyingType);
                    }
                }
                else
                {
                    value = Convert.ChangeType(rawInputValue, prop.PropertyType);
                }

                prop.SetValue(targetObject, value, null);

                ConsoleHelpers.DisplaySuccess($"\nSuccessfully updated '{propertyName}' to values: {rawInputValue}");
                return true;
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayFailure($"\nData Type Mismatch Error: Failed to parse '{rawInputValue}' into {prop.PropertyType.Name}. Detail: {ex.Message}");
                return false;
            }
        }
    }
}
