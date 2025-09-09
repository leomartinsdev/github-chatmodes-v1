# Weather Forecast Application Architecture

## Overview
This solution follows Clean Architecture principles using C# 12 and .NET 8.0. It is designed for maintainability, scalability, and testability.

## Project Structure

```
WeatherForecastApp/
│
├── src/
│   ├── Presentation/      # Blazor WebAssembly UI (.NET 8, C# 12)
│   ├── Application/       # Use Cases, DTOs, Service Interfaces
│   ├── Domain/            # Entities, Value Objects, Business Rules
│   └── Infrastructure/    # API Adapters, Repositories, External Services
│
├── tests/                 # xUnit tests (QA responsibility)
│
└── README.md              # Project documentation
```

## Layer Responsibilities
- **Presentation**: UI layer using Blazor WebAssembly. Communicates with Application layer via DI.
- **Application**: Contains use cases, DTOs, and service interfaces. Orchestrates business logic.
- **Domain**: Core business entities, value objects, and rules. No dependencies on other layers.
- **Infrastructure**: Implements repositories, API adapters (e.g., Open-Meteo), and external service integrations.

## Design Patterns
- Dependency Injection (built-in .NET)
- Repository Pattern (for API/data access)
- Adapter Pattern (for Open-Meteo API)
- Factory Pattern (for entity creation from DTOs)

## Technologies
- C# 12, .NET 8.0
- Blazor WebAssembly (compatible with .NET 8)
- xUnit (for tests, handled by QA)

## API Integration
- Weather data is fetched from Open-Meteo API: https://open-meteo.com/en/docs
- Example endpoint: `https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m`

## Extensibility & Testability
- New features can be added by extending Application/Infrastructure layers.
- Business logic is isolated for easy testing.

## Next Steps
1. Scaffold projects for each layer.
2. Define core interfaces and entities.
3. Implement Open-Meteo API adapter.
4. Set up Blazor WebAssembly UI.
5. Document all architectural decisions.
