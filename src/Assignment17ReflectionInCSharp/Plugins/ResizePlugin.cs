using Contracts;

namespace Plugins
{
    public class ResizePlugin : IImagePlugin
    {
        public string Name => "Resize Plugin";

        public string Description => "This plugin resizes images to a specified width and height.";

        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Resized image: {imagePath}");
            return new ImageData();
        }
    }
}
