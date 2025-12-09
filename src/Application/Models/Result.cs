namespace Finisher.Application.Models;

public class Result
{
    internal Result(bool succeeded, ICollection<string>? errors)
    {
        Succeeded = succeeded;
        Errors = errors?.ToList().AsReadOnly();
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(ICollection<string> errors)
    {
        return new Result(false, errors);
    }

    public bool Succeeded { get; init; }

    public IReadOnlyCollection<string>? Errors { get; init; }
}
