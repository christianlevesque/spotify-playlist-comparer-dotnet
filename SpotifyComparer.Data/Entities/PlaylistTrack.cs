namespace SpotifyComparer.Entities;

/// <summary>
/// Represents a playlist's track data from the Spotify API
/// </summary>
public class PlaylistTrack
{
	/// <summary>
	/// The date and time the track was added
	/// </summary>
	public string? AddedAt { get; set; }

	/// <summary>
	/// The track information
	/// </summary>
	public required Track Track { get; set; }
}
