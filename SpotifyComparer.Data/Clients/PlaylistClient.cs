using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyComparer.Entities;

namespace SpotifyComparer.Clients;

public class PlaylistClient
{
	private readonly SpotifyClient _client;
	private readonly FileCache _fileCache;

	public PlaylistClient(
		SpotifyClient client,
		FileCache fileCache)
	{
		_client = client;
		_fileCache = fileCache;
	}

	public async Task<List<Track>> GetTracks(string playlistId)
	{
		var filename = $"playlists/{playlistId}";
		var playlist = await _fileCache.Read<PagedResult<PlaylistTrack>>(filename);

		if (playlist is null)
		{
			
			var url = $"playlists/{playlistId}/tracks?offset=0&limit=100";
			playlist = await _client.SendRequest<PagedResult<PlaylistTrack>>(url);

			if (playlist is not null)
			{
				await _fileCache.Write(filename, playlist);
			}
		}

		return playlist?.Items.Select(i => i.Track).ToList() ?? [];
	}
}
