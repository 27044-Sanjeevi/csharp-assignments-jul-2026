# Assignment 11 - Task 4

## Objective
 Understand the purpose of the IDisposable interface, how the 'using' statement can be used to automatically dispose of objects, and how to release file resources properly.

## Implementation
- First `WriteIntoFile()` executes, which instantiates the custom wrapper, and locks sample.txt at the OS kernel level.
- The sample content is written to disk.
- As the code reaches the end of WriteIntoFile(), the variable falls out of scope, immediately triggering the compiler's hidden finally block, which calls writer.Dispose() returns to Main()
- So when File.ReadAllText(filePath) attempts to read the file, no exceptions occured as the resource got cleaned up safely using `Dispose()`.

### Managed Memory:
- When the CustomFileWriter class is instantiated, it belongs to the .NET Managed Heap.
- The Garbage Collector knows how to clean this.

### Unmanaged Resources:
- To actually write text to sample.txt, the .NET class must request a low-level file lock descriptor directly from the Operating System Kernel.
- The Garbage Collector does not manage this resource.
- If the file writer is abandoned without explicitly telling the OS to close it, that file will remain permanently locked by the system process.

### Using Keyword

Inside the WriteIntoFile method, the lines of code are present:
```csharp
using var writer = new CustomFileWriter(filePath);
writer.Write("This is the text written to the file.");
```

Internally, even when we don't manually have to call writer.Dispose(), the C# compiler intercepts this variable declaration and rewrites it into a try-finally block before compiling it into bytecode.

```csharp
CustomFileWriter writer = new CustomFileWriter(filePath);
try
{
    writer.Write("This is the text written to the file.");
}
finally
{
    if (writer != null)
    {
        ((IDisposable)writer).Dispose();
    }
}
```

Because of this hidden finally block, the execution of Dispose() is guaranteed.
Even if the writing logic encounters a runtime crash, the CPU will excute that finally block before leaving the method scope, ensuring the file handle is dropped cleanly.
