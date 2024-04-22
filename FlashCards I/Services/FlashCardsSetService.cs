using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Authorization;
using FlashCards_I.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlashCards.Services
{

    public class FlashCardsSetService : IFlashCardsService
    {
        private readonly FlashCardsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashCardsSetService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public FlashCardsSetService(FlashCardsDbContext dbContext, IMapper mapper,ILogger<FlashCardsSetService> logger,IAuthorizationService authorizationSevice,IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationSevice;
            _userContextService = userContextService;
        }

        public FlashCardsSetDto GetById(int id)
        {
            var stack = _dbContext.FlashCardsSets
                        .Include(x => x.flashCards)
                        .FirstOrDefault(x => x.Id == id);

            if (stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, stack, new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }
            var result = _mapper.Map<FlashCardsSetDto>(stack);

            return result;

        }
        public IEnumerable<FlashCardsSetDto> GetAll()
        {
            var stacks = _dbContext.FlashCardsSets
                         .Include(x => x.flashCards)
                         .Where(x=>x.CreatedById == _userContextService.GetUserId)
                         .ToList();

            if (stacks is null) { return null; }
            var stacksDto = _mapper.Map<List<FlashCardsSetDto>>(stacks);

            return stacksDto;
        }

        public int Create(CreateFlashCardsSetDto dto)
        {
            var set = _mapper.Map<FlashCardSet>(dto);
            set.CreatedById = _userContextService.GetUserId;
            _dbContext.FlashCardsSets.Add(set);
            _dbContext.SaveChanges();

            return set.Id;
        }
        public void Update(UpdateFlashCardsSetDto dto, int id)
        {

            var Stack = _dbContext.FlashCardsSets.FirstOrDefault(x => x.Id == id);
                
            
            if(Stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, Stack, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }

            Stack.Title= dto.Title;
            _dbContext.SaveChanges();
            
        }
        public void Delete(int id)
        {
            
            _logger.LogError($"FlashCardsSet with id: {id} DELETE action invoked");
            var stack = _dbContext.FlashCardsSets.FirstOrDefault(x=>x.Id == id);
            if (stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(_userContextService.User, stack, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }
            
            _dbContext.FlashCardsSets.Remove(stack);
            _dbContext.SaveChanges();

            
        }

    }
}
