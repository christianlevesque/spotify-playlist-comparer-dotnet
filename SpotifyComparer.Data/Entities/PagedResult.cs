using System.Collections.Generic;

namespace SpotifyComparer.Entities;

/// <summary>
/// Wraps around a Spotify API query that returns multiple results
/// </summary>
/// <typeparam name="T">The type of the entity queried</typeparam>
public class PagedResult<T>
{
	/// <summary>
	/// A link to the Web API endpoint returning the full result of the request
	/// </summary>
	public required string Href { get; set; }

	/// <summary>
	/// The maximum number of items in the response (as set in the query or by default)
	/// </summary>
	public required int Limit { get; set; }

	/// <summary>
	/// The offset of the items returned (as set in the query or by default)
	/// </summary>
	public int Offset { get; set; }

	/// <summary>
	/// The total number of items available to return
	/// </summary>
	public int Total { get; set; }

	/// <summary>
	/// URL to the previous page of items. ( null if none)
	/// </summary>
	public string? Previous { get; set; }

	/// <summary>
	/// URL to the next page of items. ( null if none)
	/// </summary>
	public string? Next { get; set; }

	/// <summary>
	/// Array of items returned by the query
	/// </summary>
	public List<T> Items { get; set; }
}
