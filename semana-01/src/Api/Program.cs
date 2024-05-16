using Api.Data;
using Api.Extensions;
using Api.Models.Dto.Account;
using Api.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuation = builder.Configuration;
builder.Services.addAuthenticationJwt(configuation);
builder.Services.AddInjectionApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Add services for Validation
builder.Services.AddTransient<AccountValidation>();
builder.Services.AddTransient<IValidator<AccountLoginRequestDto>, AccountValidation>();

builder.Services.AddDbContext<Semana01Context>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("StringConnection"));
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(
		name: MyAllowSpecificOrigins,
		builder =>
		{
			builder.WithOrigins("*");
			builder.AllowAnyHeader();
			builder.WithMethods("*");
		}
	);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
