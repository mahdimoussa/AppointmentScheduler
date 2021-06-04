
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library.Models
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :

			base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Lookup> Lookups { get; set; }

	}
}