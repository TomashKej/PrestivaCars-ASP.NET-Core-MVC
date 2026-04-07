# PrestivaCars

PrestivaCars is an ASP.NET Core MVC web application, split into a public-facing site and a separate intranet site. The solution also contains a data layer project intended for persistence and domain models.

This project is in early development and has only recently started.

## Repository Structure

- `PrestivaCars.Web` — public website (ASP.NET Core MVC, Razor views).
- `PrestivaCars.Intranet` — internal/intranet website (ASP.NET Core MVC, Razor views).
- `PrestivaCars.Data` — data layer (Entity Framework Core models and `DbContext`).

## Tech Stack

- .NET (Target Framework: `net10.0`)
- ASP.NET Core MVC + Razor Views
- Entity Framework Core (data project)

## Prerequisites

- .NET SDK that supports `net10.0`
- (Optional) Visual Studio / JetBrains Rider

## Running Locally

Restore dependencies:

```bash
dotnet restore
```

Run the public website:

```bash
dotnet run --project PrestivaCars.Web
```

Run the intranet website:

```bash
dotnet run --project PrestivaCars.Intranet
```

Default development URLs are defined in each project’s `Properties/launchSettings.json`.

## Configuration

Each web project uses `appsettings.json` plus environment-specific overrides (for example `appsettings.Development.json`).

Recommended approach for secrets (connection strings, API keys, etc.):

- use environment variables, or
- use .NET User Secrets for local development

Do not commit real secrets to the repository.

## Data Layer

`PrestivaCars.Data` contains:

- `PrestivaCarsContext` (`DbContext`)
- entity models for CMS, user profiles, and the vehicle catalogue/offers:
  - CMS: `Page`, `Banner`
  - Catalogue: `Vehicle`, `VehicleCategory`, `VehicleOffer`, `VehicleFeature`, `VehicleImage`, `VehicleOfferFeature`, `SavedOffer`
  - Users: `UserProfile`

Database provider and connection string setup is expected to be configured by the consuming web application(s).

### CRUD and Audit Fields

Entities inherit a shared `BaseEntity` that standardises common audit fields such as `IsActive`, `CreatedAt/CreatedBy`, `UpdatedAt/UpdatedBy`, and `DeletedAt/DeletedBy`.

`PrestivaCarsContext` automatically sets audit fields during `SaveChanges` / `SaveChangesAsync` to keep CRUD operations consistent and reduce duplication.

Controllers use explicit model binding to avoid exposing or editing base audit fields in forms and views (and to reduce overposting risk).

## Build

```bash
dotnet build
```

## Publish

```bash
dotnet publish PrestivaCars.Web -c Release
```

```bash
dotnet publish PrestivaCars.Intranet -c Release
```
