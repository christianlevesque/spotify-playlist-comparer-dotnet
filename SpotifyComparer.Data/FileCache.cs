using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpotifyComparer.Utils;

namespace SpotifyComparer;

public class FileCache
{
	private readonly ILogger<FileCache> _logger;
	private readonly IJsonSerializer _jsonSerializer;

	public FileCache(
		ILogger<FileCache> logger,
		IJsonSerializer jsonSerializer)
	{
		_logger = logger;
		_jsonSerializer = jsonSerializer;
	}

	public async Task<T?> Read<T>(string filename)
		where T : class
	{
		filename = GetFullFilename(filename);
		if (File.Exists(filename))
		{
			_logger.LogInformation("Cache hit for file {filename}", filename);
			var contents = await File.ReadAllTextAsync(filename);
			return _jsonSerializer.Deserialize<T>(contents);
		}

		return null;
	}

	public async Task<bool> Write(string filename, object input)
	{
		filename = GetFullFilename(filename);

		try
		{
			var json = _jsonSerializer.Serialize(input);
			await File.WriteAllTextAsync(filename, json);
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Failed to write to file cache");
			return false;
		}
	}

	private static string GetFullFilename(string filename)
	{
		return $"{Constants.CacheDirectory}/{filename}.json";
	}
}
