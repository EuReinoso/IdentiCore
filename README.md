# IdentiCore (.NET 7 + SQL Server + JWT Authentication)

This is a multi-project solution built using **ASP.NET Core 7**, featuring:

- **RESTful API** with JWT authentication
- **MVC Web Application** frontend
- **SQL Server** database
- **DDD** architecture with clean separation of concerns (Application, Domain, Infrastructure, Web, API)

---

## :file_folder: Project Structure

```text
Solution/
│
├── API/                      # RESTful Web API with JWT auth
│   ├── Controllers/          # API Controllers
│   └── Program.cs
│
├── Application/              # Application layer with DTOs and Services
│   ├── DTOs/
│   └── Services/
│
├── Domain/                   # Domain models and interfaces
│   ├── Entities/             # Database Entities (Tables)
│   └── Interfaces/           # Interfaces
│
├── Infrastructure/           # Repositories and DbContext
│   ├── Persistence/          # EF Core DbContext
│   └── Repositories/         # Repositories and Queryes
│
├── Web/                      # ASP.NET MVC frontend that consumes the API
│   ├── Controllers/          # MVC Controllers
│   ├── Views/                # Razor views
│   ├── Models/               # MVC ViewModels
│   ├── Integration/          # HTTP client + Auth handler
│   └── Program.cs
│
└── README.md                 # This file
```

## ⚙️ Technologies

- .NET 7
- SQL Server
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- Dependency Injection

---

## :rocket: Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/EuReinoso/IdentiCore.git
```

### 2. Configure the database
Make sure you have SQL Server running.

```bash
cd IdentiCore
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API
```

### 3. Run
- Run the API project (BackEnd)
- Run the Web Project (FrontEnd)

## :green_heart: Features
- Create, update, delete clients
- Add multiple addresses per client
- JWT authentication with token validation
- Modular solution structure
- Secure communication between frontend and API

## :hand: Thank you
- By Lucas Reinoso
