using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models;



var builder = WebApplication.CreateBuilder(args);

// Connect to DB
builder.Services.AddDbContext<db_TankWikiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonDbConnectionString")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
