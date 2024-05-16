using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AuthenticationExtensions
{
	public static IServiceCollection addAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true,
			ValidIssuer = configuration["JWT:Issuer"],
			ValidAudience = configuration["JWT:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
		};

		services.AddSingleton(tokenValidationParameters);
		services
			.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.RequireHttpsMetadata = false;
				option.SaveToken = true;
				option.TokenValidationParameters = tokenValidationParameters;
			});
		return services;
	}
}
