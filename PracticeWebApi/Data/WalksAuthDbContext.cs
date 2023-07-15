using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace PracticeWebApi.Data
{
	public class WalksAuthDbContext : IdentityDbContext
	{
		public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) 
			: base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "36c2e8d5-c6a9-4447-81ff-cbcc91813cd7";
			var writerRoleId = "e9716b6a-25b6-4edc-8cf3-7c1b099cce01";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = readerRoleId,
					ConcurrencyStamp = readerRoleId,
					Name = "reader",
					NormalizedName= "reader".ToUpper()
				},
				new IdentityRole
				{
					Id = writerRoleId,
					ConcurrencyStamp = writerRoleId,
					Name = "writer",
					NormalizedName= "writer".ToUpper()
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);	
		}
	}
}
