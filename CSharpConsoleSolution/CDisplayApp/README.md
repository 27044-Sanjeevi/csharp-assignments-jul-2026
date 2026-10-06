# Assignment 14 -  Project, Solutions and Build Orders

## Project A - Greetings App
- Displays a simple greeting message to the user.

## Project B - Math App
`MathOperations.cs`
- Contains utility methods to perform basic mathematical operations.

## Project C - Display App
`PerfromCalculation()`
- It uses user input helper utilities from `DUtilityApp` and `BMathApp` to get and validate two integers and a math operator character.
- It then evaluates a C# switch expression, calling `BMathApp` core mathematical methods (Add, Subtract, Multiply, Divide).
- If it detects successful execution, it outputs a formatted string (F2 fixed-point double accuracy) to the console and saves the result to the log file.

`LogToFile()`
- It makes a timestamped log (`[yyyy-MM-dd HH:mm:ss]`), appends it to a flat file database `math_history_log.txt`.

## Project D - Utility App
`ConsoleHelpers.cs`
- Contains the helper utility methods for input and output operations in console.

## Project E - File Reader App
`DisplayHistory()`
- It cross-references the shared solution location using the matching static `LogFilePath`.
- If it verifies the file exists, it uses `File.ReadAllLines` get data line-by-line and print the timestamped records in console.
- If the file doesn't exist yet, it informs the user.

## Inferences and Learnings
### Project
- A project is a part of an application that compiles into a single output library or executable (such as a .dll or .exe).
- In C#, this is defined by a `.csproj` file, which uses an `XML` format.
- It contains,
	- Target Framework: (e.g., <TargetFramework>net8.0</TargetFramework>), which tells C# what runtime version to compile.
	- Project Dependencies: Contains links to other projects (like Project C referencing Project E) [Method 1, Method 3].
	- NuGet Packages: External third-party libraries downloaded from the internet.
- A project can compile on its own even if it isn’t inside a solution file, as long as its internal file references are valid.

### Solution
- A solution file does not contain code, classes, or assets.
- Instead, it is a plain text file (.sln) that links multiple projects together.
- It provides a single environment for developers inside Visual Studio to manage multiple projects simultaneously.
- It contains:
	- Pointers to Projects: Relative paths to the .csproj files on disk.
	- Build Configurations
	• Solution-Level Dependencies
- A solution cannot compile code by itself.
- It simply loops through its internal list of projects and passes them to the compiler in a predefined order.

### Project Reference
- A project reference is used to add dependency between two or more projects.
- When we add project reference between two projects an xml comment gets added in the project file, for example, When we add a Project Reference from Project E (EFileReaderApp) to Project C (DisplayApp), Project E’s .csproj file  is added with an XML comment [Method 1, Method 3]:
```xml
<ItemGroup>
  <ProjectReference Include="..\DisplayApp\DisplayApp.csproj" />
</ItemGroup> 
```

- When we add reference in this way, code separation can be achieved.
- By this way, it is also ensured that the upstream projects are built correctly before downstream projects arr built.

### Build Order
- Build Order is the sequence in which the MSBuild builds the projects present in a solution.
- MSBuild constructs a Direct Acyclic Graph (DAG) to determine the build order.
- When a project reference is added to a project (say Project E referencing Project C), it is inferred that Project C is a prerequisite for Project E.