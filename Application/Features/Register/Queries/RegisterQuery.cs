using Authorization.Domain.Models;
using MediatR;

namespace Authorization.Application.Features.Register.Queries;

public class RegisterQuery : IRequest<Result>
{
    public string? Name { get; set; }
    public string? Password { get; set; }

    public RegisterQuery(string name, string password)
    {
        Name = name;
        Password = password;
    }
}