using System.Xml.Serialization;

namespace Assignments
{
    /// <summary>
    /// This is the Assignment 1: Basic Contact Manager Console Application
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This is the main entry point of the application. 
        /// </summary>
        /// <param name="args">Optinal Arguments</param>
        public static void Main(string[] args)
        {
            int choice = 0;
            List<Contact> contacts = new List<Contact>();
            
            Console.WriteLine("BASIC CONTACT MANAGER CONSOLE APPLICATION");
            try
            {
                do
                {
                    Console.WriteLine("\nOperations Available on Contact Information: ");
                    Console.WriteLine("1. Add   2. View   3. Edit   4. Delete   5. Search   6. Sort   7. Exit\n");
                    Console.Write("Choose a functionality to continue: ");

                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1: AddContacts(contacts); break;
                        case 2: ViewContacts(contacts); break;
                        case 3: EditContacts(contacts); break;
                        case 4: DeleteContacts(contacts); break;
                        case 5: SearchContacts(contacts); break;
                        case 6: SortContacts(contacts); break;
                        case 7: break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                while (choice != 7);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Add new contacts to the list based on user input.
        /// </summary>
        /// <param name="contacts">A list of contacts defined in Main, used here to add additional contacts.</param>
        private static void AddContacts(List<Contact> contacts)
        {
            Console.Write("Enter the number of Contacts to be added : ");
            int numberOfContacts = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfContacts; i++)
            {
                Console.WriteLine($"\nEnter details for Contact {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Additional Notes: ");
                string additionalNotes = Console.ReadLine();
                Contact contact = new Contact
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    AdditionalNotes = additionalNotes
                };
                contacts.Add(contact);
                Console.WriteLine("Contact Added Successfully.");
            }
        }
        /// <summary>
        /// View the details of a particular contact or all contacts based on user input.
        /// </summary>
        /// <param name="contacts">A list of contacts defined in Main, used here to view the contact details.</param>
        private static void ViewContacts(List<Contact> contacts)
        {
            Console.WriteLine("\n1. View a particular contact\n2. View all Contacts");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the name of the contact to view: ");
                    string nameToView = Console.ReadLine();
                    foreach (var contact in contacts)
                    {
                        if (contact.Name.Equals(nameToView))
                        {
                            Console.WriteLine($"Name: {contact.Name}");
                            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                            Console.WriteLine($"Email: {contact.Email}");
                            Console.WriteLine($"Additional Notes: {contact.AdditionalNotes}");
                            return;
                        }
                    }
                    Console.WriteLine("Contact not found.");

                    break;
                case 2:
                    Console.WriteLine("\nAll Contacts:");
                    foreach (var contact in contacts)
                    {
                        Console.WriteLine($"Name: {contact.Name}");
                        Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                        Console.WriteLine($"Email: {contact.Email}");
                        Console.WriteLine($"Additional Notes: {contact.AdditionalNotes}");
                        Console.WriteLine();
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        /// <summary>
        /// Edits the details of an existing contact based on user input.
        /// </summary>
        /// <param name="contacts">A list of contacts defined in Main,used here to search for the contact to edit.</param>
        private static void EditContacts(List<Contact> contacts)
        {
            Console.Write("Enter the name of the contact to edit: ");
            string nameToEdit = Console.ReadLine();
            foreach (var contact in contacts)
            {
                if (contact.Name.Equals(nameToEdit))
                {
                    Console.WriteLine($"Editing Contact: {contact.Name}");

                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Phone Number: ");
                    string phoneNumber = Console.ReadLine();

                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    Console.Write("Additional Notes: ");
                    string additionalNotes = Console.ReadLine();

                    contact.Name = name;
                    contact.PhoneNumber = phoneNumber;
                    contact.Email = email;
                    contact.AdditionalNotes = additionalNotes;

                    return;
                }
            }
        }
        /// <summary>
        /// Delete the contact based on the name provided by the user. 
        /// If the contact is found, it will be removed from the list; otherwise, a message indicating that the contact was not found will be displayed.
        /// </summary>
        /// <param name="contacts">A list of Contacts defined in Main, used here to delete a contact.</param>
        private static void DeleteContacts(List<Contact> contacts)
        {
            Console.Write("Enter the name of the Contact to be deleted : ");
            string nameToDelete = Console.ReadLine();
            foreach (var contact in contacts)
            {
                if (contact.Name.Equals(nameToDelete))
                { 
                    contacts.Remove(contact);
                    Console.WriteLine($"Contact {nameToDelete} deleted successfully.\n");
                    return;
                }
            }
            Console.WriteLine("The Contact is not found.\n");
        }
        /// <summary>
        /// Users should be able to search for contacts based on their names or other relevant details. 
        /// The search functionality should display the matching contacts or indicate if no results are found. 
        /// </summary>
        /// <param name="contacts">Contacts Reference Type defined in Main, used here to Search in the list.</param>
        private static void SearchContacts(List<Contact> contacts)
        {
            Console.WriteLine("Enter the parameter for searching : ");
            Console.WriteLine("[N]ame\n[P]hone Number\n[E]mail Address");
            char searchParameter = char.Parse(Console.ReadLine());

            foreach (var contact in contacts)
            {
                switch (searchParameter)
                {
                    case 'N':
                        Console.WriteLine("Enter the name to be searched : ");
                        string nameToSearch = Console.ReadLine();

                        if (contact.Name.Equals(nameToSearch))
                        {
                            Console.WriteLine($"Name: {contact.Name}");
                            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                            Console.WriteLine($"Email: {contact.Email}");
                            Console.WriteLine($"Additional Notes: {contact.AdditionalNotes}");
                        }
                        break;
                    case 'P':
                        Console.WriteLine("Enter the phone number to be searched : ");
                        string phoneNumberToSearch = Console.ReadLine();

                        if (contact.PhoneNumber.Equals(phoneNumberToSearch))
                        {
                            Console.WriteLine($"Name: {contact.Name}");
                            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                            Console.WriteLine($"Email: {contact.Email}");
                            Console.WriteLine($"Additional Notes: {contact.AdditionalNotes}");
                        }
                        break;
                    case 'E':
                        Console.WriteLine("Enter the Email Address to be searched : ");
                        string emailToSearch = Console.ReadLine();

                        if (contact.PhoneNumber.Equals(emailToSearch))
                        {
                            Console.WriteLine($"Name: {contact.Name}");
                            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                            Console.WriteLine($"Email: {contact.Email}");
                            Console.WriteLine($"Additional Notes: {contact.AdditionalNotes}");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        /// <summary>
        /// sorts the contacts based on user choice of sorting parameter (name, phone number, or email).
        /// </summary>
        /// <param name="contacts">A list of contacts defined in Main, used here to sort the contacts.</param>
        private static void SortContacts(List<Contact> contacts)
        {
            Console.WriteLine("Sort Contacts by:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Phone Number");
            Console.WriteLine("3. Email");
            int sortChoice = int.Parse(Console.ReadLine());
            switch (sortChoice)
            {
                case 1: contacts.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));  break;
                case 2: contacts.Sort((c1, c2) => c1.PhoneNumber.CompareTo(c2.PhoneNumber)); break;
                case 3: contacts.Sort((c1, c2) => c1.Email.CompareTo(c2.Email)); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
            Console.WriteLine("\nContacts Sorted Successfully.");
        }
        /// <summary>
        /// defines the structure of a contact with properties for name, phone number, email, and additional notes.
        /// </summary>
        private class Contact
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string AdditionalNotes { get; set; }
        }
    }
}