# SafeSend — Deliverable 2

ASP.NET Core MVC application targeting .NET 9.0. Designed for Visual Studio Community 2022 and .NET SDK 9.0.318.

## Included in this delivery

- MVC controllers and Razor Views for Home, Login, Register, Dashboard, Create Link and the receiver route.
- Shared responsive Razor layout and custom CSS/JavaScript.
- ASP.NET Core Identity registration, login, logout and cookie authentication.
- SQLite database for Identity.
- Dapper repository (`IShareLinkRepository` / `ShareLinkRepository`) and dependency injection.
- AJAX email availability check on the registration form.
- Basic client-side validation through ASP.NET Core validation attributes and jQuery unobtrusive validation.

## Intentionally not implemented yet

The create-link page is a UI/interaction preview only. It does not encrypt or upload content, create real share links, decrypt receiver content, revoke links or run background expiry cleanup. The dashboard reads link metadata from the Dapper repository; it will be empty until link creation is implemented in a later delivery. Do not enter real sensitive information into this preview.

## Requirements

- .NET 9 SDK (your reported version 9.0.318 is suitable)
- Visual Studio Community 2022 with the ASP.NET and web development workload
- Internet connection for the first NuGet restore and the optional Google Fonts / jQuery validation CDN assets

## Run in Visual Studio 2022

1. Extract the ZIP.
2. Open `SafeSend.csproj` in Visual Studio.
3. Allow NuGet packages to restore.
4. Set `SafeSend` as the startup project.
5. Press **F5** or **Ctrl+F5**.
6. Register a new account, log out, and log in again. Visit **My links** after signing in.

## Run from terminal

Open a terminal in the folder containing `SafeSend.csproj`:

```bash
dotnet --version
dotnet restore
dotnet build
dotnet run
```

Open the localhost URL printed by `dotnet run`.

## Database

The SQLite database `safesend.db` is created in the application working directory at first run. Identity tables are created by EF Core `EnsureCreated`. The `ShareLinks` metadata table is initialized separately and accessed with Dapper.

For this deliverable the project uses `EnsureCreated` to keep first-run setup simple. If you later introduce EF Core migrations, switch to a migrations-based database initialization strategy rather than mixing migrations with `EnsureCreated`.

## Suggested test checklist

- [ ] Home page and navigation render at desktop and mobile widths.
- [ ] Register a new account.
- [ ] Registration rejects invalid email / weak password / mismatched confirmation.
- [ ] AJAX email check reports whether an email is already registered.
- [ ] Log out and log back in with the registered credentials.
- [ ] Dashboard redirects anonymous visitors to Login.
- [ ] Dashboard loads with an empty state for a new account.
- [ ] Create Link preview validates empty content and displays selected settings.
- [ ] No IIS deployment is included, as requested.

## Push to your existing GitHub repository

After you have tested locally, copy the project files into your existing repository folder (or replace the old static HTML-only implementation after keeping a backup), then run:

```bash
git add .
git commit -m "Add Deliverable 2 MVC Identity implementation"
git push origin main
```

Review the files before committing. Do not commit real user data, database files, passwords or secrets. Add `safesend.db` to `.gitignore` if it is created in the project directory.
