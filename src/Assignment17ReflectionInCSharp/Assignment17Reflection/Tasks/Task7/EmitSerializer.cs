using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Assignment17Reflection.Tasks.Task7
{
    /// <summary>
    /// Represents the serializer using Reflection.Emit.
    /// </summary>
    public static class EmitSerializer
    {
        /// <summary>
        /// Represents a delegate for serialization.
        /// </summary>
        /// <param name="targetObject">The target object instance.</param>
        /// <param name="stringBuilder">The string builder.</param>
        public delegate void SerializeDelegate(object targetObject, StringBuilder stringBuilder);

        /// <summary>
        /// Creates the serializer.
        /// </summary>
        /// <param name="type">The type for serialization.</param>
        /// <returns>The serializer delegate.</returns>
        public static SerializeDelegate CreateSerializer(Type type)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(
                $"Serialize_{type.Name}",
                null,
                new[] { typeof(object), typeof(StringBuilder) },
                typeof(EmitSerializer).Module,
                skipVisibility: true);

            ILGenerator il = dynamicMethod.GetILGenerator();

            LocalBuilder typedInstance = il.DeclareLocal(type);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Unbox_Any, type);
            il.Emit(OpCodes.Stloc, typedInstance);

            MethodInfo? stringMethodInfo = typeof(StringBuilder).GetMethod("Append", new[] { typeof(string) });
            if (stringMethodInfo == null)
            {
                throw new ArgumentNullException(nameof(stringMethodInfo), "Failed to reflectively look up the StringBuilder.Append(string) method definition.");
            }

            MethodInfo appendString = stringMethodInfo;

            MethodInfo? intMethodInfo = typeof(StringBuilder).GetMethod("Append", new[] { typeof(int) });
            if (intMethodInfo == null)
            {
                throw new ArgumentNullException(nameof(intMethodInfo), "Failed to reflectively look up the StringBuilder.Append(int) method definition.");
            }

            MethodInfo appendInt = intMethodInfo;

            MethodInfo? boolMethodInfo = typeof(StringBuilder).GetMethod("Append", new[] { typeof(bool) });
            if (boolMethodInfo == null)
            {
                throw new ArgumentNullException(nameof(boolMethodInfo), "Failed to reflectively look up the StringBuilder.Append(bool) method definition.");
            }

            MethodInfo appendBool = boolMethodInfo;

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldstr, $"{field.Name}:");
                il.Emit(OpCodes.Callvirt, appendString);
                il.Emit(OpCodes.Pop);

                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldloc, typedInstance);
                il.Emit(OpCodes.Ldfld, field);

                if (field.FieldType == typeof(int))
                {
                    il.Emit(OpCodes.Callvirt, appendInt);
                }
                else if (field.FieldType == typeof(bool))
                {
                    il.Emit(OpCodes.Callvirt, appendBool);
                }
                else if (field.FieldType == typeof(string))
                {
                    il.Emit(OpCodes.Callvirt, appendString);
                }
                else
                {
                    MethodInfo? toStringMethod = typeof(object)?.GetMethod("ToString");
                    if (toStringMethod != null)
                    {
                        il.Emit(OpCodes.Callvirt, toStringMethod);
                    }

                    il.Emit(OpCodes.Callvirt, appendString);
                }

                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldstr, ";");
                il.Emit(OpCodes.Callvirt, appendString);
                il.Emit(OpCodes.Pop);
            }

            il.Emit(OpCodes.Ret);

            return (SerializeDelegate)dynamicMethod.CreateDelegate(typeof(SerializeDelegate));
        }
    }
}