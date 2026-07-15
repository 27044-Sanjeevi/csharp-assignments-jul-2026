namespace BasicContactManagerAssignment1.Services
{
    using System;
    using System.Collections.Generic;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Persistence;

    /// <summary>
    /// Provides business operations for managing contacts.
    /// </summary>
    internal class ContactManager
    {
        private readonly Repository _repository;
        private readonly ContactValidator _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactManager"/> class.
        /// </summary>
        /// <param name="repository">The contact repository.</param>
        /// <param name="validator">The contact validator.</param>
        /// <exception cref="ArgumentNullException">Thrown when repository or validator is null.</exception>
        public ContactManager(Repository repository, ContactValidator validator)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Validates and adds a new contact.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        public void AddContact(ContactInfo contact)
        {
            this._validator.Validate(contact);

            // Clone contact to store a copy, ensuring isolation of the stored state
            ContactInfo storedContact = this.CloneContact(contact);
            storedContact.Id = Guid.NewGuid(); // Assign new ID upon addition
            this._repository.Add(storedContact);
        }

        /// <summary>
        /// Gets all contacts.
        /// </summary>
        /// <returns>A list of cloned contact objects.</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> originalList = this._repository.GetAll();
            List<ContactInfo> clonedList = new List<ContactInfo>();

            foreach (ContactInfo contact in originalList)
            {
                clonedList.Add(this.CloneContact(contact));
            }

            return clonedList;
        }

        /// <summary>
        /// Validates and updates an existing contact.
        /// </summary>
        /// <param name="contact">The contact with updated details.</param>
        public void UpdateContact(ContactInfo contact)
        {
            this._validator.Validate(contact);
            this._repository.Update(this.CloneContact(contact));
        }

        /// <summary>
        /// Deletes a contact by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the contact to delete.</param>
        public void DeleteContact(Guid id)
        {
            this._repository.Delete(id);
        }

        /// <summary>
        /// Searches for contacts matching the search term across Name, Phone, Email, or Notes.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        /// <returns>A list of matching contacts.</returns>
        public List<ContactInfo> SearchContacts(string searchTerm)
        {
            List<ContactInfo> results = new List<ContactInfo>();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return results;
            }

            string lowerSearchTerm = searchTerm.ToLowerInvariant();
            List<ContactInfo> allContacts = this._repository.GetAll();

            foreach (ContactInfo contact in allContacts)
            {
                bool matches = false;

                if (contact.Name != null && contact.Name.ToLowerInvariant().Contains(lowerSearchTerm))
                {
                    matches = true;
                }
                else if (contact.Phone != null && contact.Phone.ToLowerInvariant().Contains(lowerSearchTerm))
                {
                    matches = true;
                }
                else if (contact.Email != null && contact.Email.ToLowerInvariant().Contains(lowerSearchTerm))
                {
                    matches = true;
                }
                else if (contact.Notes != null && contact.Notes.ToLowerInvariant().Contains(lowerSearchTerm))
                {
                    matches = true;
                }

                if (matches)
                {
                    results.Add(this.CloneContact(contact));
                }
            }

            return results;
        }

        /// <summary>
        /// Gets all contacts sorted by the specified criteria and direction.
        /// </summary>
        /// <param name="sortBy">The field to sort by (e.g., "name", "phone", "email").</param>
        /// <param name="choice">True to sort in ascending order; false for descending.</param>
        /// <returns>A sorted list of contacts.</returns>
        public List<ContactInfo> GetSortedContacts(string sortBy, bool choice)
        {
            List<ContactInfo> contacts = this.GetAllContacts();

            contacts.Sort((x, y) =>
            {
                string valueX = string.Empty;
                string valueY = string.Empty;

                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    valueX = x.Name ?? string.Empty;
                    valueY = y.Name ?? string.Empty;
                }
                else if (sortBy.Equals("phone", StringComparison.OrdinalIgnoreCase))
                {
                    valueX = x.Phone ?? string.Empty;
                    valueY = y.Phone ?? string.Empty;
                }
                else if (sortBy.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    valueX = x.Email ?? string.Empty;
                    valueY = y.Email ?? string.Empty;
                }

                int result = string.Compare(valueX, valueY, StringComparison.OrdinalIgnoreCase);

                return choice ? result : -result;
            });

            return contacts;
        }
        /// <summary>
        /// Creates a deep copy of a contact info object.
        /// </summary>
        /// <param name="source">The source contact to copy.</param>
        /// <returns>A new contact info instance with copied details.</returns>
        private ContactInfo CloneContact(ContactInfo source)
        {
            return new ContactInfo
            {
                Id = source.Id,
                Name = source.Name,
                Phone = source.Phone,
                Email = source.Email,
                Notes = source.Notes,
            };
        }
    }
}
