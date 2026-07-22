namespace BasicContactManagerAssignment1.View
{
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.Utilities;

    /// <summary>
    /// Handles console rendering and user interaction.
    /// </summary>
    internal class ContactConsoleUI
    {
        private readonly ConsoleIO _consoleIo;
        private readonly ConsoleInputHelper _helpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactConsoleUI"/> class.
        /// </summary>
        /// <param name="consoleIo">Provides console input and output operations for the user interfac</param>
        /// <exception cref="ArgumentNullException">Throws exception when null</exception>
        public ContactConsoleUI(ConsoleIO consoleIo)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._helpers = new ConsoleInputHelper(consoleIo);
        }

        /// <summary>
        /// Displays the main application menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this._consoleIo.ClearAndWriteColored("========================================", ConsoleColor.Cyan);
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
        /// Gets the user's menu choice.
        /// </summary>
        /// <returns>The selected menu option as an integer.</returns>
        public int GetMenuChoice()
        {
            return this._helpers.GetMenuChoice("Choose a functionality to continue: ", 1, 7);
        }

        /// <summary>
        /// Collects details for a new contact.
        /// </summary>
        /// <returns>A new ContactInfo object with user-provided details.</returns>
        public ContactInfo GetNewContactDetails()
        {
            this._consoleIo.WriteColored("=== ADD NEW CONTACT ===", ConsoleColor.Yellow);

            return new ContactInfo
            {
                Name = this._helpers.GetRequiredString("Enter Name (Required): "),
                Phone = this._helpers.GetOptionalString("Enter Phone Number (Optional): "),
                Email = this._helpers.GetOptionalString("Enter Email Address (Optional): "),
                Notes = this._helpers.GetOptionalString("Enter Additional Notes (Optional): "),
            };
        }

        /// <summary>
        /// Collects updated contact information.
        /// </summary>
        /// <param name="contact",>The existing contact information to be updated.</param>
        /// <returns>A ContactInfo object with updated details.</returns>
        public ContactInfo GetUpdatedContactDetails(ContactInfo contact)
        {
            ContactInfo updatedContact = new ContactInfo
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Notes = contact.Notes,
            };

            this._consoleIo.ClearAndWriteColored($"--- Editing Contact: {contact.Name} ---", ConsoleColor.Yellow);

            this._consoleIo.WriteLine("Press [Enter] to keep current value.");

            this._consoleIo.Write($"Name [Current: {contact.Name}]: ");
            string? input = this._consoleIo.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                updatedContact.Name = input;
            }

            this._consoleIo.Write($"Phone [Current: {contact.Phone}]: ");
            input = this._consoleIo.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                updatedContact.Phone = input;
            }

            this._consoleIo.Write($"Email [Current: {contact.Email}]: ");
            input = this._consoleIo.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                updatedContact.Email = input;
            }

            this._consoleIo.Write($"Notes [Current: {contact.Notes}]: ");
            input = this._consoleIo.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                updatedContact.Notes = input;
            }

            return updatedContact;
        }

        /// <summary>
        /// Prompts for a search term.
        /// </summary>
        /// <returns>The search term entered by the user.</returns>
        public string GetSearchTerm()
        {
            this._consoleIo.WriteColored(
                "=== SEARCH CONTACTS ===",
                ConsoleColor.Yellow);

            return this._helpers.GetRequiredString(
                "Enter search term (name, phone, email, or notes): ");
        }

        /// <summary>
        /// Gets selected sort field.
        /// </summary>
        /// <returns>The selected sort field as a SortField enum.</returns>
        public SortField GetSortField()
        {
            this._consoleIo.WriteColored(
                "=== SORT CONTACTS ===",
                ConsoleColor.Yellow);

            this._consoleIo.WriteLine("Sort by:");
            this._consoleIo.WriteLine("1. Name");
            this._consoleIo.WriteLine("2. Phone Number");
            this._consoleIo.WriteLine("3. Email Address");

            int choice = this._helpers.GetMenuChoice(
                "Choose sort field (1-3): ",
                1,
                3);

            return (SortField)choice;
        }

        /// <summary>
        /// Gets selected sort direction.
        /// </summary>
        /// <returns>True for ascending, false for descending.</returns>
        public bool GetSortDirection()
        {
            this._consoleIo.WriteLine(string.Empty);
            this._consoleIo.WriteLine("Sort direction:");
            this._consoleIo.WriteLine("1. Ascending");
            this._consoleIo.WriteLine("2. Descending");

            int choice = this._helpers.GetMenuChoice("Choose direction (1-2): ", 1, 2);

            return choice == 1;
        }

        /// <summary>
        /// Displays contacts in a table.
        /// </summary>
        /// <param name="contacts">List of contacts to display.</param>
        public void DisplayContactsTable(List<ContactInfo> contacts)
        {
            if (contacts.Count == 0)
            {
                this._consoleIo.WriteLine("No contacts to display.");
                return;
            }

            this._consoleIo.WriteLine(
                string.Format(
                    "{0,-20} | {1,-15} | {2,-25} | {3}",
                    "Name",
                    "Phone",
                    "Email",
                    "Notes"));

            this._consoleIo.WriteLine(new string('-', 85));

            foreach (ContactInfo contact in contacts)
            {
                this._consoleIo.WriteLine(
                    string.Format(
                        "{0,-20} | {1,-15} | {2,-25} | {3}",
                        this._helpers.Truncate(contact.Name ?? string.Empty, 20),
                        this._helpers.Truncate(contact.Phone ?? "N/A", 15),
                        this._helpers.Truncate(contact.Email ?? "N/A", 25),
                        this._helpers.Truncate(contact.Notes ?? "N/A", 20)));
            }

            this._consoleIo.WriteLine(new string('-', 85));
            this._consoleIo.WriteLine($"Total Contacts: {contacts.Count}");
        }

        /// <summary>
        /// Allows the user to select a contact.
        /// </summary>
        /// <param name="contacts">List of contacts to choose from.</param>
        /// <param name="actionName">Action being performed (e.g., "edit", "delete").</param>
        /// <returns>The selected ContactInfo object or null if canceled.</returns>
        public ContactInfo? SelectContact(List<ContactInfo> contacts, string actionName)
        {
            this._consoleIo.WriteLine(
                $"Available contacts to {actionName}:");

            for (int i = 0; i < contacts.Count; i++)
            {
                this._consoleIo.WriteLine(
                    $"[{i + 1}] {contacts[i].Name} | " +
                    $"{contacts[i].Phone ?? "N/A"} | " +
                    $"{contacts[i].Email ?? "N/A"}");
            }

            int choice = this._helpers.GetMenuChoice($"Select contact index (1-{contacts.Count}) or 0 to cancel: ", 0, contacts.Count);

            return choice == 0 ? null : contacts[choice - 1];
        }

        /// <summary>
        /// Shows a success message in green color.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void ShowSuccessMessage(string message)
        {
            this._consoleIo.WriteColored(message, ConsoleColor.Green);
        }

        /// <summary>
        /// Shows an error message in red color.
        /// </summary>
        /// <param name="message">Error message to be displayed.</param>
        public void ShowErrorMessage(string message)
        {
            this._consoleIo.WriteColored(message, ConsoleColor.Red);
        }

        /// <summary>
        /// Pauses the console and prompts the user to return to the main menu.
        /// </summary>
        public void PauseAndReturnToMenu()
        {
            this._consoleIo.WriteLine("\nPress any key to return to the main menu...");
            this._consoleIo.ReadKey(true);
            this._consoleIo.Clear();
        }

        /// <summary>
        /// Clears the console screen.
        /// </summary>
        public void Clear()
        {
            this._consoleIo.Clear();
        }
    }
}