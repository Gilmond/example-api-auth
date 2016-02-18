using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Gilmond.Auth
{
	public class Startup
	{
		private readonly IApplicationEnvironment _environment;
		private readonly IConfigurationRoot _configuration;

		public Startup(IApplicationEnvironment environment, IHostingEnvironment host)
		{
			_environment = environment;

			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();

			if (host.IsDevelopment())
				builder.AddUserSecrets();

			_configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddSecurity(_environment, _configuration)
				.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(_configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseIISPlatformHandler();

			app.UseStaticFiles();

			app.UseMvcWithDefaultRoute();
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
