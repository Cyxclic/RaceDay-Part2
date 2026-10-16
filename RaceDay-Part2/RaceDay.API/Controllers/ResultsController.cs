using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using RaceDay.API.Data; using RaceDay.API.DTOs; using RaceDay.API.Models;
namespace RaceDay.API.Controllers;
[ApiController,Route("api/results")]
public class ResultsController(RaceDayContext db):ControllerBase
{
 [HttpPost] public async Task<IActionResult> Create(ResultRequest r){var uid=HttpContext.Session.GetInt32("UserId");if(uid is null)return Unauthorized();if(HttpContext.Session.GetString("Role")!="Organiser")return Forbid();if(r.FinishTime<TimeSpan.Zero)return BadRequest(new{message="Finish time cannot be negative."});var en=await db.Enrolments.Include(x=>x.Category).ThenInclude(x=>x!.Event).Include(x=>x.Result).SingleOrDefaultAsync(x=>x.Id==r.EnrolmentId);if(en is null)return NotFound();if(en.Category!.Event!.OrganiserId!=uid)return Forbid();if(en.Result is not null)return Conflict(new{message="A result already exists for this enrolment."});var result=new Result{EnrolmentId=en.Id,FinishTime=r.FinishTime,Position=r.Position};db.Results.Add(result);await db.SaveChangesAsync();return Created($"/api/results/my",result);}
 [HttpGet("my")] public async Task<IActionResult> Mine(){var uid=HttpContext.Session.GetInt32("UserId");if(uid is null)return Unauthorized();if(HttpContext.Session.GetString("Role")!="Participant")return Forbid();return Ok(await db.Results.Include(x=>x.Enrolment).ThenInclude(x=>x!.Category).ThenInclude(x=>x!.Event).Where(x=>x.Enrolment!.ParticipantId==uid).ToListAsync());}
}
