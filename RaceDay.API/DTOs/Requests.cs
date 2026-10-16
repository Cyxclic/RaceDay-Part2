using System.ComponentModel.DataAnnotations;
namespace RaceDay.API.DTOs;
public record RegisterRequest([Required,MaxLength(100)] string FullName,[Required,EmailAddress,MaxLength(256)] string Email,[Required,MinLength(8)] string Password,[Required] string Role);
public record LoginRequest([Required,EmailAddress] string Email,[Required] string Password);
public record EventRequest([Required,MaxLength(150)] string Name,[Required,MaxLength(2000)] string Description,DateTime Date,[Required,MaxLength(200)] string Location,[Range(0.1,1000)] decimal DistanceKm,[Required] string EventType);
public record CategoryRequest([Required,MaxLength(100)] string Name,string? Criteria);
public record EnrolmentRequest([Range(1,int.MaxValue)] int CategoryId);
public record ResultRequest([Range(1,int.MaxValue)] int EnrolmentId,TimeSpan FinishTime,[Range(1,int.MaxValue)] int Position);
public record ProfileRequest(string? Phone,DateOnly? DateOfBirth,string? EmergencyContact);
