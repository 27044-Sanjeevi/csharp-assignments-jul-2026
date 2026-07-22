namespace BasicContactManagerAssignment1.View
{
    using System;
    using System.Collections.Generic;
    using BasicContactManagerAssignment1.Controller;
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.Utilities;

    /// <summary>
    /// Handles the console user interface flow and rendering for the contact manager.
    /// </summary>
    internal class ContactConsoleUI
    {
        private ConsoleIO _consoleIo;
        private Helpers _helpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactConsoleUI"/> class.
        /// </summary>
        /// <param name="consoleIo">The console input/output abstraction.</param>
        /// <exception cref="ArgumentNullException">Thrown when contactManager or consoleIo is null.</exception>
        public ContactConsoleUI( ConsoleIO consoleIo)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._helpers = new Helpers(consoleIo);
        }

        /// <summary>
        /// Runs the main application loop.
        /// </summary>
        public void RunApplication()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    this.ShowMainMenu();
                    int choice = _helpers.GetMenuChoice("Choose a functionality to continue: ", 1, 7);
                    _consoleIo.Clear();

                    switch (choice)
                    {
                        case 1:
                            this.AddContact();
                            break;
                        case 2:
                            ViewContacts();
                            break;
                        case 3:
                            EditContact();
                            break;
                        case 4:
                            DeleteContact();
                            break;
                        case 5:
                            SearchContacts();
                            break;
                        case 6:
                            SortContacts();
                            break;
                        case 7:
                            exit = true;
                            _consoleIo.WriteColored("Thank you for using Basic Contact Manager. Goodbye!", ConsoleColor.Cyan);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _consoleIo.WriteColored($"An unexpected error occurred: {ex.Message}", ConsoleColor.Red);
                }

                if (!exit)
                {
                    PauseAndReturnToMenu();
                }
            }
        }

        /// <summary>
        /// Displays the main application header and menu options.
        /// </summary>
        private void ShowMainMenu()
        {
            _consoleIo.ClearAndWriteColored("========================================", ConsoleColor.Cyan);
            _consoleIo.WriteColored("         BASIC CONTACT MANAGER          ", ConsoleColor.Cyan);
            _consoleIo.WriteColored("========================================", ConsoleColor.Cyan);
            _consoleIo.WriteLine("1. Add New Contact");
            _consoleIo.WriteLine("2. View All Contacts");
            _consoleIo.WriteLine("3. Edit Existing Contact");
            _consoleIo.WriteLine("4. Delete Contact");
            _consoleIo.WriteLine("5. Search Contacts");
            _consoleIo.WriteLine("6. Sort Contacts");
            _consoleIo.WriteLine("7. Exit");
            _consoleIo.WriteLine("----------------------------------------");
        }

        /// <summary>
        /// Waits for user input and clears the console before returning to the main menu.
        /// </summary>
        private void PauseAndReturnToMenu()
        {
            _consoleIo.WriteLine("\nPress any key to return to the main menu...");
            _consoleIo.ReadKey(true);
            _consoleIo.Clear();
        }
    }
}
