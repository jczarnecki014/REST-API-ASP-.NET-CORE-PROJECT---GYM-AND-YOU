using GymAndYou.DatabaseConnection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
