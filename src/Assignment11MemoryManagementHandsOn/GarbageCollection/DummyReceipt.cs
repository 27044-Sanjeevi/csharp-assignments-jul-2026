using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollection
{
    internal class DummyReceipt
    {
        public DummyReceipt(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }

        public int Id { get; }

        public string Description { get; set; }
    }
}
