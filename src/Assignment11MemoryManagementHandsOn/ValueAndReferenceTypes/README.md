# Assignment 11 - Task 1 & 2

## Objective
Understand the differences between value types and reference types in C#, and demonstrate their use in a C# application and to gain a practical understanding of how the stack and the heap work in C#.

## Task 1
- For this task, I created a structural copy target (TemperatureStruct) and a matching pointer layout (TemperatureClass)
- Both are initialized to 10°C in Main and passed into the Modify method, which attempts to overwrite both values to 20°C.
- The Struct (Value Type) Behavior:
	- Inside the Modify method, the struct is changeed to 20°C.
	- However, once the code returns to Main, it drops to the original 10°C.
	- This happens because value types are passed by value (copied).
	- The method only received an independent clone of the data on the local stack frame.
	- Modifying the clone leaves the original variable untouched.
- The Class (Reference Type) Behavior:
	- The class retains its updated value of 20°C back in Main.
	- This is because reference types are passed by reference.
	- The method received a duplicate of the memory address pointer pointing directly to the object's physical space on the heap.
	- Changing the Temperature property modified the shared object in place.

## Task 2
- For this task, two functions are added:
	- CreateIntegersOnHeap(): Allocates a contiguous array of 2,000,000 integers.
	- CreateIntegersOnStack(): Declares multiple local primitive integer variables to compute its sum.
- Arrays in C# are reference types, means only the pointer is stored on the stack, but the actual block of data is allocated entirely on the Managed Heap.
- An integer takes up 4 bytes of memory. Allocating an array of 2,000,000 items requires 8 Megabytes (2,000,000 × 4 bytes) of contiguous memory space.
- When the Process Memory graph in Visual Studio's Diagnostic Tools, an upward step in the timeline line graph the moment this function is called.
- Local variables inside a standard method execution thread are allocated entirely within that specific thread's Stack Frame.
- Declaring 12 integers consumes a 48 bytes of data in stack frame.

### Value Types:
- Value types are data types that hold their value directly in memory.
- When a value type variable is created, a specific amount of memory is allocated in the stack to store the value.
- When a value type is assigned to a new variable, first a new memory space is allocated to the new variable and a copy of the value is stored in it. So both variables behave independently.
- Examples of value types include:
	- int (4 bytes)
	- float (4 bytes)
	- double (8 bytes)
	- bool (1 byte)
	- char (2 bytes)
	- struct
	- enum
- Example:
```csharp
int a = 10; // a is a value type variable
int b = a; // b is assigned the value of a, a new memory space is allocated for b
b = 20;
a = 30;
```
Result: a holds 30 and b holds 20, as they are independent of each other.

### Reference Types:
- Reference types are data types that store a reference (or pointer) to the actual data in memory, rather than the data itself.
- The actual data is stored in the heap, and the reference type variable holds the address of that data in stack.
- When a reference type variable is assigned to a new variable, both variables point to the same memory location in the heap.
- Therefore, changes made through one variable will affect the other.
```csharp
Product x = new Product
{
	Name = "Laptop",
	Price = 1000
}; // x is a reference type variable

Product y = x; // y is assigned the reference of x, both point to the same memory location in heap
y.Price = 1200; // changing the price through y will also affect x
```
Result : Both x.Price and y.Price will be updated to 1200, as they point to the same object in memory.
