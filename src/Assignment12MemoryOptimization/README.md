# Assignment 12 - Memory Optimization

## Task 1 - Memory Eater
Here the class `MemmoryEater` contains the method `Allocate()` which continuously allocates memory by adding integer arrays to a collection, leading to high memory usage.

### Inferences
- The diagnostic tool shows that the memory usage increases linearly with time as the list grows each `10ms`.
- Since `1000` integers are added into the list during each iteration, the list grows by approximately `4,000 bytes`.
- Because of this, the managed heap continues to grow indefinitely until `System.OutOfMemoryException` is hit eventually.
- Since the `memAlloc` continues to reference all the arrays, all of them remains reachable to the GC.

### Task 2 - Memory Management Best Practices
#### Technique 1: Fixed Size Memory Eater
- The original 'MemoryEater' leaks memory infinitely until an OutOfMemoryException crashes the app.
- This version enforces a ceiling at {maxTheoreticalCapMb:F2} MB.
- This optimized version uses a fixed-size list initialized with capacity, making it stable.
- Once capacity hits its max ceiling, memory stops growing because no additional arrays are added.

#### Technique 2: Bounded Memory Eater
- The original 'MemoryEater' leaks memory infinitely until an OutOfMemoryException crashes the app.
- This version enforces a ceiling at {maxTheoreticalCapMb:F2} MB.
- This optimized version uses a sliding window boundary, making it stable.
- Once capacity hits its max ceiling, memory stops growing.
- O(N) operation: List.RemoveAt(0) forces an internal memory copy array-shift operation.
- For 100,000 pointers, this forces the CPU to shift 99,999 memory references on every loop iteration.
- Continuously calling 'new int[]' pushes active objects into Gen 1/2.
- Even though the count is capped, the GC must work constantly to clean up arrays.

### Task 3 - Profiling Tools
#### Memory Eater
- The graph can be seen as increasing diagonally, as the memory increases with time.
- Since there is no upper limit, it continues to grow linearly until the `OutOfMemoryException` occurs.

#### Fixed Size Memory Eater
- The memory increases linearly until it reaches a fixed maximum limit and then it drops before it increases again.
- This resembles a sawtooth pattern.

#### Bounded Memory Eater
- The memory increases linearly until it reaches a fixed value. Then the graph becomes flat.
- This is because as soon as the maximum list count is reached the first element gets removed and the new element is added.
- This keeps the memory usage constant.