﻿using Microsoft.EntityFrameworkCore;
using WebShop.ProductApi.Models;

namespace WebShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Category
        mb.Entity<Category>()
            .HasKey(c => c.CategoryId);

        mb.Entity<Category>().
            Property(c => c.Name).
                HasMaxLength(100).
                    IsRequired();

        //Product
        mb.Entity<Product>().
           Property(c => c.Name).
               HasMaxLength(100).
                   IsRequired();

        mb.Entity<Product>().
            Property(c => c.Description).
                HasMaxLength(500).
                    IsRequired();

        mb.Entity<Product>().
            Property(c => c.ImageURL).
                HasMaxLength(255).
                    IsRequired();

        mb.Entity<Product>().
            Property(c => c.Price).
                HasColumnType("decimal(12,2)");

        mb.Entity<Category>().
            HasMany(g => g.Products).
                WithOne(c => c.Category).
                    IsRequired().
                        OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios"
                }
        );

    }
}

