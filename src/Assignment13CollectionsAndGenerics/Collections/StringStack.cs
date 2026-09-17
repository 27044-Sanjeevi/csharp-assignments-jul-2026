using System.Text;

namespace Assignment13CollectionsAndGenerics.Collections
{
    /// <summary>
    /// Represents a generic collection for managing stack.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the book list.</typeparam>
    internal class StringStack<T>
    {
        private Stack<T> _stack = new Stack<T>();

        /// <summary>
        /// Performs a series of operations on the stack.
        /// </summary>
        /// <param name="initialItems">Initial items to be added.</param>
        public void PerformOperations(List<T> initialItems)
        {
            ConsoleHelpers.DisplayTitle("TASK 2 : STACK");
            ConsoleHelpers.DisplayStatus("Created a new stack.");
            this.PushItems(initialItems);
            string? originalString = this.GetOriginalString();
            string reversedString = this.GetReversedString();

            Console.WriteLine("Original String : " + originalString);
            Console.WriteLine("Reversed String : " + reversedString);
        }

        /// <summary>
        /// Pushes the item into the list.
        /// </summary>
        /// <param name="item">Item to be pushed.</param>
        public void Push(T item)
        {
            this._stack.Push(item);
        }

        /// <summary>
        /// Pops an item from the list.
        /// </summary>
        /// <returns>Item popped from the stack.</returns>
        public T Pop()
        {
            return this._stack.Pop();
        }

        /// <summary>
        /// Pushes a collection of items onto the stack.
        /// </summary>
        /// <param name="initialItems">The list of items to push onto the stack.</param>
        public void PushItems(List<T> initialItems)
        {
            foreach (T item in initialItems)
            {
                this.Push(item);
            }
        }

        private string? GetOriginalString()
        {
            T[] stackArray = this._stack.ToArray();
            Array.Reverse(stackArray);

            return string.Join(string.Empty, stackArray);
        }

        private string GetReversedString()
        {
            StringBuilder result = new StringBuilder();
            while (this._stack.Count > 0)
            {
                result.Append(this.Pop());
            }

            return result.ToString();
        }
    }
}
