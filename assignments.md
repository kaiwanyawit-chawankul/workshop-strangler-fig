## Workshop Assignments

### Step 0 — Monolith Baseline

* Run **Monolith API** on port `5070`.
* Verify `/users`, `/products`, `/orders` all work.
* Run acceptance tests; all should pass.

### Step 1 — Introduce Gateway

* Create **Gateway** using YARP.
* Route **all traffic** to Monolith.
* Update acceptance tests `BaseAddress` to `http://localhost:5080`.
* Verify tests still pass.

### Step 2 — Strangle `User`

* Create **UserService** with SQLite DB.
* Route `/users*` to UserService in Gateway.
* Remove `/users` endpoint from Monolith.
* Verify acceptance tests still pass.

### Step 3 — Strangle `Product`

* Create **ProductService** with SQLite DB.
* Route `/products*` to ProductService in Gateway.
* Remove `/products` endpoint from Monolith.
* Verify tests still pass.

### Step 4 — Strangle `Order`

* Create **OrderService** with SQLite DB.
* Implement Outbox pattern.
* Integrate with **Mock Email Service**.
* Route `/orders*` to OrderService in Gateway.
* Remove `/orders` endpoint from Monolith.
* Verify acceptance tests pass, and email is received at `/sent`.

### Step 5 — Cleanup

* Ensure Monolith no longer serves `/users`, `/products`, `/orders`.
* Gateway + services provide full functionality.

---
## Checklist / Acceptance Criteria
 - Baseline acceptance tests green against Monolith
 - Gateway proxies all traffic
 - `/users` routed to UserService; tests still green
 - `/products` routed to ProductService; tests still green
 - `/orders`/orders routed to OrderService; mock email received; tests still green
 - Monolith no longer exposes Users/Products/Orders

## Tips & Gotchas
 - Keep route shapes and response schemas identical as you migrate, so tests don’t need rewrites.
 - Use SQLite files per service to simulate autonomous persistence without environment friction.
 - For real systems, do a data backfill + dual‑write window before flipping routes.
 - Keep the Gateway simple; push business logic into services.

## References (general)
 - EF Migrations: dotnet ef migrations add <Name> then dotnet ef database update.
 - YARP routes can be hot‑reloaded if you keep reloadOnChange: true.
 - Use HttpClientFactory and IHostedService for production‑grade outbox dispatchers.

---

## Stretch Goals

* Add **OpenTelemetry tracing**.
* Replace SQLite with **Postgres + Testcontainers**.
* Containerize services with Docker Compose.
* Add contract tests.
