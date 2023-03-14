using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.Models;

namespace FlashCards_I.Services
{
    public interface IFlashcardService
    {
        int Create(int flashcardId, CreateFlashCardDto dto);
    }
    public class FlashCardService:IFlashcardService
    {
        private readonly FlashCardsDbContext _context;
        private readonly IMapper _mapper;
        public FlashCardService(FlashCardsDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }

        public int Create(int flashcardId, CreateFlashCardDto dto)
        {
            var flashcard = _context.FlashCardsSets.FirstOrDefault(f=>f.Id == flashcardId);
            if (flashcard is null) { throw new NotFoundException("FlashCardSet not found"); }

            var flashcardEnt = _mapper.Map<FlashCard>(dto);

            flashcardEnt.FlashCardsSetId = flashcardId;
            _context.FlashCards.Add(flashcardEnt);
            _context.SaveChanges();

            return flashcardEnt.Id;



        }
    }
}
