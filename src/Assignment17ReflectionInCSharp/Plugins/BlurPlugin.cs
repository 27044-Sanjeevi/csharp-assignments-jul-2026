using Contracts;

namespace Plugins
{
    /// <summary>
    /// Represents the blur plugin for the image.
    /// </summary>
    public class BlurPlugin : IImagePlugin
    {
        /// <inheritdoc/>
        public string Name => "Blur Plugin";

        /// <inheritdoc/>
        public string Description => "This plugin applies a blur effect to images.";

        /// <inheritdoc/>
        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Blurred image: {imagePath}");
            return new ImageData();
        }
    }
}
