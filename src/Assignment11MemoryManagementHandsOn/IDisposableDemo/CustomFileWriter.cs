namespace IDisposableDemo
{
    using System;

    /// <summary>
    /// Represents a custom file writer with IDisposable interface implementation.
    /// </summary>
    internal class CustomFileWriter : IDisposable
    {
        private StreamWriter _streamWriter;
        private string _filePath;
        private bool _isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFileWriter"/> class.
        /// </summary>
        /// <param name="filePath">The path of the file to write into.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="filePath"/> is null.</exception>
        public CustomFileWriter(string filePath)
        {
            this._filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this._streamWriter = new StreamWriter(filePath);
        }

        /// <summary>
        /// Writes the given text into the <see cref="_filePath"/> file.
        /// </summary>
        /// <param name="text">The text to be written into the file.</param>
        public void Write(string text)
        {
            this.ThrowIfDisposed();
            if (text == null)
            {
                return;
            }

            this._streamWriter.WriteLine(text);
        }

        /// <summary>
        /// Disposes the file resource by releasing the operating system's file handle.
        /// </summary>
        public void Dispose()
        {
            if (this._isDisposed)
            {
                return;
            }

            if (this._streamWriter != null)
            {
                this._streamWriter.Dispose();
                Console.WriteLine("Dispose() Method is called automatically after the scope of using statement got over.\n" +
                    "File resources got released.\n");
            }

            this._isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException($"The filePath {this._filePath} has been disposed already.");
            }
        }
    }
}
