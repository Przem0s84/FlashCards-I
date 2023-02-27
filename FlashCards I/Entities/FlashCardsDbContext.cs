using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FlashCards.Entities
{
    public class FlashCardsDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=CardsDb;Trusted_Connection=True;";
        public FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : base(options)
        {

        }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<WordAndDef> WordAndDefs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stack>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(30);


        }


        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


                
        

    }
}
