# Assignment 13 - Working with Collections and Generics

This is a C# console application demonstrating the usage of generic collections, generics, `IEnumerable<T>`, and read-only collection interfaces.

The application contains implementations for:
 
- `List<T>`
- `Stack<T>`
- `Queue<T>`
- `Dictionary<TKey, TValue>`
- `IEnumerable<T>`
- `IReadOnlyDictionary<TKey, TValue>`

## Implementation
1. `Program.cs`
- Acts as the application's entry point.
- Creates the collection objects.
- Creates the menu view.
- Displays the menu.
- Reads the user's choice.
- Executes the appropriate task.
- Handles top-level exceptions.
- Returns the user to the main menu after each task.

2. `MenuOptions.cs`: Defines the available menu options using an enum

3. `MenuView.cs`: Responsible for console menu presentation and input handling.

4. `ConsoleHelpers.cs`: Contains reusable console formatting functionality

5. `TestData.cs`: Contains the predefined data used by the tasks.

## Task Details
### Task 1: Working with Lists
#### Implementation
- Implemented `BookList<string>` using `List<string>` to manage book titles.
- The class supports adding books, removing a book, checking whether a book exists using "Contains", and displaying all books.
- Created a `List<string>` internally.
- Added five book titles.
- Removed ""Atomic Habits"".
- Checked whether ""Lord of the Rings"" exists.
- Displayed the updated collection.
 
`List` is suitable for ordered, dynamically sized collections and provides various operations such as "Add", "Remove", and "Contains". Generics also make the implementation reusable for different data types.

---

### Task 2: Using Stacks
#### Implementation
- Implemented `StringStack<char>` using `Stack<char>`. Characters from the string ""collection"" are pushed onto the stack and then popped to produce the reversed string.
- Created a `Stack<T>`.
- Pushed each character onto the stack.
- Popped characters one by one.
- Appended them to a "StringBuilder".
- Displayed the original and reversed strings.
- Learned the LIFO (Last In, First Out) principle. A stack naturally reverses a sequence because the last element pushed is the first element popped.
 
---
 
### Task 3: Working with Queues
 
#### Implementation
 
- Implemented `PersonQueue<string>` using `Queue<string>` to simulate people waiting in a line. The class supports "Enqueue", "Dequeue", and displaying the queue.
- Created a `Queue<string>`.
- Added five people using `Enqueue`.
- Removed the first person using `Dequeue`.
- Displayed the remaining people.
- Learned the FIFO (First In, First Out) principle. Queues are useful when elements must be processed in the same order in which they arrive.
 
---
 
### Task 4: Understanding Dictionaries 
- Implemented `StudentDictionary<string, int>` using `Dictionary<string, int>` to map student names to grades. The implementation supports adding, removing, and displaying key-value pairs.
- Created a `Dictionary<TKey, TValue>`.
- Added five student-grade pairs.
- Removed ""Bala"".
- Displayed the remaining students and grades.
- Applied the "notnull" constraint to dictionary keys.
- Learned that dictionaries are useful for representing key-value relationships and providing efficient key-based operations. Generics allow the same implementation to work with different key and value types.
 
---
 
### Task 5: Applying Generic Collections
#### Implementation
- The commit `32519cb` contains the version of Task 1 to 4 where generics is not used.
- For task 5, the collections are modified to use generic types:
	- `List<T>`
	- `Stack<T>`
	- `Queue<T>`
	- `Dictionary<TKey, TValue>`

- Instead of creating separate classes for specific data types, generic type parameters were used so the same collection logic can be reused.
- Generics provide type safety, reusability, and reduced code duplication while allowing the compiler to enforce the expected data types.

---

### Task 6: IEnumerable, Concrete Types and IReadOnlyDictionary
 
### 6.1 IEnumerable
 
#### Implementation
```csharp
int SumOfElements(IEnumerable<int> elements)
{
    return elements.Sum();
}
```
- The method is tested with a `List<int>`, `array`, and `Queue<int>`.
- Instead of creating separate methods for each collection type, "IEnumerable<int>" was used because all three collections can be enumerated.
- An abstraction such as "IEnumerable<T>" makes a method reusable across multiple concrete collection types. The method only requires the ability to enumerate the integers, not a specific collection implementation.

### 6.2 IReadOnlyDictionary
 
#### Implementation
 
- Implemented "GenerateDictionary()" to create a "Dictionary<string, int>" and return it as `IReadOnlyDictionary<string, int>`.
- A "PrintDictionary()" method accepts the same read-only interface and displays its contents. 
- Created a Dictionary<string, int>.
- Returned it through IReadOnlyDictionary<string, int>.
- Passed the result to PrintDictionary().
- Showed that assignment through the read-only interface is not allowed.
```
IReadOnlyDictionary<string, int> dictionary = GenerateDictionary();
 
// Not allowed:
// dictionary["Apple"] = 10;
```
- This shows the attempted modification as a compile-time error.
- Learned that "IReadOnlyDictionary<TKey,TValue>" exposes dictionary data for reading without exposing mutation operations through the interface. This helps protect data from modification by consumers of the API.