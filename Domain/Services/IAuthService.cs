using Authorization.Domain.Models;

namespace Authorization.Domain.Services;

public interface IAuthService
{
    Task<User?> Login(string name, string password);
    Task<Result> Register(string name, string password);
}