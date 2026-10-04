# Assignment 15 - Working with Files and Streams in C# 

Console application which showcases work with different types of streams including FileStream, MemoryStream, and BufferedStream and performed operations like creating, reading and writing files.

## Implementations and Inferences
### Task 1
1. `FileGenrator.cs`: Creates a weather data file of given size synchronously using stream writer.
2. `FileProcessor.cs`: Processes the weather data file synchronously using fileStream, buffered stream and compares both of the perfomances.
3. `WeatherStatistics.txt`: Holds the final processed data report.
#### Inferences
- At small scales (4096 and 8192 bytes), `BufferedStream` outperformed `FileStream` by around **945.80 ms to 1473.18 ms** gap.
- Even though the local `buffer` array is small, `BufferedStream` initializes with its own fast internal **64KB RAM cache layer**.
- It pre-fetches large data arrays from the storage hardware in single frames, reducing the number of slow physical disk calls.
- At 32KB, the difference drops to a mere **96.34 ms**. At 65KB, both methods hit a performance wall, jumping to roughly **4500 ms**.
- As the buffer sizes are increased, the gap becomes smaller.
- At 32KB, the difference drops to a **96.34 ms**.
- At 65KB, both methods almost exceute in same time.
- The spike around 65KB represents the execution threshold where the operating system's internal hardware file cache gets saturated or flushed by background processes, forcing the app to drop from pure RAM speed down to physical storage drive limits.

### Task 2
1. `FileGeneratorAsync.cs`: Creates weather data files of given size asynchronously using stream writer.
2. `FileProcessorAsync.cs`: Processes the weather data file asynchronously using fileStream, buffered stream and compares both of the perfomances.
3. `WeatherFileStats.cs`: Holds the weather statistics properties.

#### Inferences
- Processing three separate files synchronously typically take more time than the asynchronous method.
- This is because the use of `await reader.ReadLineAsync()`, the main thread was released to handle other tasks while the hardware disk controller fetched data frames in the background.
- While Thread 1 is paused waiting for a data block from `Weather1.txt`, the CPU switches to process a line that just arrived in the memory buffer for `Weather2.txt`.
- But in some tries, the async was slower than the synchronous way. This is because running parallel async operations on the *same physical drive* will not increase raw data transfer speeds because the hardware storage channel is the bottleneck.


### Task 3
`StreamAndMemoryWriter.cs`: Run diagnosis on both the given unoptimized and fixed optimized code.

#### Inferences
1. **Issue:** `MemoryStream` was used as redundant logic alongside an explicit `.ToArray()` call. This duplicated the entire data buffer on the managed heap, creating high overhead and potential for **Large Object Heap (LOH)** fragmentation.
**Fix:** Bypassed the temporary memory allocation completely. Wrote data directly to the `FileStream` using a tuned `StreamWriter` configured with an optimized internal buffer.

2. **Issue:** `Encoding.ASCII` was hardcoded, which stripped out or corrupted UTF-8 and non-ASCII localized symbols.
**Fix:** Adopted `Encoding.UTF8` universally across all file reader and writer modules to preserve cross-platform data integrity.

3. **Issue:** Casting binary data directly to text using `(char)buffer[i]` cut multi-byte UTF-8 sequences in half, rendering international characters unreadable.
**Fix:** Replaced manual byte casting with a robust `StreamReader` engine to parse character boundaries accurately.

4. **Issue:** Executing byte-by-byte `Console.Write` logs inside the heavy inner loop forced the CPU to pause constantly, freezing your thread processing speed.
**Fix:** Shifted to a chunked design strategy, processing text strings in **4KB bulk blocks** instead of individual characters to optimize terminal performance.

### Task 4
1. `IndependentUserLogger.cs`: Made a logging system such that instead of logging all errors to a single file, each user’s error is logged to a unique file. This helps to prevent file access contention and make the logs easier to manage.
2. `LoadTester.cs`: Implemented a load test to simulate multiple users logging errors at the same time. The performance of the initial logging system and the improved system is compared. 
3. `ThreadSafeLogger.cs`: Implemented thread-safe code for logging.

#### Inferences
#### 1. Thread-Safe Single Shared File
- Completed **1,000 operations in 2,639.2 ms**, with a throughput limit of **379 logs/s**.
- This strategy maintains structural stability but scales poorly under high load due to **Lock Contention (Thread Serialization)**.
- The `Parallel.For` setup runs with a maximum allocation of 20 parallel processing cores trying to trigger a file write simultaneously. 
- Because the shared logging logic uses a block-level synchronization object (`lock (FileLock)`), **only one virtual user thread can interact with the file stream handle at any single time.**
- The other 19 execution threads are thrown into an idle sleep queue managed by the operating system scheduler.
- This creates a overhead as threads constantly switch contexts while waiting to acquire the lock.
- This waiting slows down execution speed and limits throughput to a lower logs/s range.

#### 2. Independent Isolated Files
- Completed **1,000 operations in 1,430.4 ms**, with **84.4% throughput efficiency increased**.
- This strategy achieves high scalability by replacing synchronization locks with **Data Partitioning**.
- By isolating file output paths by user ID (`user_{userId}.log`), we can completely eliminate cross-thread resource dependencies. 
- Because no `lock` statement is used, all 20 threads execute simultaneously at full speed on different CPU cores.
- The operating system handles these concurrent writes through parallel channels without forcing any threads to wait in a queue.
- This allows the application to achieve more logging speeds.