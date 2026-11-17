# SafeSend

**Private sharing. Without the permanent footprint.**

SafeSend is a secure file and text-sharing web application designed to make sharing sensitive information simpler and more private. It aims to provide temporary, self-destructing links that allow recipients to access shared content only within a defined period or number of views.

## Overview

SafeSend is designed around the idea that sensitive information should not remain accessible indefinitely.

The application aims to allow senders to:
- Share sensitive text and small files through temporary links.
- Set link expiration times.
- Restrict access to a single view.
- Share information without requiring an account.

Registered users can also have access to account-based link management.

## Technology Stack

| Component | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 9) |
| Language | C# |
| Frontend | Razor Views, HTML, CSS, JavaScript |
| Authentication | ASP.NET Core Identity |
| Database | SQLite |
| ORM | Entity Framework Core |
| Data Access | Dapper |
| Architecture | MVC, Repository Pattern, Dependency Injection |
| Client-side Requests | AJAX |

## Application Architecture

The application follows the Model-View-Controller (MVC) architectural pattern.

- **Models:** Represent application data and view-specific information.
- **Views:** Razor-based interfaces responsible for presenting the application.
- **Controllers:** Handle incoming requests and coordinate application operations.
- **Repositories:** Encapsulate database access and provide a structured data-access layer.
- **Dependency Injection:** Manages application services and repository dependencies.

ASP.NET Core Identity handles user registration, authentication and account management.

## Implemented Features

The current implementation includes:

- Responsive landing page and shared navigation layout.
- User registration and login using ASP.NET Core Identity.
- Authentication cookie management and logout.
- AJAX-based email availability checking.
- Client-side form validation.
- Authenticated dashboard with link metadata display.
- Create Link interface with message input and file selection.
- Repository-based data access using Dapper.
- SQLite database integration.
- Dependency injection for application services.

## Current Development Scope

The current version focuses on the MVC application structure, authentication, user interfaces and database integration.

The following application capabilities are planned for subsequent development:

- Browser-side encryption using AES-GCM and the Web Crypto API.
- Secure file uploading and encrypted content storage.
- Generation of unique, temporary sharing links.
- Receiver-side content retrieval and decryption.
- One-time access enforcement and automatic link expiration.
- Link revocation and background cleanup.

These capabilities are not part of the current implementation.

## Getting Started

### Prerequisites

- .NET 9 SDK
- Visual Studio Community 2022
- ASP.NET and web development workload

### Installation

Clone the repository:

```bash
git clone https://github.com/ahsan-rajput/SafeSend.git
```

Navigate to the project directory:

```bash
cd SafeSend
```

Restore dependencies:

```bash
dotnet restore
```

Build the application:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

Open the localhost URL displayed in the terminal.

### Database

SafeSend uses SQLite for persistent application data.

The database is initialized when the application starts for the first time. ASP.NET Core Identity manages authentication-related data, while Dapper is used for repository-based data access.

## Project Status

**Current Stage:** Deliverable 2 – ASP.NET Core MVC and Identity implementation.

SafeSend is under active development, with additional security and sharing capabilities planned for future stages.