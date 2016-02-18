using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Gilmond.Auth
{
	internal static class ServiceExtensions
	{
		internal static IServiceCollection AddSecurity(this IServiceCollection services, IApplicationEnvironment environment, IConfigurationRoot configuration)
		{
			try
			{
				var cert = new X509Certificate2(
					Path.Combine(environment.ApplicationBasePath, $"{configuration["Security:PfxName"]}.pfx"),
					configuration["Security:Password"]);

				var builder = services.AddIdentityServer(options =>
				{
					options.SigningCertificate = cert;
				});

				builder.AddInMemoryClients(Configuration.Clients.Get());
				builder.AddInMemoryScopes(Configuration.Scopes.Get(configuration));
			}
			catch (Exception)
			{
				// intentionally drop security details contained in thrown exception
				throw new Exception("Could not configure security.");
			}

			return services;
		}
	}
}
