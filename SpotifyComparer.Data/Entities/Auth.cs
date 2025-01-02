namespace SpotifyComparer.Entities;

/// <summary>
/// Represents a Spotify API auth token
/// </summary>
public class Auth
{
	/// <summary>
	/// An access token that can be provided in subsequent calls, for example to Spotify Web API services
	/// </summary>
	public required string AccessToken { get; set; }

	/// <summary>
	/// How the access token may be used: always "Bearer"
	/// </summary>
	public required string TokenType { get; set; }

	/// <summary>
	/// The time period (in seconds) for which the access token is valid
	/// </summary>
	public int ExpiresIn { get; set; }
}
