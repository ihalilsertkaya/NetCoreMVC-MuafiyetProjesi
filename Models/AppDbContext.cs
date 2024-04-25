    
using Microsoft.EntityFrameworkCore;

namespace MuafiyetProjesi2024.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { 
        


        }

         public DbSet<Kullanicilar> Kullanicilars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanicilar>().HasKey(k => k.TCKimlik);
        }


    }
}
