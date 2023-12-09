using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MediaMather
{
	public static class MathHandler
	{
		public static async Task<IResult> Determine(Func<Operands, double?> method, Operands ops, string apiKey, DatabaseContext? db)
		{

			if ( db != null && !ApiKeyChecker.checkApiKey(apiKey, db))
			{
				return Results.Unauthorized();
			}
			double? mathResult = null;
			IResult requestResult;
			try
			{
				mathResult = method(ops);

				if (mathResult == null) { return Results.NoContent(); }

				double definiteMathResult = mathResult ?? default(double);


				requestResult = Results.Ok<Dictionary<string, double>>(new Dictionary<string, double> { { "result", definiteMathResult } });
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				requestResult = Results.BadRequest();
			}
			if (db != null)
			{
				db.Add(new Audit { Operation = method.GetMethodInfo().Name, X = ops.X, Y = ops.Y, Result = mathResult, DateTime = DateTime.Now });
				await db.SaveChangesAsync();
			}
			return requestResult;
		}
	}
}
