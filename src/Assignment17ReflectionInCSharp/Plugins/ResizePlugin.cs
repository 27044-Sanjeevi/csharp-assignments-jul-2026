using Contracts;

namespace Plugins
{
    /// <summary>
    /// Represents the resize filter for the image.
    /// </summary>
    public class ResizePlugin : IImagePlugin
    {
        /// <inheritdoc/>
        public string Name => "Resize Plugin";

        /// <inheritdoc/>
        public string Description => "This plugin resizes images to a specified width and height.";

        /// <inheritdoc/>
        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Resized image: {imagePath}");
            return new ImageData();
        }
    }
}
