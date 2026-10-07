using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class InvertPlugin : IImagePlugin
    {
        public string Name => "Invert Plugin";
        public string Description => "This plugin inverts the colors of images.";
        public ImageData ProcessImage(string imagePath)
        {
            Console.WriteLine($"Inverted image: {imagePath}");
            return new ImageData();
        }
    }
}
