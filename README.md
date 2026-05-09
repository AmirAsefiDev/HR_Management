# 🏢 HR Leave Management System API

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-Success?style=for-the-badge)

## 📖 Overview
A highly scalable and robust RESTful API designed to streamline organizational leave management. This system acts as a reliable bridge between employees and the HR department, simplifying the process of submitting, reviewing, and managing leave requests. 

Built with **Clean Architecture** principles, this project prioritizes code readability, maintainability, and seamless future scalability.

## ✨ Key Features
- **👥 Employee & HR Workflow:** Engineered core workflows allowing employees to easily submit leave requests, while HR can seamlessly review, approve, or reject them.
- **🔐 Secure Access (JWT & RBAC):** Secured the application using JSON Web Tokens (JWT) and implemented Role-Based Access Control (RBAC) to distinguish between regular employees, HR personnel, and administrators for advanced reporting.
- **✉️ Automated Notifications:** Integrated an automated email notification system to instantly alert users of any updates or changes to their leave request status.
- **⚙️ High Performance & Reliability:** Optimized data operations utilizing the **CQRS** pattern to separate read and write logic, ensuring high performance.

## 🛠️ Architecture & Technologies
This project leverages modern .NET ecosystem tools and architectural best practices:

*   **Architecture:** Clean Architecture
*   **Design Patterns:** CQRS (Command Query Responsibility Segregation)
*   **Security:** JWT Authentication, Role-Based Access Control (RBAC)
*   **Validation:** FluentValidation for robust data integrity
*   **Testing:** Comprehensive Unit Testing to ensure system reliability
*   **API Design:** RESTful API principles

## 🏗️ Project Structure (Clean Architecture)
*   **Domain Layer:** Enterprise logic and core entities.
*   **Application Layer:** Business logic, Interfaces, CQRS Handlers, and FluentValidation rules.
*   **Infrastructure Layer:** External services (e.g., Email Notification system).
*   **Persistence Layer:** Database context and migrations.
*   **Presentation Layer (API):** Controllers and API endpoints.

## 🚀 Getting Started
*(Note: Add instructions here on how to run your project locally. Example below:)*
1. Clone the repository: `git clone https://github.com/AmirAsefiDev/HR_Management.git`
2. Update the database connection string in `appsettings.json`.
3. Run Entity Framework migrations to update the database.
4. Run the project using Visual Studio or the .NET CLI: `dotnet run`
