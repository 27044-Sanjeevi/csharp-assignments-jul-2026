namespace Assignment13CollectionsAndGenerics.Collections
{
    /// <summary>
    /// Represents a generic person queue.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the queue.</typeparam>
    internal class PersonQueue<T>
    {
        private Queue<T> _personQueue = new Queue<T>();

        /// <summary>
        /// Performs a series of operations on a book collection.
        /// </summary>
        /// <param name="initialQueue">The initial queue elements.</param>
        public void PerformOperations(Queue<T> initialQueue)
        {
            ConsoleHelpers.DisplayTitle("TASK 3 : PERSON QUEUE");
            ConsoleHelpers.DisplayStatus("Created a new queue of persons.");

            this.AddMultipleItems(initialQueue);

            ConsoleHelpers.DisplayStatus("Added the persons into the queue.");
            this.DisplayAllItems();

            T? dequeuedElement = this.Dequeue();
            if (dequeuedElement is not null)
            {
                ConsoleHelpers.DisplayStatus($"Removed the person {dequeuedElement} from the queue.");
            }
            else
            {
                ConsoleHelpers.DisplayFailure("No elements left to remove in the queue.");
            }

            this.DisplayAllItems();
        }

        /// <summary>
        /// Enqueues the item into the queue.
        /// </summary>
        /// <param name="item">The item to be queued.</param>
        public void Enqueue(T item)
        {
            this._personQueue.Enqueue(item);
        }

        /// <summary>
        /// Dequeues the element from the queue.
        /// </summary>
        /// <returns>The dequeued element from the queue; null if queue is empty.</returns>
        public T? Dequeue()
        {
            if (this._personQueue.Count == 0)
            {
                return default;
            }

            return this._personQueue.Dequeue();
        }

        /// <summary>
        /// Displays all the items in the queue.
        /// </summary>
        public void DisplayAllItems()
        {
            ConsoleHelpers.WriteColored("\nCurrent Queue:\n", ConsoleColor.Cyan);
            foreach (T item in this._personQueue)
            {
                Console.WriteLine("- " + item);
            }
        }

        /// <summary>
        /// Adds multiple elements into the queue.
        /// </summary>
        /// <param name="items">The items to be added.</param>
        private void AddMultipleItems(Queue<T> items)
        {
            foreach (var item in items)
            {
                this.Enqueue(item);
            }
        }
    }
}
