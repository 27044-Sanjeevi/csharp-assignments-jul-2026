using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Assignment13CollectionsAndGenerics
{
    internal class BookList
    {
        private const string BookToRemove = "Atomic Habits";
        private const string BookToCheckExistence = "Lord of the Rings";

        private List<string> _books = new List<string>();

        public void PerformOperations()
        {
            this.AddFiveBooks();
            Console.WriteLine("[OPERATION] Added Five Books to the list.");

            bool removeResult = this.Remove(BookToRemove);
            Console.WriteLine($"[OPERATION] Removed the book {BookToRemove} from the list.");

            bool existence = this.ContainsBook(BookToCheckExistence);
            Console.WriteLine($"[OPERATION] Checked for existence of the book {BookToCheckExistence}. Result : {existence}");

            Console.WriteLine("\nALL BOOKS AFTER OPERATIONS:");
            this.DisplayAllBooks();
        }

        public void RemoveBook()
        {
            if (this._books.Count > 0)
            {
                this._books.RemoveAt(0);
            }
        }

        public bool ContainsBook(string book)
        {
            return this._books.Contains(book);
        }

        public void Add(string book)
        {
            this._books.Add(book);
        }

        public bool Remove(string book)
        {
            return this._books.Remove(book);
        }

        public void DisplayAllBooks()
        {
            foreach (string book in this._books)
            {
                Console.WriteLine("- " + book);
            }
        }

        private void AddFiveBooks()
        {
            this._books.Add("Atomic Habits");
            this._books.Add("Power of Mind");
            this._books.Add("Rich Dad, Poor Dad");
            this._books.Add("Harry Potter");
            this._books.Add("Lord of the Rings");
        }
    }
}
