using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Environment
var environment = new ProjectEnvironment
{
    DatabaseConnection = builder.Configuration.GetConnectionString("DatabaseConnection"),
};

// Add services to the container.

//Add Interfaces
builder.Services.AddSingleton<ProjectEnvironment>(environment);
builder.Services.AddSingleton(environment);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(option =>
{
    option.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
});
app.Run();

