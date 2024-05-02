using Microsoft.EntityFrameworkCore;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
        }

         public DbSet<Kullanicilars> Kullanicilar { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanicilars>().HasKey(k => k.TCKimlik);
        }


    }
}
