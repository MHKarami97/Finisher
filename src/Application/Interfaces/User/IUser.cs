namespace Finisher.Application.Interfaces.User;

public interface IUser
{
    /// <summary>
    /// throw exception if user not loggedIn
    /// </summary>
    int Id { get; }

    /// <summary>
    /// return zero if user not loggedIn
    /// </summary>
    int IdIfLoggedIn { get; }
}
