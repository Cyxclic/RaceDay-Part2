namespace RaceDay.API.Models;
public class UserProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Phone { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? EmergencyContact { get; set; }
    public User? User { get; set; }
}
