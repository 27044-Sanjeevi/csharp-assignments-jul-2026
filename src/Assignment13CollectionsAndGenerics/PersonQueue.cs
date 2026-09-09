using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment13CollectionsAndGenerics
{
    internal class PersonQueue
    {
        private Queue<string> _personQueue = new Queue<string>();

        public void PerformOperations()
        {
            this.AddFivePersons();

        }

        public void Enqueue(string name)
        {
            this._personQueue.Enqueue(name);
        }

        public string Dequeue()
        {
            return this._personQueue.Dequeue();
        }

        private void AddFivePersons()
        {
            this.Enqueue("Arun");
            this.Enqueue("Bala");
            this.Enqueue("Cbum");
            this.Enqueue("Dicaprio");
            this.Enqueue("Elias");
        }
    }
}
