# SU.Group2

## Project Overview
The SU.Group2 project is the outcome of **business analysis**, **data modeling**, and a series of **customer meetings**. The development process has been carefully crafted to meet business requirements and provide a solution using technologies and design patterns learned from previous development courses.

## Architecture
The solution follows a **multi-layered architecture**, separating concerns between **Frontend** and **Backend** layers, ensuring maintainability and scalability.

### Backend
- **Framework**: .NET 6.0 (_enforced by course_)
- The backend is responsible for processing business logic and managing data persistence.
- Communication between different layers of the backend follows a clean architecture with:
  - **Controllers** to handle requests from the frontend (WPF).
  - **Services** to process business logic. These services are injected into controllers using **dependency injection**, which promotes loose coupling, testability, and maintainability. Services are registered in the **DIConfiguration** class, ensuring that dependencies are centrally managed.
  - **Unit of Work (UoW)** pattern to group multiple repository operations within a transaction.
  - **Repository Pattern** for handling database operations. We use both **generic repositories** for common CRUD operations and **specific repositories** (e.g., `EmployeeRepository`) for entity-specific operations, allowing flexibility while reducing code duplication.
  - **DbContext** and **Entity Framework (EF)** for interacting with the underlying SQL database (School DB).
  - **Dependency Injection** is used to inject interfaces into controllers and services, ensuring that services can easily be swapped or mocked during testing, improving code modularity.

### Frontend
- **Framework**: .NET 6.0 (WPF)
- **Design Pattern**: MVVM (Model-View-ViewModel)
- The frontend is implemented using **Windows Presentation Foundation (WPF)**, providing a rich user interface that interacts with the backend through controllers.
- **MVVM** separates the presentation logic (View) from the backend, where the ViewModel facilitates interaction between the UI and backend services.


## Technology Stack
- **Backend**: .NET 6.0, Entity Framework Core
- **Frontend**: WPF, MVVM
- **Database**: SQL (School DB)


## Backend Process Overview

### Login Flow

1. **Employee Input (Front-end - WPF)**
   - The process starts when the employee (user) inputs their **username** and **password** into the front-end WPF application.

2. **Controller Handling**
   - The **Controller** manages communication with the front-end, specifically collecting user input for login.
   - The Controller contains an instance of the `LoginService`, which handles the business logic for authentication.

3. **LoginService Logic**
   - The **LoginService** uses an instance of the`UnitOfWork` class to manage database transactions.
   - **UnitOfWork** includes a DB context and repositories that represent tables in the database.

4. **Repository Access**
   - The **LoginService** requests the `EmployeeRepository` from the **UnitOfWork**.
   - The **EmployeeRepository** includes both generic and specific methods, with specific methods used to verify if a user exists with the provided credentials.

5. **Authentication Response**
   - The **LoginService** either returns a valid user or `null` depending on the credentials provided.
   - It then returns a response including a boolean value, a response message, and the user object if the credentials are correct.

6. **Controller Response**
   - The **Controller** takes the response from **LoginService** and provides an appropriate message to the front-end.

![image](https://github.com/user-attachments/assets/f7aa07a0-7d91-4510-94c6-ba3b0f963b10)
