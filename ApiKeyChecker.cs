using MediaMather.Models;
using System.Text.Json;

namespace MediaMather
{
    public class ApiKeyChecker
	{
		public static bool checkApiKey(string key, DatabaseContext db)
		{
			Console.WriteLine("validating token");
			if (key == null)
			{
				return false;
			}
			Console.WriteLine($"extracted token {key}");
			Console.WriteLine($"existing tokens: {JsonSerializer.Serialize(db.Tokens.ToArray())}");
			if (db.Tokens.Any<Token>((token) => token.AuthToken == key))
			{
				return true;
			}

			return false;
		}
	}
}

