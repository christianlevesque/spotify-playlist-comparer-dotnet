namespace SpotifyComparer;

public class SpotifyApiOptions
{
	/// <summary>
	/// The ID of the client application to use to get an auth token
	/// </summary>
	public required string ClientId { get; set; }

	/// <summary>
	/// The secret key of the client application to use to get an auth token
	/// </summary>
	public required string ClientSecret { get; set; }
}
