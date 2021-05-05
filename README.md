
﻿In this assignment we are going integrate the Web project with the API project, and fix some testing failures from the last assignment.

# Assignment
- Update the API project with the following
  - Add a new Dto namespace ✔❌
  - The `UserController` should only accept or return classes from the Dto namespace. Create any needed classes there and update the controller to use them. You will need to convert between the DTO classes and the relevant entities as needed. ✔❌
  - Add OpenAPI (swagger) endpoint ✔❌
  - Use the OpenAPI to generate a C# client with relevant interfaces ✔❌
  - Decorate all relevant API endpoints in the `UserController` with the appropriate [ProducesResponseType](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0#iactionresult-type) ✔❌
- Update the Web project with the following
  - Register the generated `UserClients` class using its interface with the Dependency Injection (DI) service. ✔❌
  - Update the `UserController` to take in the generated client as a dependency. ✔❌
  - Convert the `UserController` from using the `MockData` to using the client to get data from the API service. ✔❌
  - Update the controller to convert back and forth between the ViewModel classes and the API DTO classes as needed. ✔❌
- Update the SecretSanta.Api.Tests with the following:
  - The controller tests should be [interation tests](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0) not unit tests. ✔❌
  - All endpoints should be tested. ✔❌
  - The `UserController` should use a test double for the `IUserRepository` to avoid needing to retest business and data logic. ✔❌
- Create a new SecretSanta.Web.Tests projecs with the following:
  - It should be inside of the test directory. ✔❌
  - It should have nullable reference types enabled. ✔❌
  - It should have functional tests for all endpoints in the `UserController` that access the API. ✔❌
  - It should use a test double for the API service. This test double should be used to validate the controller's interaction with the API without actually calling the API. ✔❌

Extra Credit:
- Rather than coping data between view models and DTOs, and DTOs and entities; use [AutoMapper](https://automapper.org/) to handle it for you.