using Microsoft.Data.SqlClient;
using Finisher.Application.Interfaces;

namespace Finisher.Infrastructure.Database;

public class SqlServerUniqueConstraintChecker : IUniqueConstraintChecker
{
    public bool IsUniqueConstraintViolation(Exception ex)
    {
        return ex is DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } };
    }
}
