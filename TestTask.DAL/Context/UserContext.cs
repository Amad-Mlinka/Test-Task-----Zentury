	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using TestTask.DAL.Models;

	namespace TestTask.DAL.Context
	{

		public class UserContext : DbContext
		{
			public UserContext(DbContextOptions<UserContext> options) : base(options) { }

			public DbSet<User> Users { get; set; }
			public DbSet<Login> Logins { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<User>()
					.Property(s => s.Id)
					.UseIdentityColumn();

				modelBuilder.Entity<User>()
					.Property(u => u.PasswordHash)
					.HasColumnType("bytea");

				modelBuilder.Entity<User>()
					.Property(u => u.PasswordSalt)
					.HasColumnType("bytea"); 



				modelBuilder.Entity<Login>()
				.HasOne(l => l.User)
				.WithMany(u => u.Logins)
				.HasForeignKey(l => l.UserId) 
				.OnDelete(DeleteBehavior.Cascade); 



				base.OnModelCreating(modelBuilder);
			}

		}
	}