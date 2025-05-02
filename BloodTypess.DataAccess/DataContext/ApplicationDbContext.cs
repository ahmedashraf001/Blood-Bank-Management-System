using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.DataContext
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		//public DbSet<AppUser> AppUsers { get; set; }
		/*	public DbSet<Donor> Donors { get; set; }
			public DbSet<BloodStock> BloodStocks { get; set; }*/

		public DbSet<BloodType> BloodTypes { get; set; } = null!;

		public DbSet<BloodTypeStock> BloodTypesStock { get; set; } = null!;
		public DbSet<Donor> Donors { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Configure BloodType Stock entity
			builder.Entity<BloodTypeStock>()
				.HasKey(b => b.Id);
			builder.Entity<BloodTypeStock>()
				.HasOne(b => b.BloodType)
				.WithOne(b=>b.BloodTypeStock)
				.HasForeignKey<BloodTypeStock>(b => b.BloodTypeId);


			// Configure Donor entity
			builder.Entity<Donor>()
				.HasKey(d => d.Id);
			builder.Entity<Donor>()
				.HasOne(d => d.bloodType)
				.WithMany(d=> d.Donors)
				.HasForeignKey(d => d.BloodTypeId);


			// Configure BloodType entity (The Broker btw them == Lookup Table)
			builder.Entity<BloodType>()
				   .HasKey(b => b.Id);

			builder.Entity<BloodType>().HasData(
				new BloodType { Id = 1, Type = "A+" },
				new BloodType { Id = 2, Type = "A-" },
				new BloodType { Id = 3, Type = "B+" },
				new BloodType { Id = 4, Type = "B-" },
				new BloodType { Id = 5, Type = "AB+" },
				new BloodType { Id = 6, Type = "AB-" },
				new BloodType { Id = 7, Type = "O+" },
				new BloodType { Id = 8, Type = "O-" }
			);



		}
	}
}
