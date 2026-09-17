namespace Assignment13CollectionsAndGenerics.Collections
{
    /// <summary>
    /// Represents a collection that maps student identifiers to their corresponding grades.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary. Must be non-nullable.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary, representing student grades.</typeparam>
    internal class StudentDictionary<TKey, TValue>
        where TKey : notnull
    {
        private Dictionary<TKey, TValue> _students = new ();

        /// <summary>
        /// Performs a sequence of operations on a student dictionary.
        /// </summary>
        /// <param name="initialStudents">A dictionary containing student names as keys and their corresponding grades as values.</param>
        /// <param name="keyToRemove">The key of the student to remove from the dictionary.</param>
        public void PerformOperations(Dictionary<TKey, TValue> initialStudents, TKey keyToRemove)
        {
            ConsoleHelpers.DisplayTitle("TASK 4 : STUDENT DICTIONARY");
            ConsoleHelpers.DisplayStatus("Created a new dictionary with key = student name, value = grade.");
            this.AddMultipleItems(initialStudents);
            ConsoleHelpers.DisplayStatus("Added student details into the dictionary.");
            this.DisplayAllItems();
            if (this.Remove(keyToRemove))
            {
                ConsoleHelpers.DisplayStatus($"Removed the student {keyToRemove} from the dictionary.");
            }
            else
            {
                ConsoleHelpers.DisplayFailure($"The student {keyToRemove} doesn't exist in the dictionary.");
            }

            this.DisplayAllItems();
        }

        /// <summary>
        /// Adds the specified key and value to the collection.
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to associate with the key.</param>
        public void Add(TKey key, TValue value)
        {
            this._students.Add(key, value);
        }

        /// <summary>
        /// Removes the student associated with the specified key from the dictionary.
        /// </summary>
        /// <remarks>If the key does not exist, a message is printed to the console.</remarks>
        /// <param name="key">The key of the student to remove.</param>
        /// <returns>true if the student was successfully removed; otherwise, false.</returns>
        public bool Remove(TKey key)
        {
            if (!this._students.Remove(key))
            {
                Console.WriteLine("The given key doesn't exist in the dictionary.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Displays all students and their grades in the dictionary.
        /// </summary>
        public void DisplayAllItems()
        {
            ConsoleHelpers.WriteColored("\nCurrent Dictionary (Name : Grade):\n", ConsoleColor.Cyan);
            foreach (var student in this._students)
            {
                Console.WriteLine($"- {student.Key} : {student.Value}");
            }
        }

        private void AddMultipleItems(Dictionary<TKey, TValue> students)
        {
            foreach (var student in students)
            {
                this.Add(student.Key, student.Value);
            }
        }
    }
}
