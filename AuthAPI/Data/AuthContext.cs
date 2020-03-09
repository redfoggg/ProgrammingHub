using System.Linq;
using AuthAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthContext : DbContext
    {
        DbSet<User> Users { get; set; }

        public AuthContext(DbContextOptions<AuthContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            ConfigureModelBuilderForUser(modelBuilder);
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            
            // modelBuilder.Entity<User>()
            //     .Property(user => user.UserName)
            //     .HasMaxLength(60)
            //     .IsRequired();
            
            // modelBuilder.Entity<User>()
            //     .Property(user => user.Email)
            //     .HasMaxLength(60)
            //     .IsRequired();
        }
    }
}