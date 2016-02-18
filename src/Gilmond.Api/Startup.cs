using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Gilmond.Api
{
	public class Startup
	{
		private readonly IConfigurationRoot _configuration;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();
			_configuration = builder.Build();
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(_configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseIISPlatformHandler();

			app.UseCors(policy =>
			{
				policy.WithOrigins(_configuration["Security:AllowedOrigins"]);
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
			});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			app.UseIdentityServerAuthentication(options =>
			{
				options.Authority = _configuration["Security:AuthServerUri"];
				options.ScopeName = _configuration["Security:Api:ScopeName"];
				options.ScopeSecret = _configuration["Security:Api.ScopeSecret"];
				options.AutomaticAuthenticate = true;
				options.AutomaticChallenge = true;
			});

			app.UseStaticFiles();

			app.UseMvc();
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
