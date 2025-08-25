# C# Workshop: Strangler Fig Pattern — Break Down a Monolith into Microservices (User/Product/Order)

This repo demonstrates the **Strangler Fig Pattern** to incrementally break down a **monolithic C# API** into microservices (**User**, **Product**, **Order**), fronted by a **Gateway (YARP)**. Acceptance tests ensure consistent behavior throughout the migration.

---

## Repository Structure

```
strangler-workshop/
  src/
    Monolith/                # Legacy API (starting point)
    Gateway/                 # YARP reverse proxy façade
    Services/
      UserService/
      ProductService/
      OrderService/
      EmailService.Mock/     # Mock email microservice
  tests/
    AcceptanceTests/         # xUnit + FluentAssertions; hits Gateway
```

---

## Getting Started

### Prerequisites

* .NET 9 SDK
* Git
* SQLite tools (optional)

### Run Monolith (baseline)

```bash
cd src/Monolith
DOTNET_URLS=http://localhost:5070 dotnet run
```

### Run Acceptance Tests (against Monolith)

```bash
cd tests/AcceptanceTests
dotnet test
```

All tests should pass ✅

### Run Gateway (facade)

```bash
cd src/Gateway
DOTNET_URLS=http://localhost:5080 dotnet run
```

Update `AcceptanceTests` to point to `http://localhost:5080`.

---

## Migration Steps (Assignments)

See `assignments.md` for detailed workshop tasks.

---

## References

* [Martin Fowler — Strangler Fig Pattern](https://martinfowler.com/bliki/StranglerFigApplication.html)
* [Azure Architecture Center — Strangler Fig](https://learn.microsoft.com/en-us/azure/architecture/patterns/strangler-fig)
* [YARP Reverse Proxy](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/yarp/getting-started?view=aspnetcore-9.0)
* [EF Core + SQLite](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)

