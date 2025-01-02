using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SpotifyComparer.Utils;

public class DefaultJsonSerializer : IJsonSerializer
{
	private readonly ILogger<DefaultJsonSerializer> _logger;

	private JsonSerializerOptions _options = new ()
	{
		PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
	};

	public DefaultJsonSerializer(ILogger<DefaultJsonSerializer> logger)
	{
		_logger = logger;
	}

	public string Serialize(object input)
	{
		return JsonSerializer.Serialize(input, _options);
	}

	public T? Deserialize<T>(string input)
		where T : class
	{
		try
		{
			return JsonSerializer.Deserialize<T>(input, _options);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Failed to deserialize JSON input");
			return null;
		}
	}
}
