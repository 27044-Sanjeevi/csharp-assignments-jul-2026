# Exploration Questions & Answers

## 1. Explain what the .NET platform is and its primary purpose. 
.NET platform is a free, cross-platform, open-source developer platform developed by Microsoft for building many different types of applications.

### Different versions of .NET:
- .NET Framework: The original version of .NET, primarily for Windows applications.
- .NET Core: A cross-platform version of .NET for building applications that can run on Windows, macOS, and Linux.
- .NET 5 and later: A unified platform that allows developers to build applications for multiple platforms.
- The latest stable version is .NET 10 (LTS) and latest preview version is .NET 11 (Preview).

### Applications that can be built using .NET include:
- Web applications
- Desktop applications
- Console applications
- Mobile applications
- Cloud applications
- Gaming applications
- IoT applications

### Primary purpose of .NET:
- Cross-platform development: .NET allows developers to build applications which can support multiple platforms (Windows, Mac, Linux, etc.)
- Multiple Language Support: .NET supports building application using various languages including C#, F# and Visual Basic.
- Common Libraries and APIs: .NET provides a variety of libraries and APIs that developers can use to build applications quickly which supports multiple languages.
- Common Language Runtime (CLR): .NET provides a runtime environment that manages the execution of code and provides services such as memory management, security, and exception handling.

---

## 2. What are the key components of the .NET platform? 

### Key Components
- Common Language Runtime (CLR): The CLR is the execution engine for .NET applications. It provides the following services:
	- Execution of code
	- JIT Compilation
	- Garbage Collection
	- Exception Handling
	- Thread Management
	- Memory Management
	- Type Safety
	- Assembly Loading
- Base Class Library (BCL): The BCL is a collection of reusable classes, interfaces, and value types which provides various functionality for .NET applications:
	- List
	- Dictionary
	- String
	- DateTime
	- File
	- Thread
	- Console
	- LINQ
	- JSON
	- Reflection
	- HttpClient etc.
- Application Frameworks: .NET provides various application frameworks for building different types of applications:
	- ASP.NET Core: For building web applications and APIs.
	- Windows Forms: For building traditional desktop applications.
	- WPF (Windows Presentation Foundation): For building modern desktop applications.
	- Xamarin/MAUI: For building mobile applications.
	- Blazor: For building web UIs using C# instead of JavaScript.
- Roslyn Compiler: The Roslyn compiler is the open-source C# and Visual Basic compiler. It provides code analysis APIs and enables developers to build custom code analysis and refactoring tools.
- SDK and Tools: .NET provides a Software Development Kit (SDK) and various tools for building, testing, and deploying applications:
	- .NET CLI (Command Line Interface)
	- Visual Studio
	- Visual Studio Code
	- NuGet package manager

---

## 3. Differentiate between the Common Language Runtime (CLR) and the Common Type System (CTS) in .NET. 

### Common Language Runtime (CLR):
- The CLR is the execution engine for .NET applications. The functionalities of CLR includes:
	- Loads the compiled code.
	- Converts IL (Intermediate Language) code to native machine code using Just-In-Time (JIT) compilation.
	- Manages memory allocation and deallocation through garbage collection.
	- Enforces type safety and security.
- Internal components of CLR:
	- JIT Compiler
	- Garbage Collector
	- Type Loader
	- Security Engine
	- Exception Handling
- CLR is responsible for executing the code and managing the runtime environment, while CTS defines the rules for data types and their interactions.

### Common Type System (CTS):
- The CTS defines a set of rules and standards for data types in .NET. 
- It ensures that objects written in different .NET languages can interact with each other.
- For example, a library written in F# can be used in a C# application because both languages follows the same type system defined by CTS.
- CTS defines the following:
	- Value types (e.g., int, float, bool)
	- Reference types (e.g., class, interface, delegate)
	- Type safety rules
	- Type conversion rules
	- Type inheritance rules
- Example Conversion:
	- The `int` type in C# is equivalent to the `System.Int32` type in .NET, and both follow the same rules defined by CTS.
	- The `string` type in C# is equivalent to the `System.String` type in .NET, and both follow the same rules defined by CTS.

---

