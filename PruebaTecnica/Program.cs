using PruebaTecnica.Concretes.Contexts;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Concretes;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Environment
var environment = new ProjectEnvironment
{
    DatabaseConnection = builder.Configuration.GetConnectionString("DatabaseConnection"),
};

// Add services to the container.
builder.Services.AddDbContext<PruebDbContext>(options =>
    options.UseSqlServer(environment.DatabaseConnection));


//Add Interfaces
builder.Services.AddSingleton<ProjectEnvironment>(environment);
builder.Services.AddSingleton(environment);
builder.Services.AddScoped<IEjemplo, EjemploConcrete>(); //Ejemplo de como aplicar el concrete con la interface
builder.Services.AddScoped<IPersona, PersonaConcrete>();
builder.Services.AddScoped<IEmpresa, EmpresaConcrete>();

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

