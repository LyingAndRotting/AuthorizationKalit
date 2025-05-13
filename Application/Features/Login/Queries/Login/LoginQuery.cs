using Authorization.Domain.Models;
using MediatR;

namespace Authorization.Application.Features.Login.Queries.Login;

public class LoginQuery : IRequest<User?>
{
    public string Name { get; set; }
    public string Password { get; set; }
    
    public LoginQuery(string name, string password)
    {
        Name = name;
        Password = password;
    }
}