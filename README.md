# Fortytwo Software Engineer Practical Test

## Developer Notes

### Running the app

**I was unable to conclude the docker image (problems with swagger), for this reason the app should be ran in the IDE.**

### Requirements

1. **Authentication**
- Implement JWT-based authentication.  ✅
- For simplicity, you can keep one hardcoded user in memory (`admin:password`). ✅
- Endpoints must be protected.  ✅

**Use the following: user:admin password:admin123!. The login endpoint will return a Token.**

2. **CRUD API**
- Entity: you may choose something simple (e.g. `Todo`, `Product`,`Customer`).
- Endpoints required:
    - `GET /todos/{id}`
    - `GET /todos`
    - `POST /todos`
- The `POST` should insert into either:
    - An in-memory list, or a lightweight local database (SQLite, for example).

✅
**Endpoints were implemented. Used EF Core with SQLite.**


3. **External Service Integration**
    -   Consume a public API, for example: `https://jsonplaceholder.typicode.com/todos/{id}`
    -   Extend your `GET /todos/{id}` response with an additional property `externalTitle` coming from the external service.

✅
**Implemented a custom HttpCLient in infrastructure to call the external API.**


## Bonus (Optional, counts extra points)
- Docker image ⚠️ Image was created but I was unable to figure out, in time, why swagger was not being loaded in the container.
- Configure **GitHub Actions** for CI/CD: ✅
  - `dotnet restore`
  - `dotnet build`
  - `dotnet test`
