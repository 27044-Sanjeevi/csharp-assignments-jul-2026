namespace BasicContactManagerAssignment1.Persistence
{
    using System;
    using System.Collections.Generic;
    using BasicContactManagerAssignment1.Models;

    /// <summary>
    /// Manages in-memory storage of contact data.
    /// </summary>
    internal class Repository
    {
        private readonly List<ContactInfo> _contacts = new List<ContactInfo>();

        /// <summary>
        /// Adds a new contact to the repository.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        public void Add(ContactInfo contact)
        {
            this._contacts.Add(contact);
        }

        /// <summary>
        /// get all contacts from the repository.
        /// </summary>
        /// <returns>A list of all contacts.</returns>
        public List<ContactInfo> GetAll()
        {
            // Return a copy of the list to prevent external modification of the internal collection
            return new List<ContactInfo>(this._contacts);
        }

        /// <summary>
        /// updates an existing contact in the repository.
        /// </summary>
        /// <param name="contact">The contact with updated details.</param>
        public void Update(ContactInfo contact)
        {
            for (int i = 0; i < this._contacts.Count; i++)
            {
                if (this._contacts[i].Id == contact.Id)
                {
                    this._contacts[i] = contact;
                    return;
                }
            }

            throw new KeyNotFoundException($"Contact with ID {contact.Id} not found.");
        }

        /// <summary>
        /// Deletes a contact from the repository by its unique identifier.
        /// </summary>
        /// <param name ="id">The unique identifier of the contact to delete.</param>
        public void Delete(Guid id)
        {
            for (int i = 0; i < this._contacts.Count; i++)
            {
                if (this._contacts[i].Id == id)
                {
                    this._contacts.RemoveAt(i);
                    return;
                }
            }

            throw new KeyNotFoundException($"Contact with ID {id} not found.");
        }
    }
}
