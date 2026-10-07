using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment17Reflection.Utilities;

namespace Assignment17Reflection.Tasks
{
    internal class DynamicMethodInvoker
    {
        /// <summary>
        /// Dynamically locates and invokes a method on a target object by its string name.
        /// </summary>
        /// <param name="targetObject">The object instance containing the method.</param>
        /// <param name="methodName">The string identifier of the method to run.</param>
        /// <param name="parameters">Optional array of arguments required by the target method.</param>
        public void InvokeMethod(object targetObject, string methodName, object[]? parameters = null)
        {
            if (targetObject == null)
            {
                ConsoleHelpers.DisplayFailure("[ERROR] Target object reference cannot be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                ConsoleHelpers.DisplayFailure("[ERROR] Method name parameter cannot be empty.");
                return;
            }

            Type type = targetObject.GetType();

            try
            {
                MethodInfo? method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (method == null)
                {
                    ConsoleHelpers.DisplayFailure($"Method '{methodName}' matching required signatures was not found on Type '{type.Name}'.");
                    return;
                }

                ConsoleHelpers.DisplayStatus($"Dynamically invoking method '{method.Name}' on '{type.Name}'");
                object? result = method.Invoke(targetObject, parameters);
                if (method.ReturnType != typeof(void))
                {
                    ConsoleHelpers.DisplaySuccess($"Execution Completed Successfully. Return Value: {result ?? "null"}");
                }
                else
                {
                    ConsoleHelpers.DisplaySuccess("Execution Completed Successfully. (Method returned void)");
                }
            }
            catch (TargetInvocationException ex)
            {
                Exception? underlyingBug = ex.InnerException;
                string errorMessage = underlyingBug != null ? underlyingBug.Message : ex.Message;

                ConsoleHelpers.DisplayFailure($"Runtime Exception inside '{methodName}': {errorMessage}");
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayFailure($"Reflection engine execution failure: {ex.Message}");
            }
        }
    }
}
