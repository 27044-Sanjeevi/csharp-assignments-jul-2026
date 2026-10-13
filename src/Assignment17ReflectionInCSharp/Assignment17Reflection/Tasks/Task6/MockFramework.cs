using System.Reflection;
using System.Reflection.Emit;
using Assignment17Reflection.Utilities;

namespace Assignment17Reflection.Tasks.Task6
{
    /// <summary>
    /// Represents a mock framework.
    /// </summary>
    internal class MockFramework
    {
        /// <summary>
        /// Runs the current task.
        /// </summary>
        public void RunTask()
        {
            ConsoleHelpers.DisplaySubtitle("Initializing Dynamic Mock Test");
            ISampleUserInterface? mockRepo = Interface<ISampleUserInterface>();

            if (mockRepo == null)
            {
                return;
            }

            ConsoleHelpers.DisplayStatus($"Generated Mock Object Class Type: {mockRepo.GetType().FullName}");

            // Verifying if all methods return default values without crashing
            int userCount = mockRepo.GetUserCount();
            string email = mockRepo.GetUserEmail(42);
            bool isAdmin = mockRepo.IsAdmin(99);

            Console.WriteLine($"mockRepo.GetUserCount() returned: {userCount} (Expected: 0)");
            Console.WriteLine($"mockRepo.GetUserEmail(42) returned: {(email == null ? "null" : email)} (Expected: null)");
            Console.WriteLine($"mockRepo.IsAdmin(99) returned: {isAdmin} (Expected: False)");

            ConsoleHelpers.DisplayStatus("Executing mockRepo.DeleteUser(12)... ");
            mockRepo.DeleteUser(12);
            ConsoleHelpers.DisplaySuccess("Success (No exceptions thrown for void call).");

            ConsoleHelpers.DisplaySuccess("Unit Test Passed Successfully");
        }

        private static T? Interface<T>()
            where T : class
        {
            Type interfaceType = typeof(T);

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType.Name} must be an interface type to mock.");
            }

            AssemblyName assemblyName = new AssemblyName("DynamicMockAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MockModule");

            string typeName = $"{interfaceType.Name}_Mock";
            TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.BeforeFieldInit);

            typeBuilder.AddInterfaceImplementation(interfaceType);

            MethodInfo[] interfaceMethods = interfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo method in interfaceMethods)
            {
                ImplementMockMethod(typeBuilder, method);
            }

            Type? mockedType = typeBuilder.CreateTypeInfo()?.AsType();

            if (mockedType != null)
            {
                return (T?)Activator.CreateInstance(mockedType);
            }

            return null;
        }

        private static void ImplementMockMethod(TypeBuilder typeBuilder, MethodInfo interfaceMethod)
        {
            ParameterInfo[] parameters = interfaceMethod.GetParameters();
            Type[] parameterTypes = Array.ConvertAll(parameters, p => p.ParameterType);
            Type returnType = interfaceMethod.ReturnType;

            MethodAttributes methodAttributes =
                MethodAttributes.Public |
                MethodAttributes.Virtual |
                MethodAttributes.HideBySig |
                MethodAttributes.NewSlot;

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                interfaceMethod.Name,
                methodAttributes,
                returnType,
                parameterTypes);

            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);
            ILGenerator il = methodBuilder.GetILGenerator();

            if (returnType == typeof(void))
            {
                il.Emit(OpCodes.Ret);
            }
            else if (returnType.IsValueType)
            {
                LocalBuilder local = il.DeclareLocal(returnType);
                il.Emit(OpCodes.Ldloca_S, local);
                il.Emit(OpCodes.Initobj, returnType);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ret);
            }
        }
    }
}
