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
        public DbSet<FlashCardSet> FlashCardsSets { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlashCardSet>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<FlashCardSet>()
                .HasMany(u => u.flashCards)
                .WithOne(u => u.FlashCardsSet)
                .HasForeignKey(u => u.FlashCardsSetId);
            modelBuilder.Entity<FlashCardSet>()
                .Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<FlashCard>(eb =>
            {
                eb.Property(s => s.Word)
                .IsRequired()
                .HasMaxLength(30);
                eb.Property(s => s.Def)
                .IsRequired()
                .HasMaxLength(60);


            });



        }


        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


                
        

    }
}
