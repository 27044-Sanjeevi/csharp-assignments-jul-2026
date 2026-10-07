using System.Reflection;
using Assignment17Reflection.Utilities;
using Contracts;

namespace Assignment17Reflection.Tasks
{
    /// <summary>
    /// Represents the plugin application.
    /// </summary>
    internal class PluginApplication
    {
        /// <summary>
        /// Runs the image processor.
        /// </summary>
        /// <param name="sampleImage">The image data to be processed.</param>
        public void RunImageProcessor(ImageData sampleImage)
        {
            string pluginsFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
            string sampleImagePath = "sampleImagePath.png";

            ConsoleHelpers.DisplayStatus($"Scanning Plugin Directory: {pluginsFolder}");

            if (!Directory.Exists(pluginsFolder))
            {
                ConsoleHelpers.DisplayFailure($"Plugins directory not found. Creating path folder context");
                Directory.CreateDirectory(pluginsFolder);
                return;
            }

            string[] pluginFiles = Directory.GetFiles(pluginsFolder, "*.dll");

            if (pluginFiles.Length == 0)
            {
                ConsoleHelpers.DisplayFailure("No compiled plugin assembly files (.dll) discovered inside directory.");
                return;
            }

            foreach (string fileFile in pluginFiles)
            {
                try
                {
                    ConsoleHelpers.WriteLineColored($"\nLoading Assembly via LoadFile: {Path.GetFileName(fileFile)}", ConsoleColor.DarkGray);

                    Assembly assembly = Assembly.LoadFile(fileFile);
                    ConsoleHelpers.WriteLineColored($" -> Successfully initialized: {assembly.FullName}", ConsoleColor.DarkGreen);

                    Type[] allTypes = assembly.GetTypes();

                    foreach (Type type in allTypes)
                    {
                        if (!type.IsClass || type.IsAbstract)
                        {
                            continue;
                        }

                        Type? interfaceMatch = type.GetInterface(typeof(IImagePlugin).FullName ?? typeof(IImagePlugin).Name);

                        if (interfaceMatch != null)
                        {
                            ConsoleHelpers.WriteLineColored($" -> Target class implementing interface: {type.FullName}", ConsoleColor.Cyan);
                            object? rawInstance = Activator.CreateInstance(type);

                            if (rawInstance is IImagePlugin plugin)
                            {
                                ConsoleHelpers.DisplaySuccess($"Loaded Plugin Context: {plugin.Name}");
                                ConsoleHelpers.WriteLineColored($" Description: {plugin.Description}", ConsoleColor.DarkGray);
                                ConsoleHelpers.DisplayStatus($"Calling the process method available in the plugin");
                                plugin.ProcessImage(sampleImagePath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelpers.DisplayFailure($"Failed to process plugin assembly context: {ex.Message}");
                }
            }
        }
    }
}
