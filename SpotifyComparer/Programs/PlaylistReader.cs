using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpotifyComparer.Clients;
using SpotifyComparer.Entities;

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

	public async Task FindSharedSongs(string playlistId1, string playlistId2)
	{
		var playlistTracklist1 = await _client.GetTracks(playlistId1);
		var playlistTracklist2 = await _client.GetTracks(playlistId2);

		var sharedTracks = new List<Track>();

		foreach (var track in playlistTracklist1)
		{
			var duplicate = playlistTracklist2.FirstOrDefault(t => t.Id == track.Id);
			if (duplicate is not null)
			{
				sharedTracks.Add(duplicate);
			}
		}

		if (sharedTracks.Count == 0)
		{
			_logger.LogInformation("There are no duplicate tracks between the provided playlists");
		}
		else
		{
			_logger.LogInformation("Shared tracks:");
			PrintTracks(sharedTracks);
		}
	}

	public async Task FindSharedArtists(string playlistId1, string playlistId2)
	{
		var playlistTracklist1 = await _client.GetTracks(playlistId1);
		var playlistArtistList1 = new List<Artist>();
		AddArtistsToUniqueList(playlistTracklist1, playlistArtistList1);

		var playlistTracklist2 = await _client.GetTracks(playlistId2);
		var playlistArtistList2 = new List<Artist>();
		AddArtistsToUniqueList(playlistTracklist2, playlistArtistList2);

		var sharedArtists = new List<Artist>();

		foreach (var artist in playlistArtistList1)
		{
			if (playlistArtistList2.Any(a => a.Id == artist.Id))
			{
				sharedArtists.Add(artist);
			}
		}

		if (sharedArtists.Count == 0)
		{
			_logger.LogInformation("There are no shared artists between the provided playlists.");
		}
		else
		{
			_logger.LogInformation("There are {count} shared artists:", sharedArtists.Count);
			foreach (var artist in sharedArtists.OrderBy(a => a.Name))
			{
				_logger.LogInformation("{name}", artist.Name);
			}
		}
	}

	public async Task FindUniqueArtists(string playlistId)
	{
		var trackList = await _client.GetTracks(playlistId);
		var artistList = new List<Artist>();
		AddArtistsToUniqueList(trackList, artistList);

		_logger.LogInformation("There are {count} unique artists", artistList.Count);

		foreach (var artist in artistList.OrderBy(a => a.Name))
		{
			_logger.LogInformation("{name}", artist.Name);
		}
	}

	public async Task GetAllTracks(string playlistId)
	{
		var trackList = await _client.GetTracks(playlistId);
		_logger.LogInformation("There are {count} total tracks:", trackList.Count);
		PrintTracks(trackList);
	}

	private void PrintTracks(List<Track> tracks)
	{
		foreach (var track in tracks)
		{
			_logger.LogInformation(
				"{name} | {album} | {artist}",
				track.Name,
				track.Album.Name,
				string.Join(", ", track.Artists.Select(a => a.Name)));
		}
	}

	private static void AddArtistsToUniqueList(List<Track> tracks, List<Artist> artists)
	{
		foreach (var track in tracks)
		{
			foreach (var artist in track.Artists)
			{
				if (!artists.Any(a => a.Id == artist.Id))
				{
					artists.Add(artist);
				}
			}
		}
	}
}
