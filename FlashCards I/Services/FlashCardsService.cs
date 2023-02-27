using FlashCards.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Services
{
    public class FlashCardsService
    {
        private readonly FlashCardsDbContext  _dbContext;
        public FlashCardsService(FlashCardsDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public IEnumerable<Stack> GetAll()
        {
            var flashcards = _dbContext
                .Stacks
                .Include(r => r.wordAndDef)//dodanie tablic powiązanych
                .ToList();

            
            return flashcards;
        }
    }
}
