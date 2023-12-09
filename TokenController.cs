namespace MediaMather
{
	public class TokenController
	{
		public static async Task<IResult> GetNewToken(int length, DatabaseContext db)
		{
			string token = "";
			Random random = new Random();
			for (int i = 0; i< length; i++)
			{
				token += GenerateChar(random);
			}
			db.Tokens.Add(new Token { AuthToken = token });
			await db.SaveChangesAsync();
			IResult result = Results.Ok<string>(token);
			return result;
		}

		public static char GenerateChar(Random random)
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			return Convert.ToChar(chars[Convert.ToInt16(Math.Floor(random.NextDouble() * 52))]);
		}
	}
}
