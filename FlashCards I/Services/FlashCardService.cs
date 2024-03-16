using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Authorization;
using FlashCards_I.Exceptions;
using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlashCards_I.Services
{
    public class FlashCardService:IFlashcardService
    {
        private readonly FlashCardsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public FlashCardService(FlashCardsDbContext context, IMapper mapper, IAuthorizationService authorizationSevice, IUserContextService userContextService)
        {
            _context= context;
            _mapper = mapper;
            _authorizationService = authorizationSevice;
            _userContextService = userContextService;
        }

        public int Create(int flashcardsetId, CreateFlashCardDto dto)
        {
            var flashcardset = _context.FlashCardsSets.FirstOrDefault(f=>f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }
            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, flashcardset, new ResourceOperationRequirement(ResourceOperation.Read)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }

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
            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, flashcardset, new ResourceOperationRequirement(ResourceOperation.Read)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }

            FlashCard flashcard = _context.FlashCards.FirstOrDefault(f => f.Id == flashcardId);
            if(flashcard is null || flashcard.FlashCardsSetId != flashcardset.Id)
            {
                throw new NotFoundException("Flashcard not found");
            }
            var flashcardDto = _mapper.Map<FlashCardDto>(flashcard);

            return flashcardDto;
     
        }

        public List<FlashCardDto> GetAll(int flashcardsetId)
        {
            var flashcardset = _context.FlashCardsSets.Include(s=>s.flashCards).FirstOrDefault(f => f.Id == flashcardsetId);
            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, flashcardset, new ResourceOperationRequirement(ResourceOperation.Read)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }

            var flashcardDtos = _mapper.Map<List<FlashCardDto>>(flashcardset.flashCards);
            return flashcardDtos;

            
        }

        public void RemoveAll(int flashcardsetId)
        {
            var flashcardset = _context.FlashCardsSets.Include(s => s.flashCards).FirstOrDefault(f => f.Id == flashcardsetId);

            if (flashcardset is null) { throw new NotFoundException("FlashCardSet not found"); }
            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, flashcardset, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }
            _context.RemoveRange(flashcardset.flashCards);
            _context.SaveChanges();

        }
    }
}
