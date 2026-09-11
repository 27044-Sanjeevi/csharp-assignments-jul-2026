namespace Assignment13CollectionsAndGenerics.Data
{
    /// <summary>
    /// Represents the test data for working with collections.
    /// </summary>
    internal static class TestData
    {
        // Task 1 data

        /// <summary>
        /// Gets the title of the book to remove.
        /// </summary>
        /// <value>A string holding the name of the book.</value>
        public static string BookToRemove => "Atomic Habits";

        /// <summary>
        /// Gets the title of the book used to check for existence.
        /// </summary>
        /// <value>A string holding the name of the book.</value>
        public static string BookToCheckExistence => "Lord of the Rings";

        /// <summary>
        /// Gets a list of book titles.
        /// </summary>
        /// <value>A list of string holding the name of the books.</value>
        public static List<string> Books => new List<string>()
        {
            "Atomic Habits",
            "Power of Mind",
            "Rich Dad, Poor Dad",
            "Harry Potter",
            "Lord of the Rings",
        };

        // Task 2 data

        /// <summary>
        /// Gets a list of characters representing the initial stack.
        /// </summary>
        /// <value>A list holding the initial elements of the stack.</value>
        public static List<char> InitialStack => "collection".ToList();

        // Task 3 data

        /// <summary>
        /// Gets a queue containing a predefined list of person names.
        /// </summary>
        /// <value>A queue containing the a list of names.</value>
        public static Queue<string> PersonQueue => new Queue<string>(new[]
        {
            "Arun",
            "Bala",
            "Cbum",
            "Dicaprio",
            "Elias",
        });

        // Task 4 data

        /// <summary>
        /// Gets a dictionary of student names and their corresponding grades.
        /// </summary>
        /// <value>A dictionary holding the student name and grade.</value>
        public static Dictionary<string, int> StudentGrades => new Dictionary<string, int>()
        {
            { "Arun", 3 },
            { "Bala", 2 },
            { "Cbum", 5 },
            { "Dicaprio", 4 },
            { "Elias", 1 },
        };

        /// <summary>
        /// Gets the name of the student to remove.
        /// </summary>
        /// <value>A string holding the name of the student.</value>
        public static string StudentToRemove => "Bala";

        public static List<int> integerList => new List<int>()
        {
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
        };

        public static int[] integerArray => new int[]
        {
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
        };

        public static Queue<int> integerQueue => new Queue<int>(new[]
        {
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
        });

        public static Dictionary<string, int> InventoryDictionary => new Dictionary<string, int>()
        {
            { "Apple", 5 },
            { "Banana", 8 },
            { "Orange", 12 },
            { "Mango", 7 },
            { "Cherry", 15 },
        };
    }
}
