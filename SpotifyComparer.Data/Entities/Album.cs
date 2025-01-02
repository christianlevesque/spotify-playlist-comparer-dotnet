using System.Collections.Generic;

namespace SpotifyComparer.Entities;

/// <summary>
/// Represents album data from the Spotify API
/// </summary>
public class Album
{
	/// <summary>
	/// The Spotify ID for the album
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The name of the album. In case of an album takedown, the value may be an empty string
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// The date the album was first released
	/// </summary>
	public required string ReleaseDate { get; set; }

	/// <summary>
	/// The type of the album
	/// </summary>
	public required string AlbumType { get; set; }

	/// <summary>
	/// The number of tracks on the album
	/// </summary>
	public required int TotalTracks { get; set; }

	/// <summary>
	/// The markets in which the album is available based on their ISO 3166-1 alpha-2 country codes
	/// </summary>
	public required List<string> AvailableMarkets { get; set; }

	/// <summary>
	/// A link to the Web API endpoint providing full details of the album
	/// </summary>
	public required string Href { get; set; }

	/// <summary>
	/// The Spotify URI for the album
	/// </summary>
	public required string Uri { get; set; }

	/// <summary>
	/// The artists of the album
	/// </summary>
	public required List<Artist> Artists { get; set; }
}
