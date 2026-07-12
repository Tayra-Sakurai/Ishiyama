using Kara.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Contexts
{
    public class KaraContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SmallCategory> SmallCategories { get; set; }
        public DbSet<LargeCategory> LargeCategories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActionType> Actions { get; set; }
        public DbSet<Location> Locations { get; set; }

        public KaraContext(DbContextOptions<KaraContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasIndex(e => e.Name)
                .IsUnique();
            modelBuilder.Entity<Item>();
            modelBuilder.Entity<Log>();
            modelBuilder
                .Entity<ActionType>()
                .HasIndex(e => e.Name)
                .IsUnique();
            modelBuilder.Entity<LargeCategory>();
            modelBuilder.Entity<SmallCategory>();
            modelBuilder
                .Entity<Location>()
                .HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
