# Assignment 18 - Asynchronous Programming

## Task 1 : Downloading Content from internet using HttpClient()
- Calling `GetStringAsync` with the await keyword ensures that the executing thread is immediately released back to the system while waiting for Google's servers to respond, satisfying the task's requirement to remain responsive.
- Under the hood, the `async` keyword tells the C# compiler to transform this method into a state machine to handle the suspension and resumption of code.
- The method returns `Task` instead of `async void`.
- Returning `Task` allows errors to propagate properly to the `Main` method and enables the caller to await its completion safely.

## Task 2: Task Parallel Library (TPL) operations using Parallel.For
- The `Parallel.For` method is used to process concurrently, using multiple threads to improve performance.
- Parallel.For executes a traditional C# for loop, but instead of running iterations sequentially (one after another on a single thread), it splits the iterations into chunks and runs them across multiple processor cores simultaneously.
- Running parallel multiple threads is a performance cost. If the loop does a tiny calculation over a small dataset, the parallel loop may run slower than a traditional sequential loop.
- There is no guarantee of execution order. If a loop is run from 0 to 100, iteration 55 might finish before iteration 2 even starts.

## Task 3: Multithreading using Thread class
- The `Thread` class is used to create and manage threads in C#.
- Utilizing `.Start()` method to non-blockingly start the worker threads and implementing `.Join()` on the primary calling thread ensures the benchmark stopwatch (stopwatch2) accurately captures the total lifespan of all background actions before finalizing.
- Multi-threading allows a the computational process to spawn multiple distinct paths of execution (threads) that run concurrently, instead of the sequential path, reducing time.
- By offloading long-running, heavy calculations or I/O operations to background worker threads, the primary UI thread (or main thread) remains free to handle user interactions without freezing.

## Task 4: Cascading Asynchronous operations using async and await
- Here an internal database of 9 predefined string parameters (products, users, etc.) stored in an instance array `this._parameters`. It acts as a routing lookup table for building dynamic URLs.
- `MethodA` simulates heavy mathematical processing (CPU-bound work). It is safely wrapped inside a `Task.Run()` delegate within `MethodB`. This forces the runtime to offload execution away from the primary caller to a background thread pool worker thread.
- Inside `MethodA`, `Thread.Sleep(1)` is intentionally used simulating data analysis workload.
- The network call in `MethodB` uses `await this._httpClient.GetFromJsonAsync(...)`. This performs asynchronous, non-blocking I/O. It returns the active thread pool thread back to the runtime while waiting for bytes to arrive from the remote endpoint.

## Task 5: Deadlock scenario
- A deadlock occurs when two or more threads are blocked forever, each waiting for the other to release a resource. In this case, the deadlock is caused by the combination of synchronous blocking and asynchronous programming.
- Calling `.Result` blocks the calling thread synchronously until the task completes.
- In applications with a `SynchronizationContext` (like WPF, WinForms, or older ASP.NET), that context only allows one thread to run at a time.
- The background task tries to jump back onto the original thread context to finish its continuation, but it cannot because that thread is permanently blocked waiting for` .Result`, creating a deadlock loop.
- To avoid this deadlock, you can use `await` instead of `.Result`, which allows the calling thread to be released while waiting for the task to complete.
- This way, the continuation can run on the original context without blocking it.

## Task 6: Configure Await Demo
- The `ConfigureAwait(false)` method is used to indicate that the continuation after an `await` does not need to be run on the original synchronization context (like the UI thread).
- This is useful in library code or background processing where we don't need to update the UI after an asynchronous operation, allowing for better performance and avoiding potential deadlocks.
- When `MethodA` reaches the await statement, it releases the invoking thread.
- After the 3-second delay completes, the continuation of `MethodA` will execute on a different ThreadPool worker thread.
- In a console application, there is no default SynchronizationContext, so await behaves similarly to ConfigureAwait(false).
- But, this is essential in case of desktop (WPF/WinForms) or web applications to prevent UI freezing and deadlocks.

## Task 7: Exception Handling in Asynchronous Programming (async Task vs async void)
- Using `async Task` allows exceptions to be caught and handled by the caller, while `async void` does not provide a way to catch exceptions, leading to unhandled exceptions that can crash the application.
- In `async Task`, exceptions are propagated back to the caller, allowing for proper error handling and logging.
- In `async void`, exceptions are raised directly on the calling thread, which can lead to unhandled exceptions and application crashes, especially in UI applications where the main thread is responsible for handling user interactions.
- When an exception is thrown inside `VoidMethod()`, it cannot be caught by a standard try-catch block surrounding the method call. Because it returns void, there is no Task object to capture the failure. The exception bubbles directly up to the active SynchronizationContext, which will instantly crash the entire application.

