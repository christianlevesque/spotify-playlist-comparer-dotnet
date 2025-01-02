using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotifyComparer.Entities;
using SpotifyComparer.Utils;

namespace SpotifyComparer.Clients;

public class SpotifyClient
{
	private readonly HttpClient _client;
	private readonly IJsonSerializer _jsonSerializer;
	private readonly SpotifyApiOptions _options;
	private readonly ILogger<SpotifyClient> _logger;

	private bool _authenticated;

	public SpotifyClient(
		HttpClient client,
		IJsonSerializer jsonSerializer,
		IOptions<SpotifyApiOptions> options,
		ILogger<SpotifyClient> logger)
	{
		_client = client;
		_jsonSerializer = jsonSerializer;
		_options = options.Value;
		_logger = logger;
	}

	public async Task<T?> SendRequest<T>(string url)
		where T : class
	{
		if (!_authenticated && !await Authenticate())
		{
			throw new Exception("Unable to authenticate to Spotify API");
		}

		try
		{
			var response = await _client.GetAsync($"https://api.spotify.com/v1/{url}");
			if (!response.IsSuccessStatusCode) return null;

			return _jsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Unable to get data from Spotify API");
			return null;
		}
	}

	private async Task<bool> Authenticate()
	{
		if (_authenticated) return true;

		try
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuth());
			var content = new FormUrlEncodedContent([new KeyValuePair<string, string>("grant_type", "client_credentials")]);

			var response = await _client.PostAsync(
				"https://accounts.spotify.com/api/token",
				content);

			if (!response.IsSuccessStatusCode)
			{
				return false;
			}

			var responseString = await response.Content.ReadAsStringAsync();
			var accessToken = (_jsonSerializer.Deserialize<Auth>(responseString))?.AccessToken;
			_authenticated = !string.IsNullOrEmpty(accessToken);

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			return _authenticated;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Failed to authenticate to the Spotify API");
			return false;
		}
	}

	private string GetBasicAuth()
	{
		var bytes = Encoding.UTF8.GetBytes($"{_options.ClientId}:{_options.ClientSecret}");
		return Convert.ToBase64String(bytes);
	}
}
