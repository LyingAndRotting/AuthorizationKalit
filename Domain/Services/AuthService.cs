using Authorization.Domain.Models;
using Authorization.Infrastructure;
using MediatR;  
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Authorization.Domain.Services;

public class AuthService : IAuthService
{
    private readonly AuthorizationContext _context;

    public AuthService(AuthorizationContext context)
    {
        _context = context;
    }
    public async Task<User?> Login(string name, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public async Task<Result> Register(string name, string password)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.Name == name) != null)
        {
            return new Result()
            {
                Message = "Пользователь с таким именем уже существует.",
                Success = false
            };
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { Id = Guid.NewGuid(), Name = name, Password = hashedPassword };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new Result()
        {
            Message = "Пользователь успешно зарегистрирован!",
            Success = true
        };
    }
}