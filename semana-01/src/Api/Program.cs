using Api.Data;
using Api.Models.Domain.Interfaces;
using Api.Models.Mapper;
using Api.Services;
using Api.Services.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

// var mapperConfig = new MapperConfiguration(mc =>
// {
//     mc.AddProfile(new AutoMapperProfiles());
// });

// IMapper mapper = mapperConfig.CreateMapper();
// builder.Services.AddSingleton(mapper);
// builder.Services.AddTransient<IAccountService, AccountService>();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(typeof(AutoMapperProfiles).Assembly);
});

builder.Services.AddSingleton(config.CreateMapper());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
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
app.MapControllers();

app.Run();
