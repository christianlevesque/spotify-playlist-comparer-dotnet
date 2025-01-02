using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyComparer.Clients;
using SpotifyComparer.Utils;

namespace SpotifyComparer;

public static class SpotifyComparerDataExtensions
{
	public static IServiceCollection AddSpotifyData(
		this IServiceCollection self,
		IConfiguration configuration)
	{
		self
			.Configure<SpotifyApiOptions>(configuration.GetSection("Spotify:Api"))
			.AddSingleton<IJsonSerializer, DefaultJsonSerializer>()
			.AddSingleton<SpotifyClient>()
			.AddSingleton<PlaylistClient>()
			.AddSingleton<FileCache>();

		return self;
	}
}
