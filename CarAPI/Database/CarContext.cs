using System;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace CarAPI.Database
{
	public class CarContext : DbContext
	{

        public DbSet<Car> Cars { get; set; }
        public DbSet<Telemetry> Telemetries { get; set; }

        public CarContext(DbContextOptions options) : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //}
    }
}

