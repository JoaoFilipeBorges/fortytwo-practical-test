# Fortytwo Software Engineer Practical Test

Welcome to the technical challenge!

The goal of this exercise is to evaluate your ability to design and implement a small but well-structured .NET application following **Clean Architecture** principles.

We are not looking for a "perfect" solution, but for clean code, good design decisions, and pragmatic engineering.

## Project Setup

The repository already contains the following empty projects:

- Fortytwo.PracticalTest
  - Fortytwo.PracticalTest.API
  - Fortytwo.PracticalTest.Application
  - Fortytwo.PracticalTest.Domain
  - Fortytwo.PracticalTest.Infrastructure

Projects are **not yet referenced** between each other. It is up to you to wire them correctly and respect the architectural
boundaries.

## Functional Requirements
1. **Authentication**
  - Implement JWT-based authentication.
  - For simplicity, you can keep one hardcoded user in memory (`admin:password`).
  - Endpoints must be protected.

2. **CRUD API**
  - Entity: you may choose something simple (e.g. `Todo`, `Product`,`Customer`).
  - Endpoints required:
    - `GET /todos/{id}`
    - `GET /todos`
    - `POST /todos`
  - The `POST` should insert into either:
    - An in-memory list, or a lightweight local database (SQLite, for example).

3. **External Service Integration**
   -   Consume a public API, for example: `https://jsonplaceholder.typicode.com/todos/{id}`
   -   Extend your `GET /todos/{id}` response with an additional property `externalTitle` coming from the external service.

## Non-Functional Requirements
- Follow **Clean Architecture** principles.
- Use **Dependency Injection** to wire dependencies.
- Implement **Repository Pattern** (or another suitable pattern) for data access.
- Add at least **1 ~ 2 unit tests** in the Api, Application or Domain layer.
- Validate input for `POST`.
- Provide API documentation with **Swagger/OpenAPI**.

## Constraints & Hints
- Do not use scaffolding or project templates that implement everything for you (e.g., EF Core Identity templates).
- Do not merge all logic into the API project (keep layers separated).
- Feel free to use modern libraries you normally would in production.
- If any requirement is unclear, make reasonable assumptions and document them in the README.

## Bonus (Optional, counts extra points)
- Docker image
- Configure **GitHub Actions** for CI/CD:
  - `dotnet restore`
  - `dotnet build`
  - `dotnet test`

## Delivery
- Fork this repository or create a private repo and share access.
- Provide clear instructions to run the project (if any).
- Commit history matters: incremental commits are better than one big commit.

## Evaluation Criteria

We will look at:

- Architecture & project organization.
- Code readability and maintainability.
- Proper use of design patterns and best practices.
- Error handling, validation.
- Test coverage.
- Bonus: CI/CD pipeline configuration.

Good luck, and have fun coding!