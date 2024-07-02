# library-app
Simple library app deveveloped in C# .net 8

User Story:
Trying to avoid big spending, a client asks to develop a prototype of a service to register books and users for his new business.
The client doesn't know well about technology and an analyst of requirements went to him to ask some questions to prospect more data that could help the development.
After some days, the analyst came back with the requirements, which are listed below.
- Users are those who interact directly with the system and these users will register books.
- The service should own an endpoint to register users.
- The service should own an endpoint to register books.
- Users should own a username and a password to handle books.
- Users registered shouldn't have the same user name, the service should validate it.
- Users should be authenticated to handle books in the system, and the service should validate it.
- Books should be registered with their title, author, and number register, service validate.
- All attributes from books should be informed.
- Books shouldn't own the same register number, service validate.

After a meeting with the development team, a NoSQL database for data persistence and a Jwt token for authentication was chosen.
The architecture should be clean and the code as well.
Repository and unit of work pattern should be used.
Implement request and response classes to the data that come in and leave the system over the API.
Finally, unit tests should be implemented.