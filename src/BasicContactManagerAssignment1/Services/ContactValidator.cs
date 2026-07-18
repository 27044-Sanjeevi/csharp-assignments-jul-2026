namespace BasicContactManagerAssignment1.Services
{
    using System;
    using BasicContactManagerAssignment1.Models;

    /// <summary>
    /// Provides methods for validating contact information.
    /// </summary>
    internal class ContactValidator
    {
        /// <summary>
        /// Validates a contact info object against business rules.
        /// </summary>
        /// <param name="contact">The contact information to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when contact is null.</exception>
        /// <exception cref="Exception">Thrown when validation fails.</exception>
        public void Validate(ContactInfo contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            // 1. Validate Name (Required)
            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                throw new Exception("Contact name is required and cannot be empty or whitespace.");
            }

            // 2. Validate Phone
            if (!string.IsNullOrEmpty(contact.Phone))
            {
                if (string.IsNullOrWhiteSpace(contact.Phone))
                {
                    throw new Exception("Phone number cannot consist of only whitespace.");
                }

                foreach (char c in contact.Phone)
                {
                    if (!char.IsDigit(c) && c != ' ' && c != '-' && c != '+')
                    {
                        throw new Exception(
                            "Phone number can only contain digits, spaces, dashes, or plus symbols.");
                    }
                }
            }

            // 3. Validate Email
            if (!string.IsNullOrEmpty(contact.Email))
            {
                if (string.IsNullOrWhiteSpace(contact.Email))
                {
                    throw new Exception("Email address cannot consist of only whitespace.");
                }

                if (!IsValidEmail(contact.Email))
                {
                    throw new Exception(
                        "Email address is not in a valid format (e.g., example@domain.com).");
                }
            }
        }

        private static bool IsValidEmail(string email)
        {
            int atIndex = email.IndexOf('@');

            // Must contain exactly one '@'
            if (atIndex <= 0 || atIndex != email.LastIndexOf('@'))
            {
                return false;
            }

            // Must have characters after '@'
            if (atIndex == email.Length - 1)
            {
                return false;
            }

            string domainPart = email.Substring(atIndex + 1);

            // Domain must contain a dot and not start/end with it
            int dotIndex = domainPart.IndexOf('.');
            if (dotIndex <= 0 || dotIndex == domainPart.Length - 1)
            {
                return false;
            }

            // No spaces allowed
            if (email.Contains(" "))
            {
                return false;
            }

            return true;
        }
    }
}