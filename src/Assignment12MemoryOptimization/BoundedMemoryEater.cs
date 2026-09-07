using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12MemoryOptimization
{
    /// <summary>
    /// Optimizes memory allocation by maintaining a list of integer arrays with a maximum count.
    /// </summary>
    internal class BoundedMemoryEater
    {
        private const int MaxListCount = 100000;
        private const int IntegerArraySize = 1000;
        private const int SleepTimeForAdditionInMs = 1;

        private readonly List<int[]> _memAlloc = new List<int[]>();

        /// <summary>
        /// Continuously allocates a new integer array and adds it to the memory allocation list, removes the first element if the maximum count is reached.
        /// </summary>
        public void Allocate()
        {
            while (true)
            {
                if (this._memAlloc.Count >= MaxListCount)
                {
                    this._memAlloc.RemoveAt(0);
                }

                this._memAlloc.Add(new int[IntegerArraySize]);

                // operations with memAlloc
                Thread.Sleep(SleepTimeForAdditionInMs);
            }
        }
    }
}
