using Authorization.Domain.Models;
using Authorization.Domain.Services;
using MediatR;

namespace Authorization.Application.Features.Login.Queries.Login;

public class LoginHandler : IRequestHandler<LoginQuery, User?>
{
    private readonly IAuthService _authService;
    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public Task<User?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = _authService.Login(request.Name, request.Password);
        return user;
    }
}