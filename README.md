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

## Deliverable 3 – Additional Requirements

This update adds the following implementation items while retaining the existing Razor layout, CSS theme, illustrations and page structure:

- **AJAX:** jQuery `$.ajax` submits create-link metadata, `$.get` refreshes dashboard data, and `$.post` revokes a link; email availability remains AJAX-based.
- **Dropdown:** the create-link form includes expiry presets (1 hour, 6 hours, 24 hours and 7 days), which populate the existing expiry date/time field.
- **Repository pattern:** `IShareLinkRepository`, `IUserRepository` and `IShareLinkSummaryRepository` separate database/user lookup work from controllers.
- **Interfaces and Dependency Injection:** all three repositories are registered in `Program.cs` and injected through their interfaces.
- **HTTP methods:** GET endpoints load pages and dashboard data; POST endpoints create metadata and revoke a link; AJAX uses both.
- **Identity customization:** the custom `ApplicationUser` continues to store `DisplayName`; Identity has unique-email, password, lockout and application-cookie settings.
- **Layout sections:** the shared Razor layout supports optional `Styles` and `Scripts` sections.
- **IIS deployment:** a folder publish profile and ASP.NET Core IIS `web.config` are included.

### Important scope note

The create-link POST currently stores **metadata only** (label, owner and expiry). It does not upload the message/file, perform browser encryption, or generate a real recipient URL. Those remain future SafeSend features as described in the project specification.

### Local IIS deployment

1. Install the .NET 9 Hosting Bundle on the Windows machine that will host the app.
2. Enable **Internet Information Services (IIS)** and the **ASP.NET Core Module**.
3. Publish the project in Release mode:

   ```powershell
   dotnet publish -c Release -o .\publish
   ```

4. In IIS Manager, create a website whose physical path is the `publish` folder and assign an available port.
5. Give the IIS Application Pool identity write permission to the folder containing `safesend.db` (the app's data folder).
6. Browse to the configured local site address.

For a first local test, run `dotnet run` and use the localhost URL printed by ASP.NET Core. IIS hosting should be tested separately after publishing.

### Deliverable 3 verification checklist

- [ ] Register a new account and verify the custom display name is accepted.
- [ ] Confirm duplicate email feedback appears through AJAX.
- [ ] Log in and verify authenticated dashboard access.
- [ ] Create link metadata using each expiry dropdown preset.
- [ ] Refresh dashboard data using the AJAX refresh button.
- [ ] Revoke a link and confirm its status changes.
- [ ] Confirm a user cannot revoke another user's link.
- [ ] Run `dotnet build` and `dotnet publish -c Release`.
- [ ] Test the published folder through local IIS.
