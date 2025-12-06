namespace Finisher.Application.Interfaces;

public interface IUniqueConstraintChecker
{
    bool IsUniqueConstraintViolation(Exception ex);
}
