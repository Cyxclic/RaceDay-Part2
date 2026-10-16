using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RaceDay.API.Data;
using RaceDay.API.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "RaceDay API", Version = "v1", Description = "Event management API for Organisers and Participants." }); });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => { o.Cookie.HttpOnly = true; o.Cookie.IsEssential = true; o.Cookie.Name = ".RaceDay.Session"; o.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddDbContext<RaceDayContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("RaceDayConnection")));
builder.Services.AddScoped<PasswordService>();
var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseSession();
app.MapControllers();
app.Run();
public partial class Program { }
