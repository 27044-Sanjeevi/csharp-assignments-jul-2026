namespace BasicContactManagerAssignment1
{
    using System;
    using System.Collections.Generic;
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;

    /// <summary>
    /// Handles the console user interface flow and rendering for the contact manager.
    /// </summary>
    internal class ContactConsoleUI
    {
        private ContactManager _contactManager;
        private ConsoleIO _consoleIo;
        private Helpers _helpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactConsoleUI"/> class.
        /// </summary>
        /// <param name="contactManager">The business logic contact manager.</param>
        /// <param name="consoleIo">The console input/output abstraction.</param>
        /// <exception cref="ArgumentNullException">Thrown when contactManager or consoleIo is null.</exception>
        public ContactConsoleUI(ContactManager contactManager, ConsoleIO consoleIo)
        {
            this._contactManager = contactManager ?? throw new ArgumentNullException(nameof(contactManager));
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._helpers = new Helpers(consoleIo);
        }

        /// <summary>
        /// Runs the main application loop.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    this.ShowMainMenu();
                    int choice = this._helpers.GetMenuChoice("Choose a functionality to continue: ", 1, 7);
                    this._consoleIo.Clear();

                    switch (choice)
                    {
                        case 1: this.AddContactFlow(); break;
                        case 2: this.ViewContactsFlow(); break;
                        case 3: this.EditContactFlow(); break;
                        case 4: this.DeleteContactFlow(); break;
                        case 5: this.SearchContactsFlow(); break;
                        case 6: this.SortContactsFlow(); break;
                        case 7:
                            exit = true;
                            this._consoleIo.WriteColored("Thank you for using Basic Contact Manager. Goodbye!", ConsoleColor.Cyan);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this._consoleIo.WriteColored($"An unexpected error occurred: {ex.Message}", ConsoleColor.Red);
                }

                if (!exit)
                {
                    this._consoleIo.WriteLine("\nPress any key to return to the main menu...");
                    this._consoleIo.ReadKey(true);
                    this._consoleIo.Clear();
                }
            }
        }

        /// <summary>
        /// Displays the main application header and menu options.
        /// </summary>
        private void ShowMainMenu()
        {
            this._consoleIo.Clear();
            this._consoleIo.WriteColored("========================================", ConsoleColor.Cyan);
            this._consoleIo.WriteColored("         BASIC CONTACT MANAGER          ", ConsoleColor.Cyan);
            this._consoleIo.WriteColored("========================================", ConsoleColor.Cyan);
            this._consoleIo.WriteLine("1. Add New Contact");
            this._consoleIo.WriteLine("2. View All Contacts");
            this._consoleIo.WriteLine("3. Edit Existing Contact");
            this._consoleIo.WriteLine("4. Delete Contact");
            this._consoleIo.WriteLine("5. Search Contacts");
            this._consoleIo.WriteLine("6. Sort Contacts");
            this._consoleIo.WriteLine("7. Exit");
            this._consoleIo.WriteLine("----------------------------------------");
        }

        /// <summary>
        /// the process of adding a new contact.
        /// </summary>
        private void AddContactFlow()
        {
            this._consoleIo.WriteColored("=== ADD NEW CONTACT ===", ConsoleColor.Yellow);

            string name = this._helpers.GetRequiredString("Enter Name (Required): ");
            string? phone = this._helpers.GetOptionalString("Enter Phone Number (Optional): ");
            string? email = this._helpers.GetOptionalString("Enter Email Address (Optional): ");
            string? notes = this._helpers.GetOptionalString("Enter Additional Notes (Optional): ");

            ContactInfo newContact = new ContactInfo
            {
                Name = name,
                Phone = phone,
                Email = email,
                Notes = notes,
            };

            try
            {
                this._contactManager.AddContact(newContact);
                this._consoleIo.WriteColored("\nContact added successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                this._consoleIo.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Displays all contacts in a table.
        /// </summary>
        private void ViewContactsFlow()
        {
            this._consoleIo.WriteColored("=== VIEW ALL CONTACTS ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();
            this.DisplayContactsTable(contacts);
        }

        /// <summary>
        /// Orchestrates the process of editing an existing contact.
        /// </summary>
        private void EditContactFlow()
        {
            this._consoleIo.WriteColored("=== EDIT EXISTING CONTACT ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                this._consoleIo.WriteLine("No contacts available to edit.");
                return;
            }

            ContactInfo? selected = this.SelectContact(contacts, "edit");
            if (selected == null)
            {
                this._consoleIo.WriteLine("Edit cancelled.");
                return;
            }

            this._consoleIo.Clear();
            this._consoleIo.WriteColored($"--- Editing Contact: {selected.Name} ---", ConsoleColor.Yellow);

            this._consoleIo.WriteLine("Press [Enter] to keep current value.");

            this._consoleIo.Write($"Name [Current: {selected.Name}]: ");
            string? input = this._consoleIo.ReadLine()?.Trim();
            selected.Name = string.IsNullOrEmpty(input) ? selected.Name : input;

            this._consoleIo.Write($"Phone [Current: {selected.Phone}]: ");
            input = this._consoleIo.ReadLine()?.Trim();
            selected.Phone = string.IsNullOrEmpty(input) ? selected.Phone : input;

            this._consoleIo.Write($"Email [Current: {selected.Email}]: ");
            input = this._consoleIo.ReadLine()?.Trim();
            selected.Email = string.IsNullOrEmpty(input) ? selected.Email : input;

            this._consoleIo.Write($"Notes [Current: {selected.Notes}]: ");
            input = this._consoleIo.ReadLine()?.Trim();
            selected.Notes = string.IsNullOrEmpty(input) ? selected.Notes : input;

            try
            {
                this._contactManager.UpdateContact(selected);
                this._consoleIo.WriteColored("\nContact updated successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                this._consoleIo.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// flow of the process of deleting a contact.
        /// </summary>
        private void DeleteContactFlow()
        {
            this._consoleIo.WriteColored("=== DELETE CONTACT ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                this._consoleIo.WriteLine("No contacts available to delete.");
                return;
            }

            ContactInfo? selected = this.SelectContact(contacts, "delete");
            if (selected == null)
            {
                this._consoleIo.WriteLine("Delete cancelled.");
                return;
            }
            else
            {
                try
                {
                    this._contactManager.DeleteContact(selected.Id);
                    this._consoleIo.WriteColored("\nContact deleted successfully!", ConsoleColor.Green);
                }
                catch (KeyNotFoundException ex)
                {
                    this._consoleIo.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// flow of the process of searching contacts.
        /// </summary>
        private void SearchContactsFlow()
        {
            this._consoleIo.WriteColored("=== SEARCH CONTACTS ===", ConsoleColor.Yellow);
            string term = this._helpers.GetRequiredString("Enter search term (name, phone, email, or notes): ");

            List<ContactInfo> results = this._contactManager.SearchContacts(term);
            this._consoleIo.Clear();
            this._consoleIo.WriteColored($"=== SEARCH RESULTS FOR '{term}' ===", ConsoleColor.Yellow);
            this.DisplayContactsTable(results);
        }

        /// <summary>
        /// flow of the sorting settings and displays sorted contacts.
        /// </summary>
        private void SortContactsFlow()
        {
            this._consoleIo.WriteColored("=== SORT CONTACTS ===", ConsoleColor.Yellow);
            this._consoleIo.WriteLine("Sort by:");
            this._consoleIo.WriteLine("1. Name");
            this._consoleIo.WriteLine("2. Phone Number");
            this._consoleIo.WriteLine("3. Email Address");

            int sortChoice = this._helpers.GetMenuChoice("Choose sort field (1-3): ", 1, 3);
            string sortBy = "name";
            if (sortChoice == 2)
            {
                sortBy = "phone";
            }
            else if (sortChoice == 3)
            {
                sortBy = "email";
            }

            this._consoleIo.WriteLine("\nSort direction:");
            this._consoleIo.WriteLine("1. Ascending");
            this._consoleIo.WriteLine("2. Descending");
            int directionChoice = this._helpers.GetMenuChoice("Choose direction (1-2): ", 1, 2);
            bool choice = directionChoice == 1;

            List<ContactInfo> sortedList = this._contactManager.GetSortedContacts(sortBy, choice);
            this._consoleIo.Clear();
            string directionText = choice ? "Ascending" : "Descending";
            this._consoleIo.WriteColored($"=== CONTACTS SORTED BY {sortBy} ({directionText}) ===", ConsoleColor.Yellow);
            this.DisplayContactsTable(sortedList);
        }

        /// <summary>
        /// print list of contacts inside a formatted grid table.
        /// </summary>
        /// <param name="contacts">The list of contacts to show.</param>
        private void DisplayContactsTable(List<ContactInfo> contacts)
        {
            if (contacts.Count == 0)
            {
                this._consoleIo.WriteLine("No contacts to display.");
                return;
            }

            this._consoleIo.WriteLine(string.Format("{0,-20} | {1,-15} | {2,-25} | {3}", "Name", "Phone", "Email", "Notes"));
            this._consoleIo.WriteLine(new string('-', 85));

            foreach (ContactInfo contact in contacts)
            {
                string name = this.Truncate(contact.Name ?? string.Empty, 20);
                string phone = this.Truncate(contact.Phone ?? "N/A", 15);
                string email = this.Truncate(contact.Email ?? "N/A", 25);
                string notes = this.Truncate(contact.Notes ?? "N/A", 20);

                this._consoleIo.WriteLine(string.Format("{0,-20} | {1,-15} | {2,-25} | {3}", name, phone, email, notes));
            }

            this._consoleIo.WriteLine(new string('-', 85));
            this._consoleIo.WriteLine($"Total Contacts: {contacts.Count}");
        }

        /// <summary>
        /// Prompts the user to select a contact from a list by standard choice numbers.
        /// </summary>
        /// <param name="contacts">The list of contacts.</param>
        /// <param name="actionName">The action being performed (e.g. edit, delete).</param>
        /// <returns>The selected contact or null if cancelled.</returns>
        private ContactInfo? SelectContact(List<ContactInfo> contacts, string actionName)
        {
            this._consoleIo.WriteLine($"Available contacts to {actionName}:");
            for (int i = 0; i < contacts.Count; i++)
            {
                this._consoleIo.WriteLine($"[{i + 1}] {contacts[i].Name} (Phone: {contacts[i].Phone ?? "N/A"})");
            }

            int choice = this._helpers.GetMenuChoice($"Select contact index (1-{contacts.Count}) or 0 to cancel: ", 0, contacts.Count);
            if (choice == 0)
            {
                return null;
            }

            return contacts[choice - 1];
        }

        /// <summary>
        /// Truncates string to a max length and appends ellipsis if truncated.
        /// </summary>
        /// <param name="val">The string value.</param>
        /// <param name="maxLength">The maximum length allowed.</param>
        /// <returns>A truncated string.</returns>
        private string Truncate(string val, int maxLength)
        {
            if (val.Length <= maxLength)
            {
                return val;
            }

            return val.Substring(0, maxLength - 3) + "...";
        }
    }
}
