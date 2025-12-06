using System.Data.Common;

namespace Finisher.Application.FunctionalTests;

internal interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    string GetConnectionString();

    Task ResetAsync();

    Task DisposeAsync();
}
