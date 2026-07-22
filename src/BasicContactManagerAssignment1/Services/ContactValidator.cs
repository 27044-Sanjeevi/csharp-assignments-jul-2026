namespace BasicContactManagerAssignment1.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using BasicContactManagerAssignment1.Models;

    /// <summary>
    /// Provides methods for validating contact information against system business rules.
    /// </summary>
    internal class ContactValidator
    {
        private const int MaxNotesLengthAllowed = 1000;

        private static readonly Regex PhoneStructureRegex = new (@"^\+?[0-9\s\-]{7,20}$", RegexOptions.Compiled);

        private static readonly EmailAddressAttribute FrameworkEmailValidator = new ();

        /// <summary>
        /// Validates a contact info object against business rules.
        /// </summary>
        /// <param name="contact">The contact information profile to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when contact reference parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when properties contain structurally invalid data.</exception>
        public void ValidateOrThrow(ContactInfo contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact), "Cannot process validation routines on a null reference.");
            }

            ValidateName(contact.Name);
            ValidatePhone(contact.Phone);
            ValidateEmail(contact.Email);
            ValidateNotes(contact.Notes);
        }

        /// <summary>
        /// Validates that the mandatory name component.
        /// </summary>
        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Contact name is a mandatory field and cannot be empty or whitespace.", nameof(name));
            }
        }

        /// <summary>
        /// Validates the arrangement and numerical count of the phone string.
        /// </summary>
        private static void ValidatePhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return;
            }

            ValidateWhitespaceOnly(phone, nameof(phone));

            if (!PhoneStructureRegex.IsMatch(phone) || GetNumericDigitCount(phone) < 7)
            {
                throw new ArgumentException("Phone number must contain at least 7 numeric digits and use valid formatting symbols.", nameof(phone));
            }
        }

        /// <summary>
        /// Validates the email address layout using native .NET framework.
        /// </summary>
        private static void ValidateEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return;
            }

            ValidateWhitespaceOnly(email, nameof(email));

            if (!FrameworkEmailValidator.IsValid(email))
            {
                throw new ArgumentException("Provided email address does not conform to a valid routing schema (e.g., user@domain.com).", nameof(email));
            }
        }

        /// <summary>
        /// Validtes the notes from the user
        /// </summary>
        private static void ValidateNotes(string? notes)
        {
            if (string.IsNullOrEmpty(notes))
            {
                return;
            }

            if (notes.Length > MaxNotesLengthAllowed)
            {
                throw new ArgumentException($"Notes cannot exceed the maximum of {MaxNotesLengthAllowed} characters.", nameof(notes));
            }
        }

        /// <summary>
        /// Reusable check logic for ensuring string cannot consist purely of spaces.
        /// </summary>
        private static void ValidateWhitespaceOnly(string targetFieldValue, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(targetFieldValue))
            {
                throw new ArgumentException($"Field value for '{propertyName}' cannot consist solely of blank whitespace parameters.", propertyName);
            }
        }

        /// <summary>
        /// Reusable helper evaluating absolute count of raw integers inside a text pattern sequence.
        /// </summary>
        private static int GetNumericDigitCount(string textInput)
        {
            int numericCount = 0;
            foreach (char characterToken in textInput)
            {
                if (char.IsDigit(characterToken))
                {
                    numericCount++;
                }
            }

            return numericCount;
        }
    }
}
