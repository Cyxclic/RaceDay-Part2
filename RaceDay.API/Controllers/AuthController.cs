using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using RaceDay.API.Services;
namespace RaceDay.API.Controllers;
[ApiController, Route("api/auth")]
public class AuthController(RaceDayContext db, PasswordService passwords) : ControllerBase
{
    /// <summary>Register an account. Role must be Organiser or Participant.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest r)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var role = r.Role.Trim();
        if (role is not ("Organiser" or "Participant")) return BadRequest(new { message = "Role must be Organiser or Participant." });
        var email = r.Email.Trim().ToLowerInvariant();
        if (await db.Users.AnyAsync(x => x.Email == email)) return Conflict(new { message = "Email is already registered." });
        var user = new User { FullName = r.FullName.Trim(), Email = email, Role = role };
        user.PasswordHash = passwords.Hash(user, r.Password);
        db.Users.Add(user); await db.SaveChangesAsync();
        return Created($"/api/profile", new { user.Id, user.FullName, user.Email, user.Role });
    }
    /// <summary>Verify credentials and establish a server-side session.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest r)
    {
        var user = await db.Users.SingleOrDefaultAsync(x => x.Email == r.Email.Trim().ToLowerInvariant());
        if (user is null || !passwords.Verify(user, user.PasswordHash, r.Password)) return Unauthorized(new { message = "Invalid email or password." });
        HttpContext.Session.SetInt32("UserId", user.Id); HttpContext.Session.SetString("Role", user.Role);
        return Ok(new { user.Id, user.FullName, user.Email, user.Role, message = "Logged in." });
    }
    /// <summary>End the current session.</summary>
    [HttpPost("logout")]
    public IActionResult Logout() { HttpContext.Session.Clear(); return NoContent(); }
    /// <summary>Return the current session identity.</summary>
    [HttpGet("me")]
    public async Task<IActionResult> Me() { var id=HttpContext.Session.GetInt32("UserId"); if(id is null) return Unauthorized(); var u=await db.Users.FindAsync(id); return u is null ? Unauthorized() : Ok(new {u.Id,u.FullName,u.Email,u.Role}); }
}
