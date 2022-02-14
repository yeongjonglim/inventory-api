# Getting started
If you are creating migrations locally, do disable InMemoryDatabase, as migrations do not work with InMemoryDatabase.

To create migrations:<br>
```
dotnet ef migrations add InitialCreate -o Persistence/Migrations --project src/Infrastructure --startup-project src/WebAPI
```