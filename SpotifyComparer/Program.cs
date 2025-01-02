using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotifyComparer;
using SpotifyComparer.Programs;

Setup();

var host = Host
	.CreateDefaultBuilder(args)
	.ConfigureServices((context, services) =>
	{
		services
			.AddSingleton<HttpClient>()
			.AddSpotifyData(context.Configuration)
			.AddSingleton<PlaylistReader>();
	})
	.ConfigureLogging(l => l.ClearProviders().AddConsole())
	.Build();

using var scope = host.Services.CreateScope();
var sp = scope.ServiceProvider;

var program = sp.GetRequiredService<PlaylistReader>();

var myPlaylistId = "5mqGfLkxNfltlXkyCjbwBS";
var dadsPlaylistId = "17qqowXCtlv6v44wY4i0jU";

try
{
	await program.GetAllTracks(myPlaylistId);
}
catch (Exception) {}
finally
{
	await host.StopAsync();
}

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