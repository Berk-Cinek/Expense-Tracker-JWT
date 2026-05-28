# Expense Tracker API

A RESTful API for tracking personal expenses built with ASP.NET Core and Entity Framework Core.

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

## Features
- User registration and login
- JWT protected endpoints
- Create, read, update and delete expenses
- Filter expenses by date range (last week, last month, last 3 months, custom)

## Endpoints

### Auth
- `POST /api/user/register` - Register a new user
- `POST /api/user/login` - Login and receive a JWT token

### Expenses (require JWT)
- `GET /api/expense` - Get all expenses
- `GET /api/expense?filter=last_week` - Filter expenses
- `GET /api/expense/{id}` - Get expense by ID
- `POST /api/expense` - Add a new expense
- `PUT /api/expense/{id}` - Update an expense
- `DELETE /api/expense/{id}` - Delete an expense

## Setup
1. Clone the repo
2. Update the connection string in `appsettings.json`
3. Run `dotnet ef database update`
4. Run `dotnet run`
