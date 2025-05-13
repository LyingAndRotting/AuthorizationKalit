using Authorization.Domain.Models;
using Authorization.Domain.Services;
using MediatR;

namespace Authorization.Application.Features.Register.Queries;

public class RegisterHandler : IRequestHandler<RegisterQuery, Result>
{
    private readonly IAuthService _authService;
    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<Result> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
         var res = await _authService.Register(request.Name!, request.Password!);
         return res;
    }
}