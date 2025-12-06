namespace Finisher.Web.Common;

internal static class SummaryCreator
{
    public static string Create(string entity) => $"Create a new {entity.ToLowerInvariant()}";
    public static string GetById(string entity) => $"Get the specified {entity.ToLowerInvariant()} by id";
    public static string GetPagination(string entity) => $"Get a paginated list of {entity.ToLowerInvariant()}s";
    public static string Update(string entity) => $"Update an existing {entity.ToLowerInvariant()}";
    public static string Delete(string entity) => $"Delete the specified {entity.ToLowerInvariant()}";
}
