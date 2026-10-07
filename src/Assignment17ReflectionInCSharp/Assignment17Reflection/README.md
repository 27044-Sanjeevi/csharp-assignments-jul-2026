# Assignment 16 - Reflection in C#

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

### Constructors
- type.GetConstructors(): Returns all the constructors of the type



method.IsSpecialName
GetTypes()
field.FieldType.Name
