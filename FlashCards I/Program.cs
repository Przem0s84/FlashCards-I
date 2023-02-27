using FlashCards;
using FlashCards.Entities;
using FlashCards.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FlashCardsDbContext>();
builder.Services.AddScoped<FlashCardSeeder>();
builder.Services.AddScoped<FlashCardsService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FlashCardSeeder>();
seeder.Seed();
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
