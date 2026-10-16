using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;
namespace RaceDay.API.Data;
public class RaceDayContext(DbContextOptions<RaceDayContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Enrolment> Enrolments => Set<Enrolment>();
    public DbSet<Result> Results => Set<Result>();
    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().HasIndex(x => x.Email).IsUnique();
        b.Entity<User>().Property(x => x.Email).HasMaxLength(256).IsRequired();
        b.Entity<User>().Property(x => x.Role).HasMaxLength(20).IsRequired();
        b.Entity<Event>().Property(x => x.DistanceKm).HasPrecision(8,2);
        b.Entity<User>().HasOne(x=>x.Profile).WithOne(x=>x.User).HasForeignKey<UserProfile>(x=>x.UserId);
        b.Entity<Event>().HasOne(x=>x.Organiser).WithMany(x=>x.OrganisedEvents).HasForeignKey(x=>x.OrganiserId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Category>().HasOne(x=>x.Event).WithMany(x=>x.Categories).HasForeignKey(x=>x.EventId).OnDelete(DeleteBehavior.Cascade);
        b.Entity<Enrolment>().HasOne(x=>x.Participant).WithMany(x=>x.Enrolments).HasForeignKey(x=>x.ParticipantId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Enrolment>().HasOne(x=>x.Category).WithMany(x=>x.Enrolments).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Enrolment>().HasIndex(x=>new {x.ParticipantId,x.CategoryId}).IsUnique();
        b.Entity<Enrolment>().HasOne(x=>x.Result).WithOne(x=>x.Enrolment).HasForeignKey<Result>(x=>x.EnrolmentId);
        b.Entity<Event>().ToTable(t=>t.HasCheckConstraint("CK_Event_Type", "[EventType] IN ('Run','Walk','Cycle')"));
        b.Entity<User>().ToTable(t=>t.HasCheckConstraint("CK_User_Role", "[Role] IN ('Organiser','Participant')"));
    }
}
