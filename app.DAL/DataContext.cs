using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace app.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=chatApp;Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasOne(c => c.Role).
                WithMany(c => c.Users)
                .IsRequired();
            modelBuilder.Entity<ChatBox>()
                .HasKey(c => c.MessageId);
            modelBuilder.Entity<UserRole>()
                .HasKey(c => c.roleId);
            
           
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<ChatBox> Chats { get; set; }


    }
}
