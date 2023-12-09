using MediaMather.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaMather
{
    public class DatabaseContext : DbContext
	{
		public DbSet<Audit> Audits { get; set; }
		public DbSet<Token> Tokens { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlite("Filename=MediaMather.db");
		}
	}
}
