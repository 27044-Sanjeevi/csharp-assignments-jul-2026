using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ImageData
    {
        public ImageData()
        {
            Height = 0;
            Width = 0;
            Format = string.Empty;
            Pixels = new byte[0, 0];
        }

        public ImageData(int height, int width, string format, byte[,] pixels)
        {
            Height = height;
            Width = width;
            Format = format;
            Pixels = pixels;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public string Format { get; set; }

        public byte[,] Pixels { get; set; }

    }
}
