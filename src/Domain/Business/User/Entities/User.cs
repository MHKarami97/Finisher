namespace Finisher.Domain.Business.User.Entities;

public class User : BaseEntity
{
    public User(string? nationalCode,
        string? fullName,
        DateTime? dateOfBirth)
    {
        Validate();

        NationalCode = nationalCode;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }

    private User() { }

    public string? NationalCode { get; init; }
    public string? FullName { get; init; }
    public DateTime? DateOfBirth { get; init; }

    #region Public Methods

    #endregion

    private static void Validate()
    {
    }
}
