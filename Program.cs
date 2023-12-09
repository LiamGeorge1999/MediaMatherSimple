using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace MediaMather
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			//builder.Services.AddDbContext<AuditDb>(opt => opt.UseInMemoryDatabase("AuditList"));
			builder.Services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();


			//have to differentiate from the Microsoft.AspNetCore.Mvc.JsonOptions class
			builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
			{
				options.SerializerOptions.IncludeFields = true;
			});
			var app = builder.Build();

			using (var client = new DatabaseContext())
			{
				client.Database.EnsureCreated();
			}


			app.MapPost("/testJson", ([FromBody] Operands operands, [FromServices] DatabaseContext db) => { Console.WriteLine(JsonSerializer.Serialize(operands)); return Results.Ok<Operands>(operands); });

			app.MapGet("/add", async ([FromBody] Operands operands, [FromHeader] string apiKey, [FromServices] DatabaseContext db) => await MathController.Determine(Mather.Add, operands, apiKey, db));
			app.MapGet("/sub", async ([FromBody] Operands operands, [FromHeader] string apiKey, [FromServices] DatabaseContext db) => await MathController.Determine(Mather.Sub, operands, apiKey, db));
			app.MapGet("/mult", async ([FromBody] Operands operands, [FromHeader] string apiKey, [FromServices] DatabaseContext db) => await MathController.Determine(Mather.Mult, operands, apiKey, db));
			app.MapGet("/div", async ([FromBody] Operands operands, [FromHeader] string apiKey, [FromServices] DatabaseContext db) => await MathController.Determine(Mather.Div, operands, apiKey, db));

			app.MapGet("/audits", async ([FromHeader] string apiKey, [FromServices] DatabaseContext db) => await db.Audits.ToListAsync());

			app.MapGet("/key", async ([FromServices] DatabaseContext db) => await TokenController.GetNewToken(64, db));

			app.Run();
		}
	}
}