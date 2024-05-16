using Api.Models.Domain.Interfaces;
using Api.Models.Mapper;
using Api.services;
using Api.services.Repository;
using AutoMapper;

namespace Api.Extensions;

public static class InjectionExtensions
{
	public static IServiceCollection AddInjectionApi(this IServiceCollection services)
	{
		// Account
		services.AddTransient<IAccountService, AccountService>();
		services.AddTransient<IAccountRepository, AccountRepository>();

		// Task
		services.AddTransient<ITaskServices, TaskServices>();
		services.AddTransient<ITaskRepository, TaskRepository>();

		// Mapper Config
		var mapperConfig = new MapperConfiguration(mc =>
		{
			mc.AddProfile(new AutoMapperProfiles());
		});
		IMapper mapper = mapperConfig.CreateMapper();
		services.AddSingleton(mapper);
		services.AddTransient<IAccountService, AccountService>();

		/* var config = new MapperConfiguration(cfg => */
		/* { */
		/*     cfg.AddMaps(typeof(AutoMapperProfiles).Assembly); */
		/* }); */

		/* builder.Services.AddSingleton(config.CreateMapper()); */

		return services;
	}
}
