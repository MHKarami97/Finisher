using Finisher.Application.Interfaces;
using Microsoft.Data.SqlClient;

namespace Finisher.Infrastructure.Database;

public class SqlServerUniqueConstraintChecker : IUniqueConstraintChecker
{
    public bool IsUniqueConstraintViolation(Exception ex)
    {
        return ex is DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } };
    }
}
