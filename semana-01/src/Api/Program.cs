using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();
IConfiguration Configurations = app.Configuration;
var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<Semana01Context>(options =>
{
    options.UseNpgsql(
        Configurations.GetConnectionString("Semana01"),
        b => b.MigrationsAssembly(assembly)
        );
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

