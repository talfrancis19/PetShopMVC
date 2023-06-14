using FrancisPetShopMVC.Data;
using FrancisPetShopMVC.Data.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FirstMvc.Data
{
    public class MyContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
       
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Comment>()
            modelBuilder.Entity<Animal>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.AnimalItem)
                .HasForeignKey(x => x.AnimalId)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.AnimalItem)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AnimalId)
                .IsRequired();
        }
    }
}




