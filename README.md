
# Blood Bank Management System

## Overview
ASP.NET 8 MVC web application designed to showcase advanced development skills through a blood bank management system. Built with modern architecture and best practices, it provides a **solution for** managing blood donations, stock, and related information.

## Features
- Interactive Baby BloodType Prediction Tool
- Complete CRUD operations for Donors Management and BloodStock management
- Dynamic search filtering, column sorting & client-side pagination
- Register/Login with Roles and Users Management
- Analytics Dashboard with visualization & charts for critical metrics
- Performance optimization with manual cache purge capability
- Input validations to ensure data integrity
- Responsive (mobile & desktop) UI design

 
## Architecture

The Application follows **4-tier layered architecture** that ensures **separation of concerns** (each layer focuses on a specific responsibility), **maintainability** (isolating the Bussiness Logic make it easier to write unit tests for core features without relying on database), and **scalability** (new features and modules can be added without tightly coupling the entire application).

The Structure Follow this **Top To Down** Flow:

```
                                                   [Presentation (Web)]
                                                            ↓
                                                    [Business Layer]
                                                            ↓
                                                   [DataAccess Layer]
                                                            ↓
                                         [Core Layer (DTOs, Interfaces, Entities)]

```

Each lower layer never references or depends on higher ones, which prevents circular dependencies and allows each layer to be tested and modified independently.

 

## Technologies Used

### Speed
  Raw SQL Queries, IMemoryCache, HTTP Response Compression(zstd algorithm) , EF Core ORM(Eager Loading – AsNoTracking Read) , Cancellation tokens(ensures better resource management).
### Security
 Identity **Authentication** (optimal for MVC applications), **Authorization** Roles (Admin - Employee - User), **Input Validation**
### Scalability and Maintainability
**Asynchronous** code , The Layered Archticture with Readable code with comments , configuration via appsettings, and applying **SOLID** principles [ **SRP**(different classes for different features) , **DIP**(Inject interfaces and use its functionality instead of concrete implementations) , **ISP**(each interface for specific feature) ].

 ## Technical Approaches
-   **IHttpClientFactory =>** Efficient reusable HTTP client to communicate with External API, avoids common issues like socket exhaustion, and promotes clean DI
-   **Options Pattern=>** Manage external service configurations (API URLs, Keys) for scalability and maintainability
-   **Background Jobs With Hangfire=>**  Periodic automatic jobs that update the `Age` and `IsEligibleToDonate` columns weekly and daily, respectively, to ensure data accuracy (The Age is required in the database according to the business need)

 **Development Workflow =>** **Version Control using Git** basic Feature/Fix Branching to develop isolated features or fixes, then committing messages, then review and merge into Main branch (latest-stable-production-ready).

## Screenshots
 For visual examples of the application's interface and features, please see our [Project Screenshots](SCREENSHOTS.md).
 
 