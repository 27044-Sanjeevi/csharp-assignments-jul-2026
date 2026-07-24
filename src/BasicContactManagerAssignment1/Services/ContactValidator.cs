namespace BasicContactManagerAssignment1.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using BasicContactManagerAssignment1.Models;
    using BasicContactManagerAssignment1.Persistence;

    /// <summary>
    /// Provides methods for validating contact information against system business rules.
    /// </summary>
    internal class ContactValidator
    {
        private const int MaxNotesLengthAllowed = 1000;

        private static readonly Regex PhoneStructureRegex = new (@"^\+?[0-9\s\-]{7,20}$", RegexOptions.Compiled);

        private static readonly EmailAddressAttribute FrameworkEmailValidator = new ();

        private readonly Repository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactValidator"/> class with a reference to the contact repository.
        /// </summary>
        /// <param name="repository">Provides the repository from persistence</param>
        /// <exception cref="ArgumentNullException">Throws exception when Argument is null</exception>
        public ContactValidator(Repository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository), "Cannot process validation routines on a null reference.");
        }

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
            ValidatePhone(contact.PhoneNumber);
            ValidateUniquePhone(contact.PhoneNumber, this._repository);
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
            if (!ValidateOptionalString(phone, nameof(phone)))
            {
                return;
            }

            if (!PhoneStructureRegex.IsMatch(phone!) || GetNumericDigitCount(phone!) < 7)
            {
                throw new ArgumentException("Phone number must contain at least 7 numeric digits and use valid formatting symbols.", nameof(phone));
            }
        }

        /// <summary>
        /// Validates that the phone number is unique across all existing contacts in the repository.
        /// </summary>
        /// <param name="phone">Phone number to be checked in repository</param>
        /// <param name="repository">Repository object for validation</param>
        /// <exception cref="ArgumentException">Throws when argument exception occurs</exception>
        private static void ValidateUniquePhone(string? phone, Repository repository)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return;
            }

            foreach (var contact in repository.GetAll())
            {
                if (contact.PhoneNumber == phone)
                {
                    throw new ArgumentException("Phone number must be unique. This phone number is already in use.", nameof(phone));
                }
            }
        }

        /// <summary>
        /// Validates the email address layout using native .NET framework.
        /// </summary>
        private static void ValidateEmail(string? email)
        {
            if (!ValidateOptionalString(email, nameof(email)))
            {
                return;
            }

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

        /// <summary>
        /// Method to validate optional string fields, ensuring they are not empty or whitespace only.
        /// </summary>
        /// <param name="value">Value to be validated></param>
        /// <param name="propertyName">Name of the Property</param>
        /// <returns>True if validation is correct else false</returns>
        private static bool ValidateOptionalString(string? value, string propertyName)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            ValidateWhitespaceOnly(value, propertyName);
            return true;
        }
    }
}
