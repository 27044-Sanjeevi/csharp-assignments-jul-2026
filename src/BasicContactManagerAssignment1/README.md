# Basic Contact Manager
 
A simple, console-based application to help organize and manage your personal contacts efficiently. This allows you to store names, phone numbers, emails, and notes, with search and sorting capabilities.
 
## Features
 
*   **Add Contacts:** Store new contacts with a mandatory name and optional details like phone, email, and notes.
*   **View All Contacts:** See a clean, formatted table of all your saved contacts.
*   **Edit Contacts:** Update any existing contact's information.
*   **Delete Contacts:** Remove contacts you no longer need.
*   **Search:** Quickly find contacts by searching through their name, phone number, email, or notes.
*   **Sort:** Organize your contact list by Name, Phone, or Email in either Ascending or Descending order.

## Operations Available
 
### Adding a New Contact
1.  Select option **1** from the main menu.
2.  Enter the **Name** (this is required).
3.  Optionally enter a **Phone Number**, **Email Address**, and **Notes**.
4.  You will see a "Contact added successfully!" message upon completion.
 
### Viewing Contacts
*   Select option **2** to view all contacts in a formatted table.
*   If no contacts exist, the app will notify you that the list is empty.
 
### Editing a Contact
1.  Select option **3**.
2.  Choose the contact you wish to edit from the list by entering its index number.
3.  For each field (Name, Phone, etc.), you will see the current value.
    *   To **keep** the current value, just press `Enter`.
    *   To **update** it, type the new value and press `Enter`.
 
### Deleting a Contact
1.  Select option **4**.
2.  Choose the contact you wish to delete from the list.
3.  Confirm the deletion. The contact will be permanently removed from the list.
 
### Searching for Contacts
1.  Select option **5**.
2.  Type a keyword (e.g., "John", "555", or "@gmail").
3.  The app will display all contacts where the Name, Phone, Email, or Notes contain your keyword.
 
### Sorting Contacts
1.  Select option **6**.
2.  Choose the field to sort by: **Name**, **Phone**, or **Email**.
3.  Choose the direction: **Ascending** (A-Z) or **Descending** (Z-A).

## Troubleshooting
 
*   **"Invalid choice" error:** Ensure you are typing a number between 1 and 7 when selecting from the main menu.
*   **"Input cannot be empty":** The Name field is mandatory. Please provide at least one character for the name.