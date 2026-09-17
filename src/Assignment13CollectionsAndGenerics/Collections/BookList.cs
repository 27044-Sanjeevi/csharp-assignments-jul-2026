namespace Assignment13CollectionsAndGenerics.Collections
{
    /// <summary>
    /// Represents a generic collection for managing books.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the book list.</typeparam>
    internal class BookList<T>
    {
        private List<T> _books = new List<T>();

        /// <summary>
        /// Performs a series of operations on a book collection.
        /// </summary>
        /// <param name="initialBooks">The list of books to add to the collection.</param>
        /// <param name="bookToRemove">The book to remove from the collection.</param>
        /// <param name="bookToCheckExistence">The book to check for existence in the collection.</param>
        public void PerformOperations(List<T> initialBooks, T bookToRemove, T bookToCheckExistence)
        {
            ConsoleHelpers.DisplayTitle("TASK 1 : BOOK LIST");
            ConsoleHelpers.DisplayStatus("Created a new list of books.");

            this.AddMultipleItems(initialBooks);
            ConsoleHelpers.DisplayStatus("Added the book details into the list.");
            this.DisplayAllItems();

            if (this.Remove(bookToRemove))
            {
                ConsoleHelpers.DisplayStatus($"Removed the book \"{bookToRemove}\" from the list.");
            }
            else
            {
                ConsoleHelpers.DisplayFailure($"The book \"{bookToRemove}\" doesn't exist in the list.");
            }

            bool existence = this.Contains(bookToCheckExistence);
            ConsoleHelpers.DisplayStatus($"Checked for existence of the book {bookToCheckExistence}. Result : {existence}");
            this.DisplayAllItems();
        }

        /// <summary>
        /// Checks if the collection contains the given item.
        /// </summary>
        /// <param name="item">The item to check its existence in the collection.</param>
        /// <returns>True if <see cref="item"/> exists in the collection; otherwise false.</returns>
        public bool Contains(T item)
        {
            return this._books.Contains(item);
        }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The item to be added to the collection.</param>
        public void Add(T item)
        {
            this._books.Add(item);
        }

        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="item">The item to be removed from the collection.</param>
        /// <returns>true if the item gets removed successfully; false otherwise.</returns>
        public bool Remove(T item)
        {
            return this._books.Remove(item);
        }

        /// <summary>
        /// Displays all the books in the collection.
        /// </summary>
        public void DisplayAllItems()
        {
            ConsoleHelpers.WriteColored("\nCurrent List:\n", ConsoleColor.Cyan);
            foreach (T item in this._books)
            {
                Console.WriteLine("- " + item);
            }
        }

        /// <summary>
        /// Adds multiple items to the collection.
        /// </summary>
        /// <param name="items">The items to be added.</param>
        private void AddMultipleItems(List<T> items)
        {
            foreach (T item in items)
            {
                this.Add(item);
            }
        }
    }
}
