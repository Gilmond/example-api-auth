using System.Collections.Generic;
using IdentityServer4.Core.Models;

namespace Gilmond.Auth.Configuration
{
	internal sealed class Clients
	{
		internal static IEnumerable<Client> Get()
		{
			yield return new Client
			{
				ClientId = "website",
				ClientName = "Gilmond Website Javascript OAuth 2.0 Client",
				ClientUri = "https://github.com/gilmond/example-api-auth",
				Flow = Flows.Implicit,
				RedirectUris = new List<string>(1)
				{
					"https://localhost:5001/index.html"
				},
				AllowedScopes = new List<string>(2)
				{
					"guest", "customer"
				}
			};

			// intentionally omitting oidc for now
		}
	}
}
