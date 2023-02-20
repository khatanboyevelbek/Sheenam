namespace Sheenam.Api.Services.Foundations.Security.PasswordHash
{
    public interface IPasswordHashServise
    {
        public string GenerateHashPassword(string password);
    }
}
