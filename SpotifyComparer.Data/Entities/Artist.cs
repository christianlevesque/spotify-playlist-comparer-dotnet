namespace SpotifyComparer.Entities;

/// <summary>
/// Represents artist data from the Spotify API
/// </summary>
public class Artist
{
	/// <summary>
	/// The Spotify ID for the artist
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The name of the artist
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// The Spotify URI for the artist
	/// </summary>
	public required string Uri { get; set; }
}
