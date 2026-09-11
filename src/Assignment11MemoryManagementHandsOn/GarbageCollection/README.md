# Assignment 11 - Task 3

## Objective
To understand how garbage collection works in C#, and how it can impact application performance.

## Application Details
This console app (GarbageCollection) is designed to show how the .NET Garbage Collector (GC) manages memory under the hood.

### Implementation Details
- The program creates 1,000,000 instances of a `DummyReceipt` class. Because the code doesn't save these receipts into a list or an array, they are immediately abandoned on the heap. This allows us to track how memory accumulates.
- Phase 1:
	- Before the loop runs, `GC.GetTotalMemory` reported a small baseline usage (around 1855 KB).
	- 
- Phase 2:
	- After creating 1,000,000 objects, memory spikes up to around 6072 KB.
	- This is because the objects do not get cleaned up from memory immediately as they go out of scope.
	- Even though our loop immediately abandoned every single receipt it created, those bytes will still be on the heap until a garbage collection cycle is triggered to sweep them away.
- Phase 3:
	- Once the code runs `GC.Collect()`, the runtime halts execution threads, scans the heap for dead objects, and completely cleans out the 1,000,000 receipt objects.
	- Because of this sweep, memory drops back down to a baseline of roughly 572 KB.

## Garbage Collection
- Garbage collection (GC) is an automatic memory management feature in .NET that helps to manage the allocation and deallocation of memory for objects.
- The .NET runtime automatically tracks the objects that are no longer in use and free their memory.
- This eases the developers from the burden of manual memory management.

## Internal working of Garbage Collection
### Generational Hypothesis
- General Hypothesis is based on the observation that: the older an object is, the longer it is likely to live and majority of the objects are short lived after creation.

### Generational Garbage Collection:
.NET uses a generational garbage collection approach, which divides objects into three generations based on their lifespan:
- Generation 0: 
	- This generation contains short-lived objects, such as temporary variables.
	- It is collected frequently.
	- The CLR keeps the size budget of Gen 0 small so that it can fit into high speed CPU L3 cache.
	- As a result the GC cleans Gen 0 very fast. 
- Generation 1:
	- This generation contains objects that have survived one garbage collection cycle.
	- It is collected less frequently than Generation 0.
- Generation 2:
	- This generation contains long-lived objects, such as static data and objects that are referenced for a long time, all the large objects allocated via Large Object Heap (LOH).
	- It is collected the least frequently.

### Tracking Mechanism
When a collection cycle is triggered, the application threads are stopped (called as **Stop-The-World (STW)** pause) and Tracing Algorithm is initiated.

#### Reachability:
- A root object is an object that is directly accessible by the application, such as static fields, local variables, and parameters.
- An object is considered reachable if it can be accessed directly or indirectly through a chain of references from the root objects.

#### Steps involved in Tracing Mechanism (Mark, Sweep and Compact)
- Root Enumeration: First the GC gets a list of all active roots with the help of JIT compiler and CLR exceution engine.
- Graph Traversal: The GC traverses all the memory addresses stored in the roots, if an object is found, it is said to be alive. Then the GC travel down the reference chain based on the fields of the alive objects.
- Sweeping: Any object which was not reached during the traversal process is marked as dead, and it gets cleaned from the memory.
- Compaction:
	- After sweeping, the SOH can become highly fragmented This phase is requires heavy operation as this physically moves data in computer's RAM.
	- First GC calculates how far the surviving objects has to be moved to make to avoid fragmentation. A temporary relocation table is created in this process.
	- Next, since the address gets changed during relocation, the GC must update all the references in the applicaiton from old to new ones.
	- Then, GC performs bitwise memory copy operation (`memmove`). This physically shifts all bytes of live objects together.
	- Finally, the objects will be packed together on one and a continuos block of clean memory will be present on the other side.

## Advantages of Garbage Collection:
- Reduces manual memory management overhead for developers.
- Reduces dangling pointers (references to memory that has been freed).
- Reduces memory leaks by automatically reclaiming memory that is no longer in use
- Improves application stability and reliability by preventing memory corruption.

## Major Drawbacks of Garbage Collection:
- The object destruction using GC is indeterministic unlike languages like C, C++ where the destruction is deterministic.
- The most severe runtime drawback is because of the thread suspension during Mark, Sweeep, and Compact loops. During this process all the application exceution threads are stopped, effectively freezing the entire application.