using Budgy.Feature.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public sealed class AppDbContext : DbContext
{
    public const string DatabaseName = "costly.db3";
    public DbSet<Entry> Entries { get; set; }
    public DbSet<Category> Categories { get; set; }

    public required string DbPath { get; init; }



    public AppDbContext()
    {
          
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Entries)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);


        base.OnModelCreating(modelBuilder);
    }
}
