namespace GarbageCollection
{
    /// <summary>
    /// Represents a dummy receipt.
    /// </summary>
    internal class DummyReceipt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyReceipt"/> class.
        /// </summary>
        /// <param name="id">The Id of the receipt.</param>
        /// <param name="description">The description of the receipt.</param>
        public DummyReceipt(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }

        /// <summary>
        /// Gets the Id of the receipt.
        /// </summary>
        /// <value>An integer holding the Id of the receipt.</value>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the description of the receipt.
        /// </summary>
        /// <value>A string holding the description of the receipt.</value>
        public string Description { get; set; }
    }
}
