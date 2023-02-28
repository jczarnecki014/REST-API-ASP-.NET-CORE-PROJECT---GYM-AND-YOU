using GymAndYou.DatabaseConnection;
using GymAndYou.Middleware;
using GymAndYou.Services;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

//Automapper config
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// Add services to the container.
builder.Services.AddScoped<ExceptionHandler>();
builder.Services.AddScoped<GymService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<DbConnection>(option=>
{ 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<DatabaseSeeder>();


var app = builder.Build();


var scope = app.Services.CreateScope();
var DbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

DbSeeder.SeedDatabase();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
