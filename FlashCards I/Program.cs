using FlashCards;
using FlashCards.Entities;
using FlashCards.Services;
using FlashCards_I;
using FlashCards_I.Authorization;
using FlashCards_I.Entities;
using FlashCards_I.IServices;
using FlashCards_I.Middleware;
using FlashCards_I.Models;
using FlashCards_I.Models.Validators;
using FlashCards_I.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var authenticationSettings = new AuthenticationSettings();
builder.Services.AddSingleton(authenticationSettings);
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
builder.Services.AddScoped<IAuthorizationHandler, FlashCardSetResourceOperationHandler>();
builder.Services.AddDbContext<FlashCardsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<FlashCardSeeder>();
builder.Services.AddScoped<IFlashCardsService,FlashCardsSetService>();
builder.Services.AddScoped<IFlashcardService, FlashCardService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In=ParameterLocation.Header,
        Description="Enter token",
        Name="Authorization",
        Type=SecuritySchemeType.Http,
        BearerFormat="JWT",
        Scheme="bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
        new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}

        }
    });
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegistrationUDto>, RegisterUDtoValidator>();
builder.Services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
builder.Services.AddControllers().AddJsonOptions(option=>
option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddFluentValidation();
builder.Services.AddScoped<IUserContextService,UserContextService>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FlashCardSeeder>();

if (app.Environment.IsDevelopment())
{
    seeder.Seed();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FlashCard Api");
});

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
