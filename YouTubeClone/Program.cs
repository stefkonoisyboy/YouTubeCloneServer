using Microsoft.EntityFrameworkCore;
using YouTubeClone.Data;
using YouTubeClone.Services;
using YouTubeClone.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer());

builder.Services.AddCors();

// Business logic
builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
       .WithOrigins(new[] { "https://localhost:44398" })
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
