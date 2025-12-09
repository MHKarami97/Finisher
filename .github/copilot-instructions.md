## Purpose

These instructions define how AI assistants and code generators must write and modify code in this repository.  
The goals are: consistent style, high performance, clear DDD/architecture, and production‑ready quality.

***

## Target Stack

- All new projects must target **.NET 8.0**.  
- Language is **C#** only (no dynamic or scripting languages inside the solution).  
- Architecture follows **DDD + Clean/Hexagonal**: Domain and Application at the core, Infrastructure and Web at the edges.

***

## Architecture & Design Rules

1. **DDD & Clean Architecture**

   - Keep **domain model pure**: no references from Domain to Infrastructure or Web.  
   - Prefer **rich domain models**: entities, value objects, aggregates, domain events, domain services where needed.  
   - Application layer orchestrates use cases; controllers and UI must not contain business logic.  
   - Respect boundaries between layers; do not “reach across” layers for convenience.

2. **SOLID, OOP, and Patterns**

   - Apply **SOLID** systematically (especially SRP, DIP).  
   - Favor **composition over inheritance**.  
   - Prefer well‑known patterns where useful: Repository, Specification, Factory, Strategy, etc.  
   - Keep methods small and focused; avoid “god classes” and long procedural methods.

3. **Dependency Injection**

   - Use **constructor injection** for all dependencies.  
   - Avoid service locator and static access to services.  
   - Register abstractions (interfaces) at the composition root (Web/host project).

4. **Async and Performance**

   - Use **async/await** for I/O‑bound work; avoid blocking (`.Result`, `.Wait()`, `Thread.Sleep`) in production code.  
   - Avoid unnecessary allocations and boxing; prefer `readonly` where appropriate.  
   - Guard expensive logging arguments (`IsEnabled` or source‑generated logging) to satisfy analyzers and performance expectations.  
   - Prefer **streaming**/chunked APIs over buffering large payloads when possible.

5. **Time, I/O, and External Services**

   - Do **not** use `DateTime.Now` or `DateTime.UtcNow` directly. Use `TimeProvider` (injected) and `DateTimeOffset` for persistence and APIs.  
   - Encapsulate file system, network, and external services behind interfaces in Infrastructure; Application/Domain should not talk to them directly.

***

## Coding Standards

1. **General**

   - Write **clear, self‑documenting code**; names should remove the need for comments.  
   - Comments are allowed only where logic is non‑obvious; do **not** comment obvious code.  
   - Handle errors with **structured exception handling**; do not swallow exceptions silently. Log or rethrow as appropriate.

2. **Naming**

   - **Classes, interfaces, enums:** `PascalCase` (`OrderService`, `IOrderRepository`).  
   - **Methods & properties:** `PascalCase` (`GetById`, `TotalAmount`).  
   - **Local variables & parameters:** `camelCase` (`orderId`, `userName`).  
   - **Constants & static readonly fields:** `PascalCase` (`DefaultTimeoutInSeconds`).  
   - **Unit tests:** expressive, scenario‑based names:  
     `MethodName_StateUnderTest_ExpectedBehavior`.

3. **Style & Layout**

   - Braces: **Allman style** (opening brace on a new line).  
   - Indentation: **4 spaces**, no tabs.  
   - Max line length: **120 characters**; break expressions if needed.  
   - One **top‑level type per file**.  
   - Organize files by **feature/bounded context or layer**, not by primitive type (e.g., don’t create `Helpers` dumps).

4. **Using directives**

   - Place `using` directives **outside** the namespace.  
   - Remove unused `using`s.  
   - Use `file-scoped namespace` or block namespace consistently with the rest of the project; follow the existing style.

5. **Nullability & Analyzers**

   - Keep **nullable reference types enabled** and satisfy compiler/analyzer warnings.  
   - Do not ignore analyzers; fix issues or justify and suppress them locally with comments only when clearly necessary.

***

## Security & Banned Patterns

When generating code, avoid:

- Direct use of **insecure or obsolete APIs** (e.g., legacy crypto, insecure random generators).  
- Building SQL or command strings via concatenation; always use **parameterized** APIs.  
- Hard‑coding secrets, connection strings, or keys in code or configuration checked into source control.  
- Direct use of `DateTime.Now` / `DateTime.UtcNow`; use `TimeProvider`.  
- Sync‑over‑async and blocking calls in ASP.NET Core request pipelines.

If you must touch any of these areas, prefer existing abstractions and utilities already present in the solution.

***

## Testing

- Every new non‑trivial behavior must have **unit and/or integration tests**.  
- Tests must be **deterministic**; no reliance on real time, random, external services, or network unless explicitly marked as integration tests.  
- Use **Fake/Mock TimeProvider** and other abstractions to control time and side effects.  
- Keep test code readable: AAA (Arrange‑Act‑Assert) or similar pattern, one logical assertion per behavior.

***

## Git, Commits, and PR Expectations

- Group changes by **feature or fix**, not by file.  
- Write clear commit messages: `Feature: ...`, `Fix: ...`, `Refactor: ...`.  
- For generated code, ensure it **compiles** and **passes tests** before opening a PR.  
- Document **any deviation** from these instructions in the PR description and explain why.

***

## Specific Instructions for Copilot / AI Tools

When proposing or generating code in this repository:

1. **Follow the existing patterns first.**  
   - Look at current projects and modules; match their structure, naming, and style.  
   - Reuse existing abstractions instead of inventing new ones for the same concern.

2. **Prefer quality over shortcuts.**  
   - Do not generate sample/dummy code that will not compile.  
   - Avoid over‑engineering; implement the simplest design that fits the domain and respects boundaries.

3. **Respect performance and correctness.**  
   - Default to async for I/O.  
   - Guard expensive operations and logging.  
   - Avoid unnecessary allocations and dynamic constructs.

4. **Minimize comments; maximize clarity.**  
   - Fix naming and structure before adding comments.  
   - Only add comments where the intent cannot be made obvious through code alone.

5. **Do not bypass standards.**  
   - If a requested change conflicts with these rules, prefer these **repository standards**.  
   - If a deviation is explicitly required, note it clearly for manual review.