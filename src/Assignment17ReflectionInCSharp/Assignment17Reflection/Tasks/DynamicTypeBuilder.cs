using System.Reflection;
using System.Reflection.Emit;
using Assignment17Reflection.Utilities;

namespace Assignment17Reflection.Tasks
{
    /// <summary>
    /// Represents a dynamic type builder using reflection concepts.
    /// </summary>
    internal class DynamicTypeBuilder
    {
        /// <summary>
        /// Runs the current task.
        /// </summary>
        public void RunTask()
        {
            Type? myDynamicType = this.CreateImageMetadataType();
            if (myDynamicType == null)
            {
                Console.WriteLine("Type is null");
                return;
            }

            object? dynamicInstance = Activator.CreateInstance(myDynamicType);
            PropertyInfo? propInfo = myDynamicType.GetProperty("Format");
            if (propInfo != null)
            {
                propInfo.SetValue(dynamicInstance, "Sample Format");
            }

            MethodInfo? methodInfo = myDynamicType.GetMethod("PrintSummary");
            if (methodInfo != null)
            {
                Console.WriteLine();
                methodInfo.Invoke(dynamicInstance, null);
            }
        }

        /// <summary>
        /// Programmatically builds a new type signature at runtime using raw memory IL injection.
        /// </summary>
        /// <returns>The newly generated, live structural Type token definition.</returns>
        public Type? CreateImageMetadataType()
        {
            ConsoleHelpers.DisplayStatus("Starting Dynamic Type Generation");
            AssemblyName assemblyName = new AssemblyName("DynamicContractsEngine");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicContractsModule");

            string runtimeClassName = "Contracts.DynamicImageProfile";
            TypeBuilder typeBuilder = moduleBuilder.DefineType(runtimeClassName, TypeAttributes.Public | TypeAttributes.Class);

            ConsoleHelpers.WriteLineColored($" -> [TypeBuilder] Created Type container row: {runtimeClassName}", ConsoleColor.DarkGray);
            FieldBuilder fieldFormat = typeBuilder.DefineField("_format", typeof(string), FieldAttributes.Private);
            PropertyBuilder propertyFormat = typeBuilder.DefineProperty("Format", PropertyAttributes.None, typeof(string), null);
            MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            MethodBuilder getterMethodBuilder = typeBuilder.DefineMethod("get_Format", getSetAttributes, typeof(string), Type.EmptyTypes);

            ILGenerator getterIL = getterMethodBuilder.GetILGenerator();
            getterIL.Emit(OpCodes.Ldarg_0);
            getterIL.Emit(OpCodes.Ldfld, fieldFormat);
            getterIL.Emit(OpCodes.Ret);

            MethodBuilder setterMethodBuilder = typeBuilder.DefineMethod("set_Format", getSetAttributes, null, new Type[] { typeof(string) });

            ILGenerator setterIL = setterMethodBuilder.GetILGenerator();
            setterIL.Emit(OpCodes.Ldarg_0);
            setterIL.Emit(OpCodes.Ldarg_1);
            setterIL.Emit(OpCodes.Stfld, fieldFormat);
            setterIL.Emit(OpCodes.Ret);

            propertyFormat.SetGetMethod(getterMethodBuilder);
            propertyFormat.SetSetMethod(setterMethodBuilder);

            ConsoleHelpers.WriteLineColored(" -> [PropertyBuilder] Emitted Property metadata 'Format' and IL Accessors.", ConsoleColor.DarkGray);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("PrintSummary", MethodAttributes.Public, null, Type.EmptyTypes);

            MethodInfo? consoleWriteLineMethod = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });

            ILGenerator methodIL = methodBuilder.GetILGenerator();
            if (consoleWriteLineMethod == null)
            {
                throw new ArgumentNullException(nameof(consoleWriteLineMethod));
            }

            methodIL.Emit(OpCodes.Ldstr, "SUMMARY:");
            methodIL.Emit(OpCodes.Call, consoleWriteLineMethod);
            methodIL.Emit(OpCodes.Ldstr, "Image Format value evaluates onto heap as: ");

            MethodInfo? methodInfo = typeof(Console).GetMethod("Write", new Type[] { typeof(string) });
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            methodIL.Emit(OpCodes.Call, methodInfo);

            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Call, getterMethodBuilder);
            methodIL.Emit(OpCodes.Call, consoleWriteLineMethod);
            methodIL.Emit(OpCodes.Ret);

            ConsoleHelpers.WriteLineColored(" -> [MethodBuilder] Injected Intermediate Language opcodes for 'PrintSummary()'.", ConsoleColor.DarkGray);

            Type? dynamicType = typeBuilder?.CreateTypeInfo()?.AsType();

            ConsoleHelpers.DisplaySuccess($"Successfully generated a type at runtime.");
            return dynamicType;
        }
    }
}
