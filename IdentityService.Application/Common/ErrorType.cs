namespace IdentityService.Application.Common
{
    public enum ErrorType
    {
        None = 0,
        Validation = 1,
        Unauthorized = 2,
        NotFound = 3,
        Conflict = 4,
        Failure = 5
    }
}
