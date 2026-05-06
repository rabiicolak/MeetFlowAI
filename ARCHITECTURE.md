# Architecture

## Layered Architecture

The project uses a folder-based layered architecture inside a single project (`src/MeetFlow.Web`):

- **Core**: Contains domain models, interfaces, and shared logic.
- **Controllers**: Handles incoming HTTP requests and responses. (e.g., HomeController, DashboardController, AudioController)
- **Services**: Contains business logic for AI analysis.
- **Repositories**: Manages data access for future persistence.
- **ViewModels**: Data transfer objects for views (e.g., DashboardViewModel).
- **Views**: UI components built with Bootstrap 5 and Chart.js.

## Tech Stack
- **Framework**: ASP.NET Core MVC (C# / .NET 8)
- **Frontend**: Bootstrap 5, Vanilla JS, CSS3, Chart.js
- **Tooling**: GitHub Actions (planned), AI Agents for code generation
