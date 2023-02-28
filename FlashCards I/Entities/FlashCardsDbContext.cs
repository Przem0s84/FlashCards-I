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
            modelBuilder.Entity<Stack>()
                .HasMany(u => u.wordAndDefs)
                .WithOne(u => u.Stack)
                .HasForeignKey(u => u.StackId);

            modelBuilder.Entity<WordAndDef>(eb =>
            {
                eb.Property(s=>s.Word)
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
