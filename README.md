# Finisher

## Build

Run `dotnet build -tl` to build the solution.

## Run

To run the web application:

```bash
cd .\src\Web\
dotnet watch run
```

Navigate to https://localhost:5001. The application will automatically reload if you change any of the source files.

## Code Styles & Formatting

Includes [EditorConfig](https://editorconfig.org/) support to help maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. The **.editorconfig** file defines the coding styles applicable to this solution.

## Database

`where sln exist`

New Migration:
 ```
 dotnet ef migrations add --project src\Infrastructure\Infrastructure.csproj --startup-project src\Web\Web.csproj --context Finisher.Infrastructure.Data.ApplicationDbContext --configuration Debug --verbose Initial --output-dir Data\Migrations
 ```

Create script:
```
dotnet ef migrations script --project src\Infrastructure\Infrastructure.csproj --startup-project src\Web\Web.csproj --context Finisher.Infrastructure.Data.ApplicationDbContext --configuration Debug --verbose 0 20250506072517_Initial --output src\Infrastructure\Data\Scripts\script_init_01.sql
```

Update database:
```
dotnet ef database update --project src\Infrastructure\Infrastructure.csproj --startup-project src\Web\Web.csproj --context Finisher.Infrastructure.Data.ApplicationDbContext --configuration Debug --verbose 20250506072517_Initial
```


## Test

The solution contains unit, integration, and functional tests.

To run the tests:
```bash
dotnet test
```