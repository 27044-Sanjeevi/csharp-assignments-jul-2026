using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment13CollectionsAndGenerics
{
    internal class StudentDictionary
    {
        private Dictionary<string, int> _students = new ();

        public void PerformOperations()
        {
            ConsoleHelpers.DisplayTitle("TASK 4 : STUDENT DICTIONARY");
            ConsoleHelpers.DisplayStatus("Created a new dictionary with key = student name, value = grade.");
            this.AddFiveStudents();
            ConsoleHelpers.DisplayStatus("Added five students details into the dictionary.");
            this.DisplayDictionary();
            if (this.Remove("Bala"))
            {
                ConsoleHelpers.DisplayStatus("Removed a student from the dictionary.");
            }

            this.DisplayDictionary();
        }

        public void Add(string name, int key)
        {
            this._students.Add(name, key);
        }

        public bool Remove(string name)
        {
            if (!this._students.Remove(name))
            {
                Console.WriteLine("The given key doesn't exist in the dictionary.");
                return false;
            }

            return true;
        }

        public void DisplayDictionary()
        {
            ConsoleHelpers.WriteColored("\nCurrent Dictionary (Name : Grade):\n", ConsoleColor.Cyan);
            foreach (var student in this._students)
            {
                Console.WriteLine($"{student.Key} : {student.Value}");
            }
        }

        private void AddFiveStudents()
        {
            this.Add("Arun", 3);
            this.Add("Bala", 2);
            this.Add("Cbum", 5);
            this.Add("Dicaprio", 4);
            this.Add("Elias", 1);
        }
    }
}
