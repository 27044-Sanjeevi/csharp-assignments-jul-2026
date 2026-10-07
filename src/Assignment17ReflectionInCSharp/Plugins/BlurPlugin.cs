using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Plugins
{
    public class BlurPlugin : IImagePlugin
    {
        public string Name => "Blur Plugin";
        public string Description => "This plugin applies a blur effect to images.";
        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Blurred image: {imagePath}");
            return new ImageData();
        }
    }
}
