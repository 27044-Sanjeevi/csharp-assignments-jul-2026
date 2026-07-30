namespace BasicContactManagerAssignment1.Services
{
    using System;
    using System.Collections.Generic;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Persistence;
    using BasicContactManagerAssignment1.Validation;

    /// <summary>
    /// Represents the available fields that contacts can be sorted by.
    /// </summary>
    internal enum SortField
    {
        /// <summary>
        /// Sort contacts by name.
        /// </summary>
        Name = 1,

        /// <summary>
        /// Sort contacts by phone number.
        /// </summary>
        PhoneNumber = 2,

        /// <summary>
        /// Sort contacts by email address.
        /// </summary>
        Email = 3,
    }

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
            this._validator.ValidateOrThrow(contact);

            if (!string.IsNullOrEmpty(contact.PhoneNumber))
            {
                this.EnsureUniquePhoneNumber(contact.PhoneNumber, Guid.Empty);
            }

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
            this._validator.ValidateOrThrow(contact);
            if (!string.IsNullOrEmpty(contact.PhoneNumber))
            {
                this.EnsureUniquePhoneNumber(contact.PhoneNumber, contact.Id);
            }

            bool success = this._repository.Update(this.CloneContact(contact));
            if (!success)
            {
                throw new KeyNotFoundException($"Contact with ID {contact.Id} was not found.");
            }
        }

        /// <summary>
        /// Deletes a contact by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the contact to delete.</param>
        public void DeleteContact(Guid id)
        {
            bool success = this._repository.Delete(id);
            if (!success)
            {
                throw new KeyNotFoundException($"Contact with ID {id} was not found.");
            }
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
                if (this.IsMatch(contact, searchTerm))
                {
                    results.Add(this.CloneContact(contact));
                }
            }

            return results;
        }

        /// <summary>
        /// Gets all contacts sorted by the specified criteria and direction.
        /// </summary>
        /// <param name="sortField">The field to sort by (e.g., "name", "phone", "email").</param>
        /// <param name="isAscending">True to sort in ascending order; false for descending.</param>
        /// <returns>A sorted list of contacts.</returns>
        public List<ContactInfo> GetSortedContacts(SortField sortField, bool isAscending)
        {
            List<ContactInfo> contacts = this.GetAllContacts();

            contacts.Sort((x, y) =>
            {
                string valueX = this.GetValueForSort(x, sortField);
                string valueY = this.GetValueForSort(y, sortField);

                int result = string.Compare(valueX, valueY, StringComparison.OrdinalIgnoreCase);

                return isAscending ? result : -result;
            });

            return contacts;
        }

        private void EnsureUniquePhoneNumber(string phoneNumber, Guid currentContactId)
        {
            string normalized = phoneNumber.Trim();
            List<ContactInfo> allContacts = this._repository.GetAll();

            foreach (ContactInfo contact in allContacts)
            {
                // skip the current contact
                if (contact.Id == currentContactId)
                {
                    continue;
                }

                if (contact.PhoneNumber != null && contact.PhoneNumber == normalized)
                {
                    throw new ArgumentException($"A contact with phone number {phoneNumber} already exists.", nameof(phoneNumber));
                }
            }
        }

        /// <summary>
        /// Retrieves the value used for sorting from the specified contact based on the given field.
        /// </summary>
        /// <param name="contact">The contact information to evaluate.</param>
        /// <param name="field">The field indicating which contact property to use for sorting.</param>
        /// <returns>The value of the specified field, or an empty string if the field is not recognized.</returns>
        private string GetValueForSort(ContactInfo contact, SortField field)
        {
            return field switch
            {
                SortField.Name => contact.Name ?? string.Empty,
                SortField.PhoneNumber => contact.PhoneNumber ?? string.Empty,
                SortField.Email => contact.Email ?? string.Empty,
                _ => string.Empty, // for safety
            };
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
                PhoneNumber = source.PhoneNumber,
                Email = source.Email,
                Notes = source.Notes,
            };
        }

        /// <summary>
        /// Determines whether a contact matches the provided search term.
        /// </summary>
        /// <param name="contact">The contact to evaluate.</param>
        /// <param name="searchTerm">The search term.</param    >
        /// <returns>True if any field contains the search term; otherwise, false.</returns>
        private bool IsMatch(ContactInfo contact, string searchTerm)
        {
            string lowerSearchTerm = searchTerm.ToLowerInvariant();

            return (contact.Name != null && contact.Name.ToLowerInvariant().Contains(lowerSearchTerm)) ||
                   (contact.PhoneNumber != null && contact.PhoneNumber.ToLowerInvariant().Contains(lowerSearchTerm)) ||
                   (contact.Email != null && contact.Email.ToLowerInvariant().Contains(lowerSearchTerm)) ||
                   (contact.Notes != null && contact.Notes.ToLowerInvariant().Contains(lowerSearchTerm));
        }
    }
}
