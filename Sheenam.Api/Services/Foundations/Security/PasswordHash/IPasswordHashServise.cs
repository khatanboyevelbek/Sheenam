namespace Sheenam.Api.Services.Foundations.Security.PasswordHash
{
    public interface IPasswordHashServise
    {
        string GenerateHashPassword(string password);
    }
}
