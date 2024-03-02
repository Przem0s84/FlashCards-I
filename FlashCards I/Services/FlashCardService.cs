using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCards_I.Services
{
    public class FlashCardService:IFlashcardService
    {
        private readonly FlashCardsDbContext _context;
        private readonly IMapper _mapper;
        public FlashCardService(FlashCardsDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }

        public int Create(int flashcardsetId, CreateFlashCardDto dto)
        {
            var flashcardset = _context.FlashCardsSets.FirstOrDefault(f=>f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }

            var flashcardEnt = _mapper.Map<FlashCard>(dto);

            flashcardEnt.FlashCardsSetId = flashcardsetId;
            _context.FlashCards.Add(flashcardEnt);
            _context.SaveChanges();

            return flashcardEnt.Id;



        }

        public FlashCardDto GetById(int flashcardsetId, int flashcardId)
        {
            var flashcardset = _context.FlashCardsSets.FirstOrDefault(f => f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }

            FlashCard flashcard = _context.FlashCards.FirstOrDefault(f => f.Id == flashcardId);
            if(flashcard is null || flashcard.FlashCardsSetId != flashcardset.Id)
            {
                throw new NotFoundException("Flashcard nto found");
            }
            var flashcardDto = _mapper.Map<FlashCardDto>(flashcard);

            return flashcardDto;
     
        }

        public List<FlashCardDto> GetAll(int flashcardsetId)
        {
            var flashcardset = _context.FlashCardsSets.Include(s=>s.flashCards).FirstOrDefault(f => f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }

            var flashcardDtos = _mapper.Map<List<FlashCardDto>>(flashcardset.flashCards);
            return flashcardDtos;

            
        }

        public void RemoveAll(int flashcardsetId)
        {
            var flashcardset = _context.FlashCardsSets.Include(s => s.flashCards).FirstOrDefault(f => f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }

            _context.RemoveRange(flashcardset.flashCards);
            _context.SaveChanges();

        }
    }
}
