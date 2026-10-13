using Contracts;

namespace Plugins
{
    /// <summary>
    /// Represents the invert filter for the image.
    /// </summary>
    public class InvertPlugin : IImagePlugin
    {
        /// <inheritdoc/>
        public string Name => "Invert Plugin";

        /// <inheritdoc/>
        public string Description => "This plugin inverts the colors of images.";

        /// <inheritdoc/>
        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Inverted image: {imagePath}");
            return new ImageData();
        }
    }
}
