# AgriEnergyConnect

AgriEnergyConnect is a web-based platform designed to connect agricultural stakeholders with energy solutions. This README provides comprehensive information about setting up, building, and using the system.

## Table of Contents
- [Prerequisites](#prerequisites)
- [Development Environment Setup](#development-environment-setup)
- [Building and Running the Application](#building-and-running-the-application)
- [System Functionality](#system-functionality)
- [User Roles](#user-roles)
- [Database](#database)
- [References](#references)
- [GitHub Repo](#github-repo)

## Prerequisites

Before you begin, ensure you have the following installed:
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- Git
- SQLite (included with the project)

## Development Environment Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Younous87/PROG7311_POE.git
   cd PROG7311_POE
   ```

2. **Install Dependencies**
   - The project uses NuGet packages which will be automatically restored when you build the solution
   - Required packages:
     - Microsoft.Data.Sqlite.Core 
     - Microsoft.EntityFrameworkCore 
     - Microsoft.EntityFrameworkCore.Sqlite 
     - System.Data.SQLite 

3. **Database Setup**
   - The application uses SQLite database
   - The database file (agriEnergyConnect.db) is included in the project
   - No additional database setup is required

## Building and Running the Application

### Using Visual Studio
1. Open `PROG7311_POE.sln` in Visual Studio
2. Press F5 or click the "Run" button to start the application
3. The application will launch in your default web browser

### Using Command Line
1. Navigate to the project directory
2. Run the following commands:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
3. The application will be available at `https://localhost:5001` or `http://localhost:5000`

## System Functionality

The AgriEnergyConnect platform provides the following key features:

1. **User Management**
   - User registration and authentication
   - Profile management
   - Role-based access control

2. **Energy Solutions**
   - Browse available energy solutions
   - View detailed information about each solution

3. **Agricultural Services**
   - Access agricultural resources
   - Connect with energy providers


## User Roles

The system supports multiple user roles, each with specific permissions and capabilities:

1. **Employees**
   - Add new Farmers
   - View all products
   - Filter products

2. **Farmers**
   - Browse energy solutions
   - View profile
   - Access and edit own products

3. **Guest User**
   - View listed products
   - Basic browsing capabilities

## Database

The application uses SQLite as its database system:
- Database file: `agriEnergyConnect.db`
- Located in the project root directory
- Automatically configured 
- No manual database setup required

## References 

https://www.w3schools.com/html/

https://www.w3schools.com/css/default.asp

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-9.0

https://www.youtube.com/watch?v=7TpljNN0IvA

https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/?view=aspnetcore-9.

## GitHub Repo

https://github.com/Younous87/PROG7311_POE.git