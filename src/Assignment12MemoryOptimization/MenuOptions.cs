namespace Assignment12MemoryOptimization
{
    /// <summary>
    /// Specifies the menu options of the application.
    /// </summary>
    internal enum MenuOptions
    {
        /// <summary>
        /// Specifies the task 1 containing the original unoptimized code.
        /// </summary>
        OriginalUnoptimizedCode = 1,

        /// <summary>
        /// Specifies the task 2 containing the optimized code with fixed sized list.
        /// </summary>
        FixedSizedList = 2,

        /// <summary>
        /// Specifies the task 2 containing the optimized code with bounded memory.
        /// </summary>
        BoundedMemoryWithMaxListCount = 3,

        /// <summary>
        /// Specifies the task 3 displaying the inferences to the user.
        /// </summary>
        DisplayInferences = 4,

        /// <summary>
        /// Specifies exiting the application.
        /// </summary>
        Exit = 5,
    }
}
