namespace RaceDay.API.Models;
public class Enrolment
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int CategoryId { get; set; }
    public DateTime EnrolmentDate { get; set; } = DateTime.UtcNow;
    public User? Participant { get; set; }
    public Category? Category { get; set; }
    public Result? Result { get; set; }
}
