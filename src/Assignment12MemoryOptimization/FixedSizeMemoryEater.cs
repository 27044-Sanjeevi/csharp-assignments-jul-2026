using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12MemoryOptimization
{
    internal class FixedSizeMemoryEater
    {
        private readonly List<int> _memAlloc = new List<int>(1000);

        internal void Allocate()
        {
            while (true)
            {
                // operations on memAlloc
                Thread.Sleep(10);
            }
        }
    }
}
