using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpotifyComparer.Clients;

namespace SpotifyComparer.Programs;

public class PlaylistReader
{
	private readonly PlaylistClient _client;
	private readonly ILogger<PlaylistReader> _logger;

	public PlaylistReader(
		PlaylistClient client,
		ILogger<PlaylistReader> logger)
	{
		_client = client;
		_logger = logger;
	}

	public async Task Run()
	{
		var tracks = await _client.GetTracks("5mqGfLkxNfltlXkyCjbwBS");

		foreach (var track in tracks)
		{
			_logger.LogInformation(
				"Track: {name} | {album} | {artist}",
				track.Name,
				track.Album.Name,
				string.Join(", ", track.Artists.Select(a => a.Name)));
		}

		// Without this line, the code exits early 
		Console.ReadLine();
	}
}
