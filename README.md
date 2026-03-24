# CleanAuth.CQRS.StarterKit

A lightweight and production-ready starter kit for ASP.NET Core Web API, built with **Clean Architecture** and **CQRS** principles.

## Features
* **CQRS Pattern**: Implementation using MediatR for clean separation of concerns.
* **JWT Authentication**: Secure Access and Refresh Token rotation.
* **Role-Based Access Control**: Easy authorization using roles (e.g., Admin, User).
* **Global Exception Handling**: Centralized middleware for consistent error responses.
* **Security**: Password hashing with BCrypt and integrated Swagger Bearer support.

## Architecture
The project is structured around the **CQRS** pattern:
* **Commands**: Logic for data modifications (Create, Update, Delete).
* **Queries**: Logic for data retrieval (Get, List).
* **Handlers**: Isolated business logic execution.



🛠 Setup & Installation
* Clone the repository.
* Update the connection string in appsettings.json.
* Run dotnet ef database update.
* Press F5 to start the project.
