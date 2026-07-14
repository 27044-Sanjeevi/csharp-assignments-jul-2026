# Assignment - 1
## Basic Contact Manager Console Application
This is a Simple console-based contact manager application that allows users to store and manage their contacts
### Operations Available
The application provides the following operations with the help of a menu:
1. **Add a contact** : Allows the user to add a new contact to the contact list. The user will be prompted to enter the contact's name, phone number, and email address.

2. **View** : Displays the list of all contacts stored in the application. The user can view the contact details such as name, phone number, and email address of all contacts or either a single person.
3. **Edit** : Allows the user to edit the details of an existing contact. The user will be prompted to enter the contact's name, and then they can update the phone number and email address.  
4. **Delete** : Allows the user to delete a contact from the contact list. The user will be prompted to enter the contact's name, and then the contact will be removed from the list.
5. **Search** : Allows the user to search for a contact by name or phone number or email address. The user will be prompted to enter the contact's name, and if a match is found, the contact details will be displayed.
6. **Sort** : Allows the user to sort the contact list by name, phone number, or email address. The user will be prompted to choose the sorting criteria, and the contact list will be displayed in the selected order.
7. **Exit** : Allows the user to exit the application. The user will be prompted to confirm their choice before exiting.
### Error Handling
- The entire menu-driven application is enclosed within a try-catch block in the Main()
- Menu selections and other numeric inputs are validated using int.TryParse().
- While adding contacts, the number of contacts entered by the user is validated before
- The application validates user menu selections using a switch statement.