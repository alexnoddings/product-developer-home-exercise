# UK Parliament - Product Team Home Exercise for Developer

## Dependencies
Please ensure you have the following installed:
* .NET 8 SDK (you may need to ensure your Visual Studio installation is fully up to date)

## Introduction

Thanks for doing our recruitment home exercise. We have written a template solution for you, to save you from having to do too much setup.

* Clone this repository, and open the solution
* Set **UKParliament.CodeTest.Web** as the startup project
* Build and run it.
* Instructions are including below with the assessment criteria for the test. **You should aim to satisfy all points on it.**

## Submitting your test
* After you have complete this test please host your solution on GitHub (or another git based hosting platform)
* Provide us with a link so we can clone your solution

## Test Instructions
### Objective
Develop a small .NET Core Web API application to manage a simple to-do list. The application should allow users to perform CRUD (Create, Read, Update, Delete) operations on to-do items.

### Requirements
Extend the ToDo Item entity in the data project, to include these properties:

* Id (Guid or int)
* Title (string, required, max length 100)
* Description (string, optional, max length 500)
* IsCompleted (bool)
* DueDate (DateTime, optional)
* (Any other properties that you would like to add)

Implement the following features on the API:

* Create a new to-do item.
* Retrieve a list of all to-do items.
* Retrieve a single to-do item by ID.
* Update an existing to-do item.
* Delete a to-do item.
* Mark a to-do item as completed.

Technical Requirements:

.NET Core 8 for the Web API.
Entity Framework Core (EF Core) for data access. An in-memory database or SQLite is acceptable.
RESTful API principles should be followed.
Unit Testing: Write unit tests for at least two key functionalities.
Error Handling: Implement basic error handling (e.g., handling not found items, validation errors).
Dependency Injection (DI) should be used to manage dependencies.
Clean Code principles should be adhered to.
Git: Use Git to track your progress. Provide a link to a Git repository containing your solution.
