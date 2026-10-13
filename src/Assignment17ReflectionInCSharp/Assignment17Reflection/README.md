# Assignment 17 - Reflection in C#

## Implementation Details
### Task 1
`AssemblyMetadataInspector.cs`: Implements a runtime parsing loop that accepts an external compiled .dll or .exe assembly file path, extracts its internal type system structure, and exposes comprehensive structural metadata signatures.
1. The File Path is Passed: A path to a compiled assembly file is provided.
2. Types are Scanned: The file is loaded and the exact number of unique types (like classes or enums) contained inside it is displayed.
3. Every type is looped through and fields, methods and events are shown.

### Task 2
`DynamicObjectInspector.cs`: This class takes an unknown object instance, maps out all its public properties, and provides an interactive prompt to change their values on the fly.
1. Object Analysis: The app reads the live object instance, prints out its full class name, and lists every public property alongside its type and current value.
2. Targeted Selection: The user is prompted to type the exact name of the property wanted to be changed.
3. Value Updating: The user types in the new value. The app handles the type parsing automatically, updates the live object, and shows a success confirmation.

### Task 3
`DynamicMethodInvoker.cs`: This class takes an object instance, searches for a specific method by its string name, passes in any required arguments, and executes it runtime.
1. A target method name and an optional array of parameters are supplied to the invoker.
2. The object's metadata is scanned by the reflection engine to find a matching public instance method.
3. The method is dynamically executed, and the final output or return value is printed to the console.


### Task 4
`DynamicTypeBuilder.cs`: This class uses low-level intermediate language (IL) injection to build an ImageMetadata type entirely during runtime execution, bypassing standard source code compilation.
1. A temporary assembly named DynamicContractsEngine is created straight into memory.
2. A class named Contracts.DynamicImageProfile is added to the module container.
3. A property named Format and a custom console summary method called PrintSummary() are woven into the dynamic type using raw IL instructions.
4. An instance of the dynamic class is initialized, its property value is changed, and the custom output method is executed to print out the final summary results.

### Task 5
1. A target directory named `Plugins` is searched for any compiled external .dll binary libraries.
2. Discovered assemblies are loaded into memory, and their internal classes are checked to see if they implement the required interface signature.
3. Safe plugin classes are initialized in memory using their default constructor definitions.
4. The plugin's name and description are printed to the console, and its core execution method is run using a test image path asset.

### Task 6
1. A target contract interface called `ISampleUserInterface` is provided to the dynamic generator.
2. A temporary class called `ISampleUserInterface_Mock` is constructed, and default fallback return values are injected into all its method blueprints.
3. The newly compiled, memory-resident type signature is turned into a concrete object instance using `Activator.CreateInstance()`.
4. Every method is executed sequentially to verify that numbers default to 0, strings return null, bool return False, and void actions exit without throwing crashes.

### Task 7
1. A mock `UserData` object containing Name, Email, and Age fields is passed to both serialization engines to ensure their formatted text output matches.
2. The structural layout of the target data class is analyzed, and a specialized execution sequence is built out via `ILGenerator`.
3. A high-volume loop executes both serializing approaches across one million iterations while measuring elapsed time using a `Stopwatch`.
4. The final run timings are compared, showing a execution speed increase for the Reflection.Emit engine due to its allocation-free design.

## LEARNINGS
### Type Object
- The reflection concept mainly rely on the `Type` Object.
- It provides metadata about class, struct, enum, interface etc directly from the assemby metadata table.
- `typeof(T)`
	- Gets resolved at compile time
	- Evaluates the type token directly.
	- Zero/negligible performance cost
	- Cannot fail at runtime (null-safe)
- `obj.GetType()`
	- Gets resolved at Run-time
	- Evaluates the concrete run time instance type.
	- More Performance cost than `typeof(T)`, since this looks up to the methods internal table pointer
	- Throws a `NullReferenceException` if the object instance is null.

### Loading Assemblies
- `Assembly.Load(AssemblyName)`
	- It requires a strongly typed `AssemblyName` object as an input.
		- AssemblyName: Encapsulates the structural identity of a compiler binary file.
		- It consists of four components: `Name`, `Version`, `Culture`, `PublicKeyToken`
		- Properties of AssemblyName: `.Name`, `.Version`, `.CultureInfo`, `.Flags`
	- If the assembly identifier already exists in the default `AssemblyLoadContext` then it uses the cached version, otherwise it searches the base directory.
- `Assembly.LoadFrom(string AssemblyPath)`
	- It allows to point to an external path on hard disk.
	- Useful for plugin frameworks.
	- If identity already exists in default AssemblyLoadContext, it discards the file path, else registers the file path in the "LoadFrom context".
	- It automatically locates and loads the reference projects.
	- OS locks the given assembly file while application is running.
- `Assembly.LoadFile(string AssemblyPath)`
	- • Unlike LoadFrom, it does not check if the assembly identity is already loaded.
	- If we call Assembly.LoadFile() on the exact same assembly three times, it will load it into memory three separate times as three Assembly instances.
	- Do not resolve the dependencies.
	- This too locks file on disk.

### Member Discovery APIs and Member Tokens
- Parameterless overload (e.g., `type.GetMethods()`)
	- It only returns public, instance, and static members belonging to that type or inherited from base classes.
	- Hides non-public components.
- Flag-based overload (e.g., `type.GetMethods(BindingFlags flags)`)
	- We can have control over the results from metadata tables.
- Common Methods:
	- `GetFields()`
	- `GetProperties()`
	- `GetMethods()`
	- `GetEvents()`
- **`MemberInfo`**
	- Parent Wrapper.
	- Provides attributes common to all members:
		- Name (string): The exact literal identifier of the member in code (e.g., "CalculateTotal").
		- DeclaringType (Type): The class or struct where this member was explicitly written and defined.
		- ReflectedType (Type): The class object which can differ from DeclaringType if the member was inherited from a base class.
		- MemberType (MemberTypes): An enum flag indicating if the member is a Field, Method, Property, Event, or Constructor.
- **`FieldInfo`**
	- Maps directly to a fixed location in memory.
	- It knows the data type of the field and allows direct byte reading or writing.
- **`PropertyInfo`**
	- It does not point to a location in memory.
	- Instead, it points to metadata containing pointers to up to two execution blocks: a getter method and a setter method.
- **`MethodBase / MethodInfo`**
	- Provides info about the method signature, parameters, return values, and generic constraints.

### Binding Flags
- By default, the public APIs in reflection expose only public, instance, and static members directly declared or inherited.
- Binding flags can be used to access even the non-public members.
- Whenever a custom binding flag is used a combination of Visibility and Scope flags must be used, otherwsie a null or empty array is returned.
	- Valid Search =  (Public or NonPublic) and (Static or Instance)
	- Visibility = Public or NonPublic
	- Scope = Instance or Static
- Available binding flags:
	- `BindingFlags.Public`
	- `BindingFlags.NonPublic`
	- `BindingFlags.Static`
	- `BindingFlags.Instance`
	- `BindingFlags.DecalredOnly`
	- `BindingFlags.FlattenHierarchy`
	- `BindingFlags.IgnroeCase`

### Dynamic Invocation API (`MethodInfo.Invoke()`)
- The `MethodInfo.Invoke()` API allows to execute any method compiled in the application.
- Signature of execution engine: `public object? Invoke(object? obj, object?[]? parameters)`
- First Parameter (object? obj):
	- For Instance Methods:
		- This must be a live reference to an object in the managed heap.
	- For Static Methods:
		- This parameter should be explicitly passed with null.
- Second Parameter (object?[]? parameters):
	- A nullable array containing the arguments passed to the method to be invoked.
- Return Value:
	- If the invoked method returns a value, it gets wrapped as an object and returned.
	- Returns null if the invoked method returns void.
- If an exception is thrown by the method invoked using .Invoke(), then any exceptions which occurs inside the invoked method gets bundled up and thrown as `TargetInvocationException`.
- To see the actual exception thrown by the invoked method, inner exceptions has to be seen.