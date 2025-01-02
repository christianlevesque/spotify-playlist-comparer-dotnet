using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpotifyComparer;
using SpotifyComparer.Programs;

Setup();

var sc = new ServiceCollection();

var configBuilder = new ConfigurationBuilder();
configBuilder.AddEnvironmentVariables();
var config = configBuilder.Build();

sc
	.AddOptions()
	.AddSingleton<HttpClient>()
	.AddLogging(o => o.AddConsole())
	.AddSpotifyData(config)
	.AddSingleton<PlaylistReader>();

var sp = sc.BuildServiceProvider();

var program = sp.GetRequiredService<PlaylistReader>();

await program.Run();

return 0;

static void Setup()
{
	if (!Directory.Exists(Constants.CacheDirectory))
	{
		Directory.CreateDirectory(Constants.CacheDirectory);
	}

	var playlistDir = $"{Constants.CacheDirectory}/playlists";
	if (!Directory.Exists(playlistDir))
	{
		Directory.CreateDirectory(playlistDir);
	}
}