using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment13CollectionsAndGenerics
{
    internal class StringStack
    {
        private Stack<char> _stack = new Stack<char>();

        public void PerformOperations()
        {
            this.CreateStack("collection");
            string? originalString = this.GetOriginalString();
            string reversedString = this.GetReversedString();

            Console.WriteLine("Original String : " + originalString);
            Console.WriteLine("Reversed String : " + reversedString);
        }

        public void CreateStack(string value)
        {
            foreach (char ch in value)
            {
                this.Push(ch);
            }
        }

        public void Push(char ch)
        {
            this._stack.Push(ch);
        }

        public char Pop()
        {
            return this._stack.Pop();
        }

        private string? GetOriginalString()
        {
            char[] characters = this._stack.ToArray();
            Array.Reverse(characters);

            return new string(characters);
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
