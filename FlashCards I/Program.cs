using FlashCards;
using FlashCards.Entities;
using FlashCards.Services;
using FlashCards_I.Middleware;
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
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddControllers().AddJsonOptions(option=>
option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
