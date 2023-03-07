using FluentValidation;
using FluentValidation.AspNetCore;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.DTO_Models.Validators;
using GymAndYou.Entities;
using GymAndYou.Middleware;
using GymAndYou.Services;
using Microsoft.AspNetCore.Builder;
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

    //Middleware services
    builder.Services.AddScoped<ExceptionHandler>();
    builder.Services.AddSwaggerGen();

    //Controllers services
    builder.Services.AddScoped<IGymService,GymService>();
    builder.Services.AddScoped<IEquipmentService,EquipmentService>();
    builder.Services.AddScoped<IMemberService,MemberService>();
    builder.Services.AddScoped<IFileService,FileService>();

    //Validators services
    builder.Services.AddScoped<IValidator<UpsertMemberDTO>,AddMemberDtoValidator>();
    builder.Services.AddScoped<IValidator<UpsertEquipmentDTO>,AddEquipmentDTOValidator>();
    builder.Services.AddScoped<IValidator<GymQuery>,GymQueryValidator>();

    //Database services
    builder.Services.AddDbContext<DbConnection>(option=>
    { 
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Services.AddScoped<DatabaseSeeder>();

    //Additional services
    builder.Services.AddControllers();
    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();



var app = builder.Build();


var scope = app.Services.CreateScope();
var DbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

DbSeeder.SeedDatabase();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI( c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymAndYou");
} );

app.UseAuthorization();

app.MapControllers();

app.Run();
