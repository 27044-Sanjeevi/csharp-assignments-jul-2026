# Assignment 16 - Advanced C# Concepts
 
The console application provides the implementation of the assignment 16 satisfying the requirements of tasks 1 to 7.
 
## Task 1 - Events and Delegates
### Implementation
- `Notifier.cs`: Contains the definition of the delegate `public delegate void Notify(string message);`and the event ` public event Notify OnAction;` with a publisher `SendNotification` which invokes the event.
- The event is then subscribed using `+=`. After subscribing, the publishing is done using `notifier.SendNotification("message");`
 
### Inferences and Learnings
- A Delegate is a type-safe function pointer.
- The `Notify` delegate defines a method signature (`void Method(string)`). This enforces compile-time safety, ensuring only compatible methods can be subscribed to OnAction.
- Declaring `OnAction` with the `event` keyword (rather than as a public delegate field) restricts external access.
- The `Notifier` class does not depend on the subscribers, it has no knowledge about it.
- This allow us to add add new notification handlers in future while following the open/close principle.
 
---

## Task 2 - `dynamic` vs `var` keyword
### Implementation
- `VarKeywordDemo.cs` is used to demonstrate the compile-time type safety by attempting four operations (initialization, reassignment, uninitialized declaration, null assignment).
    - The invalid code is commented out and replaced with ConsoleHelpers.DisplayFailure() messages containing the Roslyn error codes (CS0029, CS0818, CS0815).
