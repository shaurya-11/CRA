using PatchManager.Models;

namespace PatchManager.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username, string role);
        bool VerifyPassword(string password, string hash);
        string HashPassword(string password);
    }
}
