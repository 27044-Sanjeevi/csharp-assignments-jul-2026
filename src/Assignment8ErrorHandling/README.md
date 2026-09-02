## Error Handling App
A C# console app that shows how to handle code errors and track crashes.
## Tasks Overview
## Task 1: Division & Cleanup

* Catches math errors without crashing the app.
* Uses standard try / catch / finally blocks.
* Divides two numbers safely.
* Triggers a DivideByZeroException if you divide by zero, then runs cleanup code.

## Task 2: Exception Wrapping

* Catches hidden bugs and saves the original error details.
* Uses Inner Exceptions to link the root cause to a new error message.

## Task 3: Custom Errors

* Separates custom business rules from standard system errors.
* Uses a custom InvalidUserInputException for bad user inputs.

## Task 4 & 5: Crash Tracking & Analysis

* Catches intentional crashes to read the program's path history.
* Task 4 purposefully throws an unhandled error to trigger a crash.
* Task 5 catches that crash, reads the exact line numbers, and maps out the execution path.