- `DynamicKeywordDemo` is used to demonstrate the runtime flexibility by executing the same four operations as in VarKeywordDemo.cs`.
    - Reassignment across types (string to int) and uninitialized declarations works with no error at runtime via the DLR (Dynamic Language Runtime).
    - Invalid method calls will compile fine but can lead to `RuntimeBinderException`during runtime.
 
### Inferences and Learnings
**`var` keyword**
- `var` keyword was introduces alongside of LINQ to support anonymous types.
- It doesn't provide the dynamic typing property.
- Once a type is fixed to the `var` variable, it cannot be changed later.
- A `var` variable cannot be declared without initialization. This is because the compiler infer the type of the variable from the RHS(Right-hand-side) type inference.
- Multiple `var` variables cannot be declared in the same statement.
- The `var` keyword can only be used for local variables. It cannot be used for class-level fields,properties or method parameters.
- `null` cannot be assigned to a `var` variable.

**`dynamic` keyword**
- A `dynamic` object can be implicitly converted to any static type without an explicit cast.
- If a dynamic object is created initially with some type, later the dynamic objects can be assigned to other types too.
- This allows to handle situations where an object's type is unknown until the execution.
- This is possible because `dynamic` defers all member lookups, conversions, and type checks to the runtime.
- Under the hood, DLR (Dynamic Runtime Language) communicates with the CLR (Common Language Runtime), handling the binding.
- If any unknown object is called via a dynamic object, the calls will compile fine but can lead to `RuntimeBinderException`during runtime.
- Unlike var, which can only be used for local variables inside methods, dynamic is a valid type specifier anywhere. It can be used for class fields, property definitions, method parameters, and method return types.
- The usage of dynamic can cause performance overhead because of heavy internal mechanics.

---

## Task 3 - Anonymous Methods
### Implementation
- `SortArray.cs`: Demonstrates the use of anonymous methods for sorting an integer array.
    - `Comparison<int> ascendingOrderComparator` takes two numbers (`x` and `y`)  as input and output is,
        - `-1`: if `x` < `y`
        - `0`: if `x` == `y`
        - `1`: if `x` > `y`

### Inferences and Learnings
- Anonymous methods (or) Inline delegates allows to  declare inline code blocks without giving them a formal name.
- The main reason for introducing delegates is that it reduces API pollution by keeping the code blocks inside the where it belongs instead of creating a new method for it.

**Syntax:**
```
TargetDelegateType instance = delegate(parameters)
{ 
    execution_body 
};
```
- The semicolon must the added after the closing brace.
- Instead of manually declaring custom delegates, anonymous methods are commonly mapped to standard .NET generic framework signatures:
  - Action<...>: Takes 0 to 16 input parameters and returns void. Used for operational side-effects.
  - Func<..., TResult>: Takes 0 to 16 input parameters and returns TResult. The final generic argument determines the return type contract.
  - Predicate<T>: Takes exactly 1 input parameter and returns a bool. Used for evaluation and collection filtering.
- Unlike lambda expressions, anonymous methods allow us to omit the parameter parentheses .

**Example:**
```
button.Click += delegate
{
    // statements
};
```
- Here the parentheses are omitted after the delegate keyword.
- Because the Roslyn compiler uses the left-hand assignment type to infer parameter and return signatures, we cannot assign an anonymous method to an implicitly typed variable (var).
- In the task 3, instantiating a built-in .NET delegate named Comparison<int> acts as a strict contract requiring an underlying function that accepts two integers (x and y) and returns an int.

---

## Task 4 - Lambda Expressions and Statements
### Implementation
- `LambdaExpressionsDemo`: Defines a list of integer and performs LINQ operations (where and select).

### Inferences and Learnings
- LINQ extension method (like .Where, .Select, .Any, or .OrderBy) is built on top of C# generics and expects a specific delegate contract as an argument.
- The `.Where()` method loops through a collection and filters out elements. It requires a function that takes an item and returns true (keep it) or false (discard it).
- The `.Select()` method transforms (projects) each item in a collection into a new structure or type.
- LINQ and lambdas is Deferred Execution (Lazy Evaluation).
- When we chain lambda expressions inside a LINQ query, no data is processed immediately. Instead, the LINQ pipeline merely builds an internal execution plan.
- Lambda expressions use the lambda declaration operator `=>`.
- The compiler automatically returns the evaluated result of that expression without an explicit return keyword.

Example
```
Func<int, int> square = x => x * x; // Implicit return
```

- Statement Lambdas contains a code block enclosed in curly braces { }. Its like a standard method body and requires an explicit return statement.

Example
```
Func<int, int> squareStatement = x =>
{
    int result = x * x;
    return result; // Explicit return mandatory.
};
```

---

## Task 5 - Advanced Use of Delegates for Sorting
### Implementation
- `Product.cs`: Contains the definition for product.
- `ProductCategory.cs`: Enum containing the product categories.
- `ProductRepository.cs`: Repository containing the list of products.
- `ProductSorter.cs`: Contains the logic for sorting based on the `SortDelegate` passed as a parameter.
- `SortStrategy.cs`: Defines the methods with same signature as the `SortDelegate` which contains various sorting strategies.

### Inferences and Learnings
- Instead of hardcoding conditional logic inside the sorting loop, it can dynamically injected different sorting algorithms at runtime using delegates.
- This satisfies the Open/Closed Principle where the code is open for extension (adding new sort rules) but closed for modification.
- The injected strategy can be passed as a delegate to the `List<T>.Sort()`


## Task 6 - Implementing and Manipulating Records
### Implementation
- `Book.cs`: Contains the definition for the Book record.
- `BookRecordDemo.cs`: Demonstrates the operations as per the task instructions.

### Inferences and Learnings
- Unlike standard C# classes (which compare references/memory locations), **records override `Equals()` and the `==`/`!=` operators by default**. Even though `book1` and `book1Copy` are separate objects on the heap, `book1 == book1Copy` evaluates to `true` because their properties match.
- The records use `init` accessors under the hood. Properties can only be set during object initialization (via constructor or object initializer).
- Attempting to overwrite a property later throws compiler error **CS8852**, guaranteeing thread-safe, immutable data structures.
- Because Book is an immutable record, we cannot change its properties directly (e.g., book2.Author = "Harry"; causes a compilation error).
- Instead of modifying the original object, the `with` expression creates a new object copy in memory.
- `book2` remains completely untouched and retains its original values.
- `modifiedBook2` is a distinct object containing the copied data, except for the Author property, which is overwritten with "Harry".

## Task 7 - Advanced Pattern Matching
### Implementation
- `Shape.cs`: Represent a shape with `Name`, `Color` and `Description` properties.
- `Circle.cs`, `Rectangle.cs`, `Triangle.cs`: Inherits the base class `Shape` with their own properties like `Radius`, `Length`, `Width` and sides length.
- `ShapesDemo.cs`: Demonstrates the operations as per the task instructions.

### Implementation and Learnings
- Pattern matching is a technique in C# that tests a value against a specific type condition.
- Before this, the C# switch statement could only evaluate primitive value types (like int, char, or strings) against constant values.
- Modern C# allows switch statements to evaluate complex types directly using Type Patterns.

```
case Circle circle:
    Console.WriteLine($"Radius = {circle.Radius:F2}");
    break;
```

- When this case executes, the runtime performs two steps in a single, atomic operation:
    - Type Testing: It checks if the underlying runtime instance of shape is compatible with the `Circle` type (equivalent to an is type check).
    - Conditional Variable Declaration & Assignment: If the type check succeeds, it casts the object to a `Circle` and assigns it to a newly declared local variable named `circle`.
