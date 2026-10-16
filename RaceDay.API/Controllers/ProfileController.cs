using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using RaceDay.API.Data; using RaceDay.API.DTOs; using RaceDay.API.Models;
namespace RaceDay.API.Controllers;
[ApiController,Route("api/profile")]
public class ProfileController(RaceDayContext db):ControllerBase
{
 [HttpGet] public async Task<IActionResult> Get(){var id=HttpContext.Session.GetInt32("UserId"); if(id is null)return Unauthorized(); var u=await db.Users.Include(x=>x.Profile).SingleOrDefaultAsync(x=>x.Id==id); return u is null?Unauthorized():Ok(new {u.Id,u.FullName,u.Email,u.Role,Profile=u.Profile});}
 [HttpPut] public async Task<IActionResult> Update(ProfileRequest r){var id=HttpContext.Session.GetInt32("UserId"); if(id is null)return Unauthorized(); var p=await db.UserProfiles.SingleOrDefaultAsync(x=>x.UserId==id); if(p is null){p=new UserProfile{UserId=id.Value};db.UserProfiles.Add(p);} p.Phone=r.Phone;p.DateOfBirth=r.DateOfBirth;p.EmergencyContact=r.EmergencyContact;await db.SaveChangesAsync();return Ok(p);}
}