## 4. What is the role of the Global Assembly Cache (GAC) in .NET? 
- Global Assembly Cache (GAC) was introduced in .NET Framework to provide a central location for shared assemblies.
- It is a machine-wide code cache that stores assemblies which has to be shared by several applications on the computer. 
- It allows multiple applications to share libraries and components, ensuring that the correct version of an assembly is used by all applications.
- Instead of each application having its own copy of an assembly, GAC allows for a single shared version of the assembly to be used by multiple applications
- This reduces duplication and saves disk space.
- Assemblies in the GAC are identified by their strong name, which includes the assembly's name, version, culture, and public key token.
- This was discontinued in .NET Core and later versions, as the GAC is not used in these versions.
- Instead, assemblies are typically referenced directly from the application's project (project references) or from NuGet packages.

---

## 5. Explain the difference between value types and reference types in C#.
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

---

## 6. Describe the concept of garbage collection on .NET and its advantages
- Garbage collection (GC) is an automatic memory management feature in .NET that helps to manage the allocation and deallocation of memory for objects.
- The .NET runtime automatically tracks the objects that are no longer in use and free their memory.
- This eases the developers from the burden of manual memory management.

### Reachability:
- A root object is an object that is directly accessible by the application, such as static fields, local variables, and parameters.
- An object is considered reachable if it can be accessed directly or indirectly through a chain of references from the root objects.

### Generational Garbage Collection:
.NET uses a generational garbage collection approach, which divides objects into three generations based on their lifespan:
- Generation 0: This generation contains short-lived objects, such as temporary variables. It is collected frequently.
- Generation 1: This generation contains objects that have survived one garbage collection cycle. It is collected less frequently than Generation 0.
- Generation 2: This generation contains long-lived objects, such as static data and objects that are referenced for a long time. It is collected the least frequently.

### Advantages of Garbage Collection:
- Reduces manual memory management overhead for developers.
- Reduces dangling pointers (references to memory that has been freed).
- Reduces memory leaks by automatically reclaiming memory that is no longer in use
- Improves application stability and reliability by preventing memory corruption.

---

## 7. What is the purpose of the Globalization and Localization features in .NET? 
### Globalization:
- Globalization means designing and developing applications that can be adapted to different languages, cultures, and regions without requiring changes to the source code.
- It involves,
	- Date formats
	- Currency formats
	- Number formats
	- Languages
	- Sorting
	- Calendars
	- Time zones etc.
- Advantages of Globalization:
	- The reach of applications can be extended to a global audience.
	- Enhances user experience by providing culturally relevant content.
	- Increases competitiveness of applications among global markets.
- Disadvantages of Globalization:
	- Requires additional testing and maintenance efforts.
### Localization:
- Localization is the process of adapting an application to a specific language, culture, or region.
- It involves translating the user interface, messages, and other content into the target language and adjusting formats to match local conventions.
- Advantages of Localization:
	- Provides a personalized experience for users in different regions.
	- Enhances user engagement and user experience.
- Disadvantages of Localization:
	- Requires additional resources for translation and adaptation.

---

## 8. Explain the role of the Common Intermediate Language (CIL) and Just-In-Time (JIT) compilation in the .NET framework. 
### Common Intermediate Language (CIL):
- CIL is a low-level programming language that is used as an intermediate representation of .NET code.
- It is CPU-independent and is generated by the .NET compilers from high-level languages like C#, F#, and Visual Basic.
- All the .NET languages compile down to CIL, which is then executed by the Common Language Runtime (CLR).
- This allows the .NET framework to support multiple programming languages and enables interoperability between them.

### Just-In-Time (JIT) Compilation:
- JIT compilation is the process of converting CIL code into native machine code at runtime, just before execution.
- The JIT compiler is part of the CLR and is responsible for translating the CIL code into optimized machine code that can be executed by the CPU.
- JIT is needed because CIL is not directly executable by the CPU and the same CIL code can be executed on different hardware architectures.
- Advantages of JIT Compilation:
	- CIL code can be executed on any platform that has a compatible CLR implementation.
	- The JIT compiler can optimize the generated machine code based on the specific hardware and runtime conditions.

### Flow of Execution:
```
C# code -> C# compiler -> CIL code (Intermediate Language) -> JIT Compilation -> Native Machine Code -> Execution by CPU
```