using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions options)
        :base(options)
        {
            
        }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }   
    public DbSet<CategoryandSubcategory> CategorySubcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             builder.Entity<CategoryandSubcategory>()
            .HasKey(cs => new { cs.CategoryId, cs.SubcategoryId });

        builder.Entity<CategoryandSubcategory>()
            .HasOne(cs => cs.Category)
            .WithMany(c => c.CategorySubcategories)
            .HasForeignKey(cs => cs.CategoryId);

        builder.Entity<CategoryandSubcategory>()
            .HasOne(cs => cs.Subcategory)
            .WithMany(s => s.CategorySubcategories)
        .HasForeignKey(cs => cs.SubcategoryId);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName="USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }

}