namespace Finisher.Domain.Exceptions;

[Serializable]
public sealed class SameDataException : Exception
{
    public SameDataException()
    {
    }

    public SameDataException(string message)
        : base(message)
    {
    }

    public SameDataException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
