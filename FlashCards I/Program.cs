using FlashCards;
using FlashCards.Entities;
using FlashCards.Services;
using FlashCards_I.Entities;
using FlashCards_I.Middleware;
using FlashCards_I.Models;
using FlashCards_I.Models.Validators;
using FlashCards_I.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text.Json.Serialization;
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


builder.Services.AddDbContext<FlashCardsDbContext>();
builder.Services.AddScoped<FlashCardSeeder>();
builder.Services.AddScoped<IFlashCardsService,FlashCardsSetService>();
builder.Services.AddScoped<IFlashcardService, FlashCardService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegistrationUDto>, RegisterUDtoValidator>();
builder.Services.AddControllers().AddJsonOptions(option=>
option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddFluentValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FlashCardSeeder>();
seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FlashCard Api");
});

app.UseAuthorization();


app.MapControllers();

app.Run();
