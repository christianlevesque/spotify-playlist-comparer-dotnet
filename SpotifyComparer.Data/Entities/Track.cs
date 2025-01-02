using System.Collections.Generic;

namespace SpotifyComparer.Entities;

/// <summary>
/// Represents track data from the Spotify API
/// </summary>
public class Track
{
	/// <summary>
	/// The Spotify ID for the track
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// The name of the track
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// A link to the Web API endpoint providing full details of the track
	/// </summary>
	public required string Href { get; set; }

	/// <summary>
	/// The Spotify URI for the track
	/// </summary>
	public string? Uri { get; set; }

	/// <summary>
	/// The album on which the track appears
	/// </summary>
	public required Album Album { get; set; }

	/// <summary>
	/// The artists who performed the track
	/// </summary>
	public required List<Artist> Artists { get; set; }

	/// <summary>
	/// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code
	/// </summary>
	public required List<string> AvailableMarkets { get; set; }

	/// <summary>
	/// The disc number (usually 1 unless the album consists of more than one disc)
	/// </summary>
	public int DiscNumber { get; set; } = 1;

	/// <summary>
	/// The number of the track. If an album has several discs, the track number is the number on the specified disc
	/// </summary>
	public int TrackNumber { get; set; }

	/// <summary>
	/// The track length in milliseconds
	/// </summary>
	public int DurationMs { get; set; }

	/// <summary>
	/// The popularity of the track between 0 and 100, with 100 being the most popular 
	/// </summary>
	public int Popularity { get; set; }

	/// <summary>
	/// Whether or not the track has explicit lyrics. <c>false</c> may mean either that it does not, or that it is unknown whether it does
	/// </summary>
	public bool Explicit { get; set; }

	/// <summary>
	/// Part of the response when Track Relinking is applied. If <c>true</c>, the track is playable in the given market
	/// </summary>
	public bool IsPlayable { get; set; }

	/// <summary>
	/// Whether or not the track is from a local file
	/// </summary>
	public bool IsLocal { get; set; }
}
