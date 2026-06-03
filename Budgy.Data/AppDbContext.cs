using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Data;

public sealed class AppDbContext : DbContext
{
    public const string DatabaseName = "costly.db3";
    public DbSet<EntryTemplate> Entries { get; set; }
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
        modelBuilder.Entity<EntryTemplate>()
            .ToTable(nameof(EntryTemplate));

        modelBuilder.Entity<EntryTemplate>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Entries)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);


        base.OnModelCreating(modelBuilder);
    }
}
