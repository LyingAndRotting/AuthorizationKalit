using Authorization.Application.Features.Login.Queries.Login;
using Authorization.Application.Features.Register.Queries;
using Authorization.Domain.Models;
using Authorization.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(AuthorizationContext context, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromQuery] string name, [FromQuery] string password)
    {
        try
        {
            var query = new LoginQuery(name, password);
            var result = await _mediator.Send(query);
            if (result != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Name),
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Ok("Вы успешно вошли!");
            }
            return BadRequest("Неверный логин или пароль.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<Result>> Register([FromQuery] string name, [FromQuery] string password)
    {
        try
        {
            var query = new RegisterQuery(name, password);
            var result = await _mediator.Send(query);
            if (result.Success) 
            {
                return Ok($"{result.Message}");
            } 

            return BadRequest($"{result.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }

    [Authorize]
    [HttpGet("main")]
    public async Task<ActionResult> Main()
    {
        return Redirect("/main.html");
    }

    [Authorize]
    [HttpGet("current-user")]
    public ActionResult<string> GetUser()
    {
        var userName = User.Identity?.Name;
        return Ok(userName);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Вы успешно вышли из системы");
    }
}