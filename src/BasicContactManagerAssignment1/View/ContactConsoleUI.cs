namespace BasicContactManagerAssignment1.View
{
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.Utilities;
    using ConsoleTables;

    /// <summary>
    /// Enum for the Action Item of the contact
    /// </summary>
    internal enum ContactAction
    {
        /// <summary>
        /// Retrieves contact for edit
        /// </summary>
        Edit = 1,

        /// <summary>
        /// Retrieves contacts for delete
        /// </summary>
        Delete = 2,
    }

    /// <summary>
    /// Handles console rendering and user interaction.
    /// </summary>
    internal class ContactConsoleUI
    {
        private const int MenuChoiceMin = 1;
        private const int MenuChoiceMax = 7;

        private readonly ConsoleIO _consoleIo;
        private readonly ConsoleInputHelper _consoleInputHelpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactConsoleUI"/> class.
        /// </summary>
        /// <param name="consoleIo">Provides console input and output operations for the user interface</param>
        /// <param name="consoleInputHelpers">Helpers object injection.</param>
        /// <exception cref="ArgumentNullException">Throws exception when null</exception>
        public ContactConsoleUI(ConsoleIO consoleIo, ConsoleInputHelper consoleInputHelpers)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._consoleInputHelpers = consoleInputHelpers ?? throw new ArgumentNullException(nameof(consoleInputHelpers));
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
        /// Prompts for user input until a non-empty string is entered.
        /// </summary>
        /// <param name="prompt">The message displayed to the user when requesting input.</param>
        /// <returns>A non-empty string entered by the user.</returns>
        public string GetRequiredString(string prompt)
        {
            while (true)
            {
                string? result = this._consoleInputHelpers.TryGetRequiredString(prompt);
                if (result != null)
                {
                    return result;
                }

                this._consoleIo.WriteColored("Input cannot be empty. Please try again.", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Gets the user's menu choice.
        /// </summary>
        /// <returns>The selected menu option as an integer.</returns>
        public int GetMenuChoiceLoop()
        {
            while (true)
            {
                int choice = this._consoleInputHelpers.TryGetMenuChoice("Choose a functionality to continue: ", MenuChoiceMin, MenuChoiceMax);

                if (choice != -1)
                {
                    return choice;
                }

                this._consoleIo.WriteColored($"Invalid choice. Please enter a number between {MenuChoiceMin} and {MenuChoiceMax}.", ConsoleColor.Red);
            }
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
                Name = this.GetRequiredString("Enter Name (Required): "),
                PhoneNumber = this._consoleInputHelpers.GetOptionalString("Enter Phone Number (Optional): "),
                Email = this._consoleInputHelpers.GetOptionalString("Enter Email Address (Optional): "),
                Notes = this._consoleInputHelpers.GetOptionalString("Enter Additional Notes (Optional): "),
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
                PhoneNumber = contact.PhoneNumber,
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

            this._consoleIo.Write($"Phone [Current: {contact.PhoneNumber}]: ");
            input = this._consoleIo.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                updatedContact.PhoneNumber = input;
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

            return this._consoleInputHelpers.GetRequiredString(
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

            int choice = this._consoleInputHelpers.GetMenuChoice("Choose sort field (1-3): ", 1, 3);

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

            int choice = this._consoleInputHelpers.GetMenuChoice("Choose direction (1-2): ", 1, 2);

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

            ConsoleTable table = new ConsoleTable("Name", "Phone Number", "Email", "Notes");

            foreach (ContactInfo contact in contacts)
            {
                table.AddRow(
                    contact.Name ?? string.Empty,
                    contact.PhoneNumber ?? "N/A",
                    contact.Email ?? "N/A",
                    contact.Notes ?? "N/A");
            }

            this._consoleIo.WriteLine(table.ToString());
        }

        /// <summary>
        /// Allows the user to select a contact.
        /// </summary>
        /// <param name="contacts">List of contacts to choose from.</param>
        /// <param name="action">Action being performed (e.g., "edit", "delete").</param>
        /// <returns>The selected ContactInfo object or null if canceled.</returns>
        public ContactInfo? SelectContact(List<ContactInfo> contacts, ContactAction action)
        {
            this._consoleIo.WriteLine($"Available contacts to {action.ToString()}:");

            for (int i = 0; i < contacts.Count; i++)
            {
                this._consoleIo.WriteLine(
                    $"[{i + 1}] {contacts[i].Name} | " +
                    $"{contacts[i].PhoneNumber ?? "N/A"} | " +
                    $"{contacts[i].Email ?? "N/A"}");
            }

            int choice = this._consoleInputHelpers.GetMenuChoice($"Select contact index (1-{contacts.Count}) or 0 to cancel: ", 0, contacts.Count);

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

        /// <summary>
        /// Truncates a string to the specified maximum length, appending "..." if it exceeds that length.
        /// </summary>
        /// <param name="value">String to be truncated.</param>
        /// <param name="maxLength">Maxlength allowed to be printed</param>
        /// <returns>Truncated message</returns>
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) ||
                value.Length <= maxLength)
            {
                return value;
            }

            return value.Substring(0, maxLength - 3) + "...";
        }
    }
}