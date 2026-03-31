namespace IdentityService.Application.Common
{
    public static class AuthErrors
    {
        public static readonly Error InvalidCredentials = new(
            "auth.invalid_credentials",
            "Invalid email or password.",
            ErrorType.Unauthorized);

        public static readonly Error InvalidRefreshToken = new(
            "auth.invalid_refresh_token",
            "Refresh token is expired, logout and login again.",
            ErrorType.Unauthorized);

        public static readonly Error DuplicateEmail = new(
            "auth.duplicate_email",
            "A user with this email already exists.",
            ErrorType.Conflict);
    }
}
