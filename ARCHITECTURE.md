# Architecture

## Layered Architecture

The project uses a folder-based layered architecture inside a single project (`src/MeetFlow.Web`):
- **Core**: Contains domain models, interfaces, and shared logic.
- **Controllers**: Handles incoming HTTP requests and responses.
- **Services**: Contains business logic and interacts with repositories.
- **Repositories**: Manages data access.
- **ViewModels**: Data transfer objects for views.
- **Views**: UI components.
