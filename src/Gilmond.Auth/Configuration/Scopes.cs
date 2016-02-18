using IdentityServer4.Core.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Gilmond.Auth.Configuration
{
	internal sealed class Scopes
	{
		internal static IEnumerable<Scope> Get(IConfigurationRoot configuration)
		{
			yield return StandardScopes.ProfileAlwaysInclude;
			yield return StandardScopes.RolesAlwaysInclude;
			yield return new Scope
			{
				Name = "Gilmond.Api",
				DisplayName = "Gilmond API",
				Description = "Features and Data provided by the secure Gilmond Web API",
				Type = ScopeType.Resource,
				ScopeSecrets = new List<Secret>(1)
				{
					new Secret(configuration["Security:Resources:Api:Secret"].Sha256())
				}
			};
		}
	}
}
