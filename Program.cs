using FluentValidation;
using FluentValidation.AspNetCore;
using GymAndYou.AutorizationRules;
using GymAndYou.DatabaseConnection;
using GymAndYou.DTO_Models;
using GymAndYou.DTO_Models.Validators;
using GymAndYou.Entities;
using GymAndYou.Middleware;
using GymAndYou.Models.DTO_Models;
using GymAndYou.Models.DTO_Models.Validators;
using GymAndYou.Services;
using GymAndYou.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

//Automapper config
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Authentication
    
    var authenticationSettings = new AuthenticationSettings();
    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    builder.Services.AddAuthentication(option => {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(cfg => 
    { 
        cfg.RequireHttpsMetadata = true;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        };
    });

// Authorization
    builder.Services.AddAuthorization(option => 
    { 
        option.AddPolicy("MinimumDaysSinceRegister",builder =>
                        builder.AddRequirements(new MinimumDaysSinceCreateAccount(Static.Minimum_Days_Since_Account_Create)));
        
    });
    builder.Services.AddScoped<IAuthorizationHandler,MinimumDaysSinceCreateAccountHandler>();

// Add services to the container.

    //Middleware services
    builder.Services.AddScoped<ExceptionHandler>();
    builder.Services.AddSwaggerGen();

    //Controllers services
    builder.Services.AddScoped<IGymService,GymService>();
    builder.Services.AddScoped<IEquipmentService,EquipmentService>();
    builder.Services.AddScoped<IMemberService,MemberService>();
    builder.Services.AddScoped<IFileService,FileService>();
    builder.Services.AddScoped<AccountService>();

    //Validators services
    builder.Services.AddScoped<IValidator<UpsertMemberDTO>,AddMemberDtoValidator>();
    builder.Services.AddScoped<IValidator<UpsertEquipmentDTO>,AddEquipmentDTOValidator>();
    builder.Services.AddScoped<IValidator<GymQuery>,GymQueryValidator>();
    builder.Services.AddScoped<IValidator<CreateUserDTO>,CreateUserDTOValidator>();

    //Database services
    builder.Services.AddDbContext<DbConnection>(option=>
    { 
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Services.AddScoped<DatabaseSeeder>();

    //CORSE Settings
    builder.Services.AddCors(option => 
    {
        option.AddPolicy("FrontEndApplication", policyBuilder => 
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
            policyBuilder.WithOrigins(builder.Configuration["AllowedOrigin"]);
        });
    });

    //Additional services
    builder.Services.AddControllers();
    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();



var app = builder.Build();


var scope = app.Services.CreateScope();
var DbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

DbSeeder.SeedDatabase();

// Configure the HTTP request pipeline.

app.UseResponseCaching();

app.UseCors("FrontEndApplication");

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandler>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI( c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymAndYou");
} );

app.UseAuthorization();

app.MapControllers();

app.Run();
