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

A test code template has been provided to save time on setting up the code project, it includes:

* WEB API Project: The beginning of an API - A Todo List API controller exists for you with a basic Swagger/OpenAPI documentation UI page.
* Services project - To contain any services you might require.
* Data project - Houses the in-memory database context with A todo entity. Todo items can be seeded into the DB on startup, if the relevant commented code line is uncommented.
* Tests project - For writing Unit tests.

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

* .NET Core 8 for the Web API.
* Entity Framework Core (EF Core) for data access. An in-memory database has already been setup in the project files.
* RESTful API principles should be followed.
* Unit Testing: Write unit tests for the Controller and Service classes.
* Error Handling: Implement basic error handling (e.g., handling not found items, validation errors).
* Dependency Injection (DI) should be used to manage dependencies.
* SOLID principles should be adhered to.
* Git: Use Git to track your progress. Provide a link to a Git repository (such as GitHub) containing your solution.

### Assessment criteria

Find the assessment criteria we will use to review your completed test below. You should aim to satisfy all the points on it.

| Assessment area | Assessment criteria |
| ----- | ------ |
|  SOLID principles | 
| Single responsibility principle |  You should structure your code so that each class has a single responsibility. Controllers should also be as lightweight as possible. For example. mapping from an entity to a view model should also be delegated to a separate mapping class. |
| Open-closed principle |  If you add any inheritance with base/derived classes (such as services or repositories), ensure that the inheritance it is architected so that new behaviour can be accommodated without needing to change the code of the base classes, will be flexible if the requirements change, and that the inheritance is not better architectured using abstraction or composition instead. |
|  Liskov substitution principle |   If you add any inheritance with base/derived classes, ensure that derived classes you add (such as services, repositories, mappers or validators) are correctly substitutable for their base classes, will be flexible if the requirements change, and that the inheritance is not better architectured using abstraction or composition instead. |
|  Interface segregation principle |  Classes in your code should only implement interfaces with methods that they actually use. For example, your To-do List services will return and save data. Interfaces used in your code should reflect this. |
| Dependency inversion principle |  You should abstract any dependencies in your back-end code into interfaces, inject them using dependency injection, and mock them where required in unit tests. For example, the services should have an abstraction of the data access code injected rather than a concrete implementation. |
|  Back-end |   
|  Architecture | You should expand upon the N-tier skeleton structure setup in the test template. Data access should use the repository pattern, which injects your database context and interacts with it. The services then use the repository to read/save data. Api Controllers then consume these services. Services and controllers should not use the the database context directly. |
|  Unit tests | Each class you add with public methods should have at least one unit test added to demonstrate how you would unit test that component. You should add at least one unit test for your controller(s) too.  |
| RESTful conventions |  You should follow RESTful conventions for any endpoints you add into your application in terms of route URLs and HTTP verbs. And also return correct HTTP status codes for responses (for example, 404 when a to-do item isn't found). This includes supporting HTTP PUT and POST correctly when it comes to saving new to-do items or saving updates to existing items. |
| Validation |   Validation should be added to check values provided for all fields are valid. This includes mandatory data fields. |
|  Database & model | Entities and models should use appropriate types for the data they are storing. For example, a DateTime or DateOnly should be used for a due date, not a string. |
