namespace Contracts
{
    /// <summary>
    /// Represents an image data.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData"/> class.
        /// </summary>
        public ImageData()
        {
            this.Height = 0;
            this.Width = 0;
            this.Format = string.Empty;
            this.Pixels = new byte[0, 0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData"/> class.
        /// </summary>
        /// <param name="height">The height of the image.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="format">The format of the image.</param>
        /// <param name="pixels">The pixel element array of the image.</param>
        public ImageData(int height, int width, string format, byte[,] pixels)
        {
            this.Height = height;
            this.Width = width;
            this.Format = format;
            this.Pixels = pixels;
        }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>An integer representing the height of the image.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        /// <value>An integer representing the width of the image.</value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the format of the image.
        /// </summary>
        /// <value>A string representing the format of the image.</value>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the pixels of the image.
        /// </summary>
        /// <value>A byte array representing the pixels of the image.</value>
        public byte[,] Pixels { get; set; }

        /// <summary>
        /// Displays the dimensions of the image.
        /// </summary>
        public void DisplayDimensions()
        {
            Console.WriteLine($"Height: {this.Height}" +
                $"\nWidth: {this.Width}");
        }
    }
}
