namespace BasicContactManagerAssignment1.Models
{
    using System;

    /// <summary>
    /// Contains the contact information of a person, including their name, email, phone number, and any additional notes.
    /// </summary>
    internal class ContactInfo
    {
        /// <summary>
        /// gets or sets the unique identifier for the contact information.
        /// </summary>
        /// <value>
        /// Unique identifier for the contact information, represented as a GUID
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// gets or sets the name of the contact person.
        /// </summary>
        /// <value>
        /// Name of the contact person, represented as a string.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// gets or sets the Email of the contact person.
        /// </summary>
        /// <value>
        /// Email of the contact person, represented as a string.
        /// </value>
        public string? Email { get; set; }

        /// <summary>
        /// gets or sets the Phone Number of the contact person.
        /// </summary>
        /// <value>
        /// Phone Number of the contact person, represented as a string.
        /// </value>
        public string? Phone { get; set; }

        /// <summary>
        /// gets or sets any additional notes of the contact person.
        /// </summary>
        /// <value>
        /// Additional Notes about the contact person, represented as a string.
        /// </value>
        public string? Notes { get; set; }
    }
}
