namespace Finisher.Shared.Exceptions;

public static class ExceptionInfo
{
    public static readonly SystemExceptionType Error500 = new(nameof(Messages.UnhandledException), Messages.UnhandledException, Messages.UnhandledExceptionUserAction);
    public static readonly SystemExceptionType Error401 = new(nameof(Messages.UnauthorizedExeption), Messages.UnauthorizedExeption, Messages.UnauthorizedExeptionUserAction);
    public static readonly SystemExceptionType Error403 = new(nameof(Messages.ForbiddenExeption), Messages.ForbiddenExeption, Messages.ForbiddenExeptionUserAction);
    public static readonly SystemExceptionType Error404 = new(nameof(Messages.NotFoundExeption), Messages.NotFoundExeption, Messages.NotFoundExeptionUserAction);
}

public record SystemExceptionType(string Type, string Reason, string UserAction);
