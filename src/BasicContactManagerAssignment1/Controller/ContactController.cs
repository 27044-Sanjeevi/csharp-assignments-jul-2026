namespace BasicContactManagerAssignment1.Controller
{
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.Utilities;
    using BasicContactManagerAssignment1.View;

    /// <summary>
    /// Class to handle the control operations between UI and Service layers
    /// </summary>
    internal class ContactController
    {
        private ContactManager _contactManager;
        private ContactConsoleUI _view;
        private Helpers _helpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class
        /// </summary>
        /// <param name="contactManager">The business logic contact manager.</param>
        /// <param name="view">The view layer of the contact manager</param>
        /// <param name="helpers">Common helpers</param>
        /// <exception cref="ArgumentNullException">Thrown when contact reference parameter is null.</exception>
        public ContactController(ContactManager contactManager, ContactConsoleUI view, Helpers helpers)
        {
            this._contactManager = contactManager ?? throw new ArgumentNullException(nameof(contactManager));
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._helpers = helpers ?? throw new ArgumentNullException(nameof(helpers));
        }

        public bool HandleMenu()
        {
            bool exit = false;
            while (!exit)
            {
                this._view.ShowMainMenu();

                int choice = this._helpers.GetMenuChoice("Choose a functionality to continue: ", 1, 7);
                this._view.Clear();

                switch (choice)
                {
                    case 1:
                        this.AddContact();
                        break;
                    case 2:
                        this.ViewContacts();
                        break;
                    case 3:
                        this.EditContact();
                        break;
                    case 4:
                        this.DeleteContact();
                        break;
                    case 5:
                        this.SearchContacts();
                        break;
                    case 6:
                        this.SortContacts();
                        break;
                    case 7:
                        exit = true;
                        this._view.WriteColored("Thank you for using Basic Contact Manager. Goodbye!", ConsoleColor.Cyan);
                        break;
                }
            }
        }

        /// <summary>
        /// Contains the process of adding a new contact.
        /// </summary>
        private void AddContact()
        {
            this._view.WriteColored("=== ADD NEW CONTACT ===", ConsoleColor.Yellow);

            string name = this._helpers.GetRequiredString("Enter Name (Required): ");
            string? phone = this._helpers.GetOptionalString("Enter Phone Number (Optional): ");
            string? email =this._helpers.GetOptionalString("Enter Email Address (Optional): ");
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
                this._view.WriteColored("\nContact added successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                this._view.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Displays all contacts in a table.
        /// </summary>
        private void ViewContacts()
        {
            this._view.WriteColored("=== VIEW ALL CONTACTS ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = _contactManager.GetAllContacts();
            this.DisplayContactsTable(contacts);
        }

        /// <summary>
        /// Orchestrates the process of editing an existing contact.
        /// </summary>
        private void EditContact()
        {
            this._view.WriteColored("=== EDIT EXISTING CONTACT ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = _contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                this._view.WriteLine("No contacts available to edit.");
                return;
            }

            ContactInfo? selected = SelectContact(contacts, "edit");
            if (selected == null)
            {
                this._view.WriteLine("Edit cancelled.");
                return;
            }

            _consoleIo.ClearAndWriteColored($"--- Editing Contact: {selected.Name} ---", ConsoleColor.Yellow);

            _consoleIo.WriteLine("Press [Enter] to keep current value.");

            _consoleIo.Write($"Name [Current: {selected.Name}]: ");
            string? input = _consoleIo.ReadLine()?.Trim();
            selected.Name = string.IsNullOrEmpty(input) ? selected.Name : input;

            _consoleIo.Write($"Phone [Current: {selected.Phone}]: ");
            input = _consoleIo.ReadLine()?.Trim();
            selected.Phone = string.IsNullOrEmpty(input) ? selected.Phone : input;

            _consoleIo.Write($"Email [Current: {selected.Email}]: ");
            input = _consoleIo.ReadLine()?.Trim();
            selected.Email = string.IsNullOrEmpty(input) ? selected.Email : input;

            _consoleIo.Write($"Notes [Current: {selected.Notes}]: ");
            input = _consoleIo.ReadLine()?.Trim();
            selected.Notes = string.IsNullOrEmpty(input) ? selected.Notes : input;

            try
            {
                _contactManager.UpdateContact(selected);
                _consoleIo.WriteColored("\nContact updated successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                _consoleIo.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Allows the user to select and delete an existing contact.
        /// </summary>
        private void DeleteContact()
        {
            _consoleIo.WriteColored("=== DELETE CONTACT ===", ConsoleColor.Yellow);
            List<ContactInfo> contacts = _contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                _consoleIo.WriteLine("No contacts available to delete.");
                return;
            }

            ContactInfo? selected = SelectContact(contacts, "delete");
            if (selected == null)
            {
                _consoleIo.WriteLine("Delete cancelled.");
                return;
            }
            else
            {
                try
                {
                    _contactManager.DeleteContact(selected.Id);
                    _consoleIo.WriteColored("\nContact deleted successfully!", ConsoleColor.Green);
                }
                catch (KeyNotFoundException ex)
                {
                    _consoleIo.WriteColored($"\nError: {ex.Message}", ConsoleColor.Red);
                }
            }
        }
        /// <summary>
        /// Prompts the user for a search term and displays matching contacts.
        /// </summary
        private void SearchContacts()
        {
            _consoleIo.WriteColored("=== SEARCH CONTACTS ===", ConsoleColor.Yellow);
            string term = _helpers.GetRequiredString("Enter search term (name, phone, email, or notes): ");

            List<ContactInfo> results = _contactManager.SearchContacts(term);
            _consoleIo.ClearAndWriteColored($"=== SEARCH RESULTS FOR '{term}' ===", ConsoleColor.Yellow);
            DisplayContactsTable(results);
        }

        /// <summary>
        /// Prompts the user for sorting options and displays the sorted contact list.
        /// </summary>
        private void SortContacts()
        {
            _consoleIo.WriteColored("=== SORT CONTACTS ===", ConsoleColor.Yellow);
            _consoleIo.WriteLine("Sort by:");
            _consoleIo.WriteLine("1. Name");
            _consoleIo.WriteLine("2. Phone Number");
            _consoleIo.WriteLine("3. Email Address");

            int sortChoice = _helpers.GetMenuChoice("Choose sort field (1-3): ", 1, 3);
            SortField sortField = (SortField)sortChoice;

            _consoleIo.WriteLine("\nSort direction:");
            _consoleIo.WriteLine("1. Ascending");
            _consoleIo.WriteLine("2. Descending");
            int directionChoice = _helpers.GetMenuChoice("Choose direction (1-2): ", 1, 2);
            bool choice = directionChoice == 1;

            List<ContactInfo> sortedList = _contactManager.GetSortedContacts(sortField, choice);
            string directionText = choice ? "Ascending" : "Descending";
            _consoleIo.ClearAndWriteColored($"=== CONTACTS SORTED BY {sortField} ({directionText}) ===", ConsoleColor.Yellow);
            DisplayContactsTable(sortedList);
        }

        /// <summary>
        /// Renders the provided contacts in a tabular format.
        /// </summary>
        /// <param name="contacts">The list of contacts to show.</param>
        private void DisplayContactsTable(List<ContactInfo> contacts)
        {
            if (contacts.Count == 0)
            {
                _consoleIo.WriteLine("No contacts to display.");
                return;
            }

            _consoleIo.WriteLine(string.Format("{0,-20} | {1,-15} | {2,-25} | {3}", "Name", "Phone", "Email", "Notes"));
            _consoleIo.WriteLine(new string('-', 85));

            foreach (ContactInfo contact in contacts)
            {
                string name = Truncate(contact.Name ?? string.Empty, 20);
                string phone = Truncate(contact.Phone ?? "N/A", 15);
                string email = Truncate(contact.Email ?? "N/A", 25);
                string notes = Truncate(contact.Notes ?? "N/A", 20);

                _consoleIo.WriteLine(string.Format("{0,-20} | {1,-15} | {2,-25} | {3}", name, phone, email, notes));
            }

            _consoleIo.WriteLine(new string('-', 85));
            _consoleIo.WriteLine($"Total Contacts: {contacts.Count}");
        }

        /// <summary>
        /// Prompts the user to select a contact from a list by standard choice numbers.
        /// </summary>
        /// <param name="contacts">The list of contacts.</param>
        /// <param name="actionName">The action being performed (e.g. edit, delete).</param>
        /// <returns>The selected contact or null if cancelled.</returns>
        private ContactInfo? SelectContact(List<ContactInfo> contacts, string actionName)
        {
            _consoleIo.WriteLine($"Available contacts to {actionName}:");
            for (int i = 0; i < contacts.Count; i++)
            {
                _consoleIo.WriteLine($"[{i + 1}] {contacts[i].Name} (Phone: {contacts[i].Phone ?? "N/A"})");
            }

            int choice = _helpers.GetMenuChoice($"Select contact index (1-{contacts.Count}) or 0 to cancel: ", 0, contacts.Count);
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
