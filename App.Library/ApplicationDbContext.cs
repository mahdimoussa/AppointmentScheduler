
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Library.Models;

	namespace App.Library
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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