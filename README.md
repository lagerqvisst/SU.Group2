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


## Example of Backend Process 
### Login Flow 
- Starts by employee _(user)_ inputs their credentials in the front-end _(WPF)_
![image](https://github.com/user-attachments/assets/f7aa07a0-7d91-4510-94c6-ba3b0f963b10)
