namespace BasicContactManagerAssignment1.Controller
{
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.View;

    /// <summary>
    /// Coordinates interactions between the view and service layers.
    /// </summary>
    internal class ContactController
    {
        private readonly ContactManager _contactManager;
        private readonly ContactConsoleUI _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// </summary>
        /// <param name="contactManager">Handles contact manager operations</param>
        /// <param name="view">Handles view operations</param>
        /// <exception cref="ArgumentNullException">Throws exception when Argument is null.</exception>
        public ContactController(ContactManager contactManager, ContactConsoleUI view)
        {
            this._contactManager = contactManager ?? throw new ArgumentNullException(nameof(contactManager));
            this._view = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <summary>
        /// Handles the main menu loop and user selections.
        /// </summary>
        public void HandleMenu()
        {
            bool exit = false;

            while (!exit)
            {
                this._view.ShowMainMenu();

                int choice = this._view.GetMenuChoice();
                this._view.Clear();

                try
                {
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
                            this._view.ShowSuccessMessage(
                                "Thank you for using Basic Contact Manager. Goodbye!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this._view.ShowErrorMessage(ex.Message);
                }

                if (!exit)
                {
                    this._view.PauseAndReturnToMenu();
                }
            }
        }

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        private void AddContact()
        {
                ContactInfo contact = this._view.GetNewContactDetails();
                this._contactManager.AddContact(contact);
                this._view.ShowSuccessMessage("Contact added successfully!");
        }

        /// <summary>
        /// Displays all contacts.
        /// </summary>
        private void ViewContacts()
        {
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();
            this._view.DisplayContactsTable(contacts);
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        private void EditContact()
        {
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                this._view.ShowErrorMessage("No contacts available to edit.");
                return;
            }

            ContactInfo? selected =
                this._view.SelectContact(contacts, "edit");

            if (selected == null)
            {
                return;
            }

            ContactInfo updatedContact = this._view.GetUpdatedContactDetails(selected);
            this._contactManager.UpdateContact(updatedContact);
            this._view.ShowSuccessMessage("Contact updated successfully!");
        }

        /// <summary>
        /// Deletes an existing contact.
        /// </summary>
        private void DeleteContact()
        {
            List<ContactInfo> contacts = this._contactManager.GetAllContacts();

            if (contacts.Count == 0)
            {
                this._view.ShowErrorMessage("No contacts available to delete.");
                return;
            }

            ContactInfo? selected = this._view.SelectContact(contacts, "delete");

            if (selected == null)
            {
                return;
            }

            this._contactManager.DeleteContact(selected.Id);
            this._view.ShowSuccessMessage("Contact deleted successfully!");
        }

        /// <summary>
        /// Searches contacts and displays matching results.
        /// </summary>
        private void SearchContacts()
        {
            string searchTerm = this._view.GetSearchTerm();

            List<ContactInfo> results = this._contactManager.SearchContacts(searchTerm);

            this._view.DisplayContactsTable(results);
        }

        /// <summary>
        /// Sorts contacts and displays the results.
        /// </summary>
        private void SortContacts()
        {
            SortField sortField = this._view.GetSortField();

            bool isAscending = this._view.GetSortDirection();

            List<ContactInfo> contacts = this._contactManager.GetSortedContacts(sortField, isAscending);

            this._view.DisplayContactsTable(contacts);
        }
    }
}