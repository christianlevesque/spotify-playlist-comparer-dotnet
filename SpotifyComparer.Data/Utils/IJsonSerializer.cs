namespace SpotifyComparer.Utils;

public interface IJsonSerializer
{
	string Serialize(object input);

	T? Deserialize<T>(string input)
		where T : class;
}
