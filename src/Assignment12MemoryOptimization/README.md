# Assignment 12 - Memory Optimization

## Task 1 - Memory Eater
Here the class `MemmoryEater` contains the method `Allocate()` which continuously allocates memory by adding integer arrays to a collection, leading to high memory usage.

### Inferences
- The diagnostic tool reveals that the memory usage increases linearly with time as the list grows each `10ms`.
- Since `1000` integers are added into the list during each iteration, the list grows by approximately `4,000 bytes`.
- Because of this, the managed heap continues to grow indefinitely until `System.OutOfMemoryException` is hit eventually.
- Since the `memAlloc` continues to reference all the arrays, all of them remains reachable to the GC.

### Task 2 - Memory Management Best Practices
