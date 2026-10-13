namespace Contracts
{
    /// <summary>
    /// Specifies the interface for the image plugins.
    /// </summary>
    public interface IImagePlugin
    {
        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>A string holding the name of the image.</value>
        string Name { get; }

        /// <summary>
        /// Gets the description of the image.
        /// </summary>
        /// <value>A string holding the description of the image.</value>
        string Description { get; }

        /// <summary>
        /// Processes the given image in the given path.
        /// </summary>
        /// <param name="imagePath">The path of the image file.</param>
        /// <returns>The processed image data.</returns>
        ImageData ProcessImage(string imagePath);
    }
}
