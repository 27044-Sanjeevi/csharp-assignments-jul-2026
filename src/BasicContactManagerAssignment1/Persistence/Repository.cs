namespace BasicContactManagerAssignment1.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        /// <returns>True when contact is updated false when contact is not found for updation.</returns>
        public bool Update(ContactInfo contact)
        {
            int index = this.GetIndexById(contact.Id);

            if (index != -1)
            {
                this._contacts[index] = contact;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes a contact from the repository by its unique identifier.
        /// </summary>
        /// <param name ="id">The unique identifier of the contact to delete.</param>
        /// <returns>True when contact is deleted, false if contact is not found for deletion.</returns>
        public bool Delete(Guid id)
        {
            int index = this.GetIndexById(id);

            if (index != -1)
            {
                this._contacts.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the index based on the Guid provided.
        /// </summary>
        /// <param name="id">Id to be serached in the list.</param>
        /// <returns>The index value of the element if found, else -1.</returns>
        private int GetIndexById(Guid id)
        {
            for (int i = 0; i < this._contacts.Count; i++)
            {
                if (this._contacts[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
