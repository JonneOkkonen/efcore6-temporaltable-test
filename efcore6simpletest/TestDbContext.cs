using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace EFCore6SimpleTest
{
    public class TestDbContext : DbContext
    {
        public DbSet<MainEntity> MainEntities { get; set; }

        public TestDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=efcore6simpletest");
            //optionsBuilder
            //    .EnableSensitiveDataLogging()
            //    .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
        }
    }
}
