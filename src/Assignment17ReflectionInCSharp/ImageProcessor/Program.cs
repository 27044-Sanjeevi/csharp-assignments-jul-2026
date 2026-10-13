using System.Reflection;
using Contracts;

namespace ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string pluginPath =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Plugins",
                    "Plugins.dll");

            Console.WriteLine($"Plugin Path:\n{pluginPath}");

            Console.WriteLine();

            if (!File.Exists(pluginPath))
            {
                Console.WriteLine("Plugins.dll does not exist.");
                return;
            }

            Console.WriteLine("Plugins.dll found.");

            Assembly assembly = Assembly.LoadFrom(pluginPath);
            Console.WriteLine($"Assembly loaded: {assembly.FullName}\n");
            Console.WriteLine("All types inside Plugins.dll:\n");
            foreach (Type type in assembly.GetTypes())
            {
                Console.WriteLine("- " + type.FullName);
            }

            var pluginTypes = assembly
                .GetTypes()
                .Where(type =>
                        type.IsClass &&
                        !type.IsAbstract &&
                        typeof(IImagePlugin).IsAssignableFrom(type));

            foreach (Type type in pluginTypes)
            {
                if (!type.IsClass || type.IsAbstract)
                {
                    continue;
                }

                object? instance = Activator.CreateInstance(type);

                if (instance is IImagePlugin plugin)
                {
                    Console.WriteLine($"\nPlugin created: {plugin.Name}");
                    Console.WriteLine($"Description: {plugin.Description}");
                }
            }

            Console.ReadKey();
        }
    }
}
