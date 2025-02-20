📚 The Reading Room – A Digital Hub for Book Lovers 📖✨
Welcome to The Reading Room! This project is a full-stack web application for book enthusiasts, where users can manage books, authors, and publishers in a structured digital library.

🚀 Built with:
✅ C# .NET (ASP.NET Core MVC + Entity Framework Core)
✅ SQL Server (Database)
✅ Identity Authentication (User Registration & Login)
✅ Swagger API Documentation
✅ Bootstrap + FontAwesome for UI Styling

📌 Project Features
📖 Books
Add, Edit, Delete, and View Books
Assign Books to Authors & Publishers
List All Available Books
✍️ Authors
Manage Author Information
Link/Unlink Authors to Books
View Details of an Author and Their Books
🏢 Publishers
Manage Publisher Details
View Publishers and Their Books
Assign Books to Publishers
🔐 User Authentication
Register & Login using ASP.NET Identity
Secure Actions (Only Logged-in Users can Add/Edit/Delete)
📜 API Documentation (Swagger)
Access API endpoints via https://localhost:7298/swagger
Supports JWT Authentication for secure API testing
🚀 Getting Started
🔹 1. Clone the Repository

git clone https://github.com/yourusername/TheReadingRoom.git
cd TheReadingRoom
🔹 2. Configure the Database
Modify appsettings.json and set up your SQL Server Connection String:

json
Copy
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TheReadingRoomDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
📍 Replace YOUR_SERVER with your actual SQL Server instance.

🔹 3. Apply Database Migrations
Run these commands in the terminal:


add-migration
update-database
✅ This creates the necessary tables in the database.

🔹 4. Run the Application

Open a browser and go to https://localhost:7298
Access Swagger API Docs: https://localhost:7298/swagger
🔧 Project Structure
scss
Copy
📦 TheReadingRoom
 ┣ 📂 AdilBooks
 ┃ ┣ 📂 Controllers
 ┃ ┃ ┣ 📜 BooksPageController.cs
 ┃ ┃ ┣ 📜 AuthorsPageController.cs
 ┃ ┃ ┣ 📜 PublishersPageController.cs
 ┃ ┣ 📂 Models
 ┃ ┃ ┣ 📜 Book.cs
 ┃ ┃ ┣ 📜 Author.cs
 ┃ ┃ ┣ 📜 Publisher.cs
 ┃ ┃ ┣ 📜 DTOs (Data Transfer Objects)
 ┃ ┣ 📂 Services
 ┃ ┃ ┣ 📜 BookService.cs
 ┃ ┃ ┣ 📜 AuthorService.cs
 ┃ ┃ ┣ 📜 PublisherService.cs
 ┃ ┣ 📂 Views
 ┃ ┃ ┣ 📂 BooksPage
 ┃ ┃ ┣ 📂 AuthorsPage
 ┃ ┃ ┣ 📂 PublishersPage
 ┃ ┃ ┣ 📜 _Layout.cshtml (Main UI Layout)
 ┃ ┣ 📜 Program.cs (Entry Point)
 ┣ 📜 README.md
 ┣ 📜 .gitignore
 ┣ 📜 TheReadingRoom.sln (Solution File)
⚡ Routes & Endpoints
📖 Books
Method	Route	Description
GET	/Books/List	View all books
GET	/Books/Find/{id}	Get details of a book
POST	/Books/Add	Add a new book
POST	/Books/Update	Update book details
POST	/Books/Delete/{id}	Delete a book
✍️ Authors
Method	Route	Description
GET	/Authors/List	View all authors
GET	/Authors/Find/{id}	Get details of an author
POST	/Authors/Add	Add a new author
POST	/Authors/Update	Update author details
POST	/Authors/Delete/{id}	Delete an author
🏢 Publishers
Method	Route	Description
GET	/Publishers/List	View all publishers
GET	/Publishers/Find/{id}	Get details of a publisher
POST	/Publishers/Add	Add a new publisher
POST	/Publishers/Update	Update publisher details
POST	/Publishers/Delete/{id}	Delete a publisher
🔑 Authentication & Authorization
Only logged-in users can perform Add, Edit, and Delete actions.
Authentication is handled via ASP.NET Identity.
Logout, Login, and Register buttons are available in the Navigation Bar.
🔹 Default User Roles (Can be extended later):

Admin (Full Access)
User (Can only view content)
🚀 Deployment
🔹 Deploy to Azure or Any Hosting
Publish the project:
dotnet publish -c Release
Deploy to a Web Server or Azure:
Deploy using IIS, Docker, or Azure Web Services.
💡 Contributing
Contributions are welcome! 🎉

Fork the repository
Create a new branch (feature-xyz)
Commit your changes
Open a pull request
🛠 Troubleshooting
❌ Swagger Not Loading?

Then visit:
https://localhost:7298/swagger/index.html
❌ Database Issues?


📜 License
None.


🌟 Thank You for Using The Reading Room!
📚 Happy Reading! 🚀📖
