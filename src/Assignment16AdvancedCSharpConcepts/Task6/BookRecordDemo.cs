namespace Assignment16AdvancedCSharpConcepts.Task6
{
    /// <summary>
    /// Represents the demo for the task 6.
    /// </summary>
    internal class BookRecordDemo
    {
        /// <summary>
        /// Runs the demonstration of task 6.
        /// </summary>
        public void RunDemo()
        {
            ConsoleHelpers.DisplayStatus("Creating Book records..");

            Book book1 = new ("Atomic Habits", "James Clear", "978-0-123456-12-3");
            Book book2 = new ("Power of Habit", "Charles Duhigg", "123-0-763456-45-6");

            ConsoleHelpers.DisplayStatus("All book details: ");
            this.DisplayBook("Book 1", book1);
            this.DisplayBook("Book 2", book2);

            Book book1Copy = new ("Atomic Habits", "James Clear", "978-0-123456-12-3");

            ConsoleHelpers.DisplayStatus("Comparing two Book records with same properties..");
            bool result = book1 == book1Copy;

            // book1.Author = "Harry"; throws compilation error CS8852: Init-only property or indexer 'property' can only be assigned in an object initializer,
            // or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            ConsoleHelpers.DisplaySuccessMessage($"Comparison Result : {result}");
            Book modifiedBook2 = book2 with
            {
                author = "Harry",
            };

            ConsoleHelpers.DisplayStatus($"Created a modified Book 2 using with keyword...");
            this.DisplayBook("Original Book 2", book2);
            this.DisplayBook("Modified Book 2", modifiedBook2);
        }

        private void DisplayBook(string message, Book book)
        {
            var (title, author, isbn) = book; // deconstruction

            Console.WriteLine($"[{message}] {title}, {author}, {isbn}");
        }
    }
}
