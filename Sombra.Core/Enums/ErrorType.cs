namespace Sombra.Core.Enums
{
    public enum ErrorType
    {
        Other = 0,
        EmailExists = 1,
        NotFound = 2,
        TokenExpired = 3,
        TokenInvalid = 4,
        InvalidEmail = 5,
        AlreadyActive = 6,
        InvalidKey = 7,
        InactiveAccount = 8,
        InvalidPassword = 9,
        CharityNotFound = 10,
        CharityActionNotFound = 11,
        UserNotFound = 12,
        NoDonationsFound = 13,
        InvalidUserKey = 14
    }
}