using System.Reflection;
using System.Text;

namespace Assignment17Reflection.Tasks.Task7
{
    /// <summary>
    /// Represents the serializer using reflection.
    /// </summary>
    public static class ReflectionSerializer
    {
        /// <summary>
        /// Serialize the given object.
        /// </summary>
        /// <param name="targetObject">The object to be serialized.</param>
        /// <returns>The serialized string.</returns>
        public static string Serialize(object targetObject)
        {
            if (targetObject == null)
            {
                return "null";
            }

            Type type = targetObject.GetType();
            StringBuilder sb = new StringBuilder();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                object? fieldValue = field.GetValue(targetObject);
                if (fieldValue == null)
                {
                    throw new ArgumentNullException(nameof(fieldValue));
                }

                object value = fieldValue;
                sb.Append($"{field.Name}:{value};");
            }

            return sb.ToString();
        }
    }
}