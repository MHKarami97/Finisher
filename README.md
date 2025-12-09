# Finisher – DDD Starter Template

> Opinionated .NET starter template focused on Domain‑Driven Design, clean architecture, and high performance.

Finisher is a template to bootstrap real‑world, domain‑centric systems (modular monolith or future microservices) with a clear separation between Domain, Application, Infrastructure, and Web/API layers. It is designed for teams that want to start with proper boundaries, rich domain models, and modern .NET practices from day one.[1]

***

## Goals

- Provide a **DDD‑friendly solution skeleton**: entities, value objects, aggregates, domain events, and policies organized cleanly.[2]
- Enforce **layered architecture and boundaries** between domain, application, infrastructure, and presentation.[1]
- Bake in **modern .NET practices**: analyzers, banned APIs, async I/O, logging, and testability.[3]
- Serve as a **reusable template** for greenfield projects via GitHub’s “Use this template” and the .NET `dotnet new` system.[4][5]

***

## Features

Adjust this list to match the actual code; it is structured for clarity and future expansion.

### Architecture & Domain

- **Domain‑Driven Design building blocks**: entities, value objects, domain events, aggregates, and application services organized per bounded contexts.[2]
- **Hexagonal / Clean architecture** style: core domain isolated from infrastructure concerns (persistence, messaging, web).[1]
- **Ubiquitous language focus**: domain concepts and namespaces are named to reflect business language, not technical plumbing.[2]

### Application Layer

- **Use‑case oriented application services**: application layer orchestrates domain logic and infrastructure without leaking persistence details.[1]
- **CQRS‑friendly structure**: commands/queries can be split cleanly if needed, keeping reads and writes independent.[6]
- **Integration with mediators / messaging** (e.g., Wolverine or similar) for in‑process messages and cross‑module communication.[6]

### Infrastructure & Persistence

- **Entity Framework Core integration** for relational persistence with a clean separation of DbContext, configurations, and migrations.[1]
- **Dedicated migrations workflow** (CLI‑friendly) so schema evolution is versioned and repeatable.[7]
- Pluggable infrastructure: repositories, outbox, and integrations encapsulated behind interfaces so they can be swapped or extended.[1]

### Web / API Layer

- **ASP.NET Core minimal / controller‑based API** structured around use cases, not tables.[8]
- **Long‑running request logging** and correlation hooks to detect slow endpoints in production.[9][8]
- **API documentation support** (e.g., Swagger/Scalar) ready to expose the public API surface.[10][8]

### Quality, Tooling & Performance

- **Roslyn analyzers and BannedSymbols** configured for secure, modern .NET usage (banning `DateTime.Now`, sync‑over‑async, legacy APIs, etc.).[3][1]
- **Time abstraction** via `TimeProvider` / `DateTimeOffset` for testable, timezone‑safe time handling.[11][1]
- **Logging best practices**: guarded logging to avoid expensive message construction when disabled (e.g., `CA1873`‑compliant patterns).[9][1]
- **Nullable reference types and code analysis** enabled to catch null and API‑usage mistakes early.[1]
- **Unit/integration test projects** scaffolded and aligned with the architecture (tests by module/bounded context).[1]

***

## Solution Structure

Adapt names/paths to your actual layout.

```text
Finisher.sln
src/
  Finisher.Domain/         // Domain model: entities, VOs, events, policies
  Finisher.Application/    // Application services, DTOs, use cases
  Finisher.Infrastructure/ // EF Core, repositories, integrations, messaging
  Finisher.Web/            // ASP.NET Core host (API/UI), composition root
tests/
  Finisher.Domain.Tests/
  Finisher.Application.Tests/
  Finisher.Integration.Tests/
.template.config/
  template.json            // dotnet new template metadata
```

This structure follows a classic DDD‑plus‑Hexagonal layout, where the Web and Infrastructure projects depend on the core domain and application, never the other way around.[6][1]

***

## How to Use This Template

### 1. Create a New Project (GitHub Template)

Once the repo is marked as a **template repository** in GitHub settings, you can create a new project with one click.[4][7]

- Open the Finisher repository in GitHub.[12]
- Click **Use this template** → **Create a new repository**.[13][7]
- Choose a name (e.g., `MyCompany.MyProduct`), visibility, and click **Create repository from template**.[7]

This gives you a new repo with the same structure but without tying to the original Git history.[7]

### 2. Install as a `dotnet new` Template (optional)

If `.template.config/template.json` is present, Finisher can be installed as a .NET template.[14][5]

- Clone the repo locally.[7]
- Run:  

  ```bash
  dotnet new install .
  ```

- Then create projects using:  

  ```bash
  dotnet new finisher -n MyProject
  ```

The exact short name (`finisher`) and parameters are defined in `template.json`.[5][8]

***

## What You Do in the Code

The template gives you structure; you bring the actual domain and integrations.[2]

Typical steps after generating a project:

1. **Rename core namespaces**  
   - Replace `Finisher.*` namespaces with your own (e.g., `MyCompany.Billing`).[8]
   - Update root assembly names and default project names.[8]

2. **Define your domain model**  
   - Identify core entities, value objects, and aggregates from your domain (e.g., `CarePlan`, `Visit`, `Report` in your healthcare idea).[15]
   - Model invariants inside the aggregate roots and express important business changes via domain events.[2]

3. **Implement application use cases**  
   - In `Application`, add command/query handlers or application services for key scenarios (create/update aggregates, domain workflows).[1]
   - Keep business logic in domain/application, not in controllers.[1]

4. **Wire infrastructure**  
   - Configure EF Core mappings, DbContext, and migrations for your aggregates.[1]
   - Implement repository adapters and any external integrations (messaging, email, payment, etc.).[1]

5. **Expose APIs / UI**  
   - Map HTTP endpoints that call into the application layer only.[8]
   - Add API docs (Swagger/Scalar) and authentication if needed.[10][8]

6. **Harden quality and performance**  
   - Tune `BannedSymbols`, analyzer severities, and logging policies to match your standards.[3][9]
   - Add tests for domain invariants and critical workflows before going further.[1]

***

## How to Contribute

Finisher is meant to evolve as a high‑quality template; contributions that improve architecture clarity, performance, or DX are welcome.[8][1]

### Working on Issues

- **Check Issues**: Look for open issues labeled `help wanted` or `good first issue`.[12][7]
- **Discuss first**: For larger changes (new patterns, libraries, or breaking layout changes), open an issue and describe the motivation and approach.[7]

### Development Workflow

- Fork the repo and create a feature branch: `feature/improve-logging` or `fix/ef-config`.[7]
- Keep changes **focused** (one concern per PR) and aligned with DDD/clean architecture principles already used in the template.[1]
- Ensure tests pass and add new tests when you change behavior.[1]

### Coding Guidelines

- Respect **layered boundaries**:  
  - Domain must not depend on Infrastructure or Web.[1]
  - Application orchestrates domain and infrastructure, but does not depend on UI.[1]
- Prefer **Value Objects** over primitive obsession and encapsulate invariants.[2]
- Use **async** APIs and avoid sync‑over‑async or blocking on tasks.[3][1]
- Use `TimeProvider` instead of `DateTime.Now`/`UtcNow` for testability.[11]
- Follow existing analyzer rules; if you need to relax or extend them, explain why in the PR.[3][1]

### Submitting a Pull Request

- Rebase onto `main` before opening a PR to keep history clean.[7]
- Describe **what** and **why** you changed, not only “fixed bug”.[7]
- If you introduce a new concept or pattern, update the README and any diagrams/architecture notes accordingly.[1]