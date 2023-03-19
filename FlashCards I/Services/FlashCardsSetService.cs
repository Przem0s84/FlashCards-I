using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Authorization;
using FlashCards_I.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlashCards.Services
{
    public interface IFlashCardsService
    {
        int Create(CreateFlashCardsSetDto dto ,int userId);
        IEnumerable<FlashCardsSetDto> GetAll();
        FlashCardsSetDto GetById(int id);
        void Update(UpdateFlashCardsSetDto dto, int id, ClaimsPrincipal user);
        void Delete(int id,ClaimsPrincipal user);

    }

    public class FlashCardsSetService : IFlashCardsService
    {
        private readonly FlashCardsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashCardsSetService> _logger;
        private readonly IAuthorizationService _authorizationService;
        public FlashCardsSetService(FlashCardsDbContext dbContext, IMapper mapper,ILogger<FlashCardsSetService> logger,IAuthorizationService authorizationSevice)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationSevice;
        }

        public FlashCardsSetDto GetById(int id)
        {
            var stack = _dbContext.FlashCardsSets
                        .Include(x => x.flashCards)
                        .FirstOrDefault(x => x.Id == id);

            if (stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var result = _mapper.Map<FlashCardsSetDto>(stack);

            return result;

        }
        public IEnumerable<FlashCardsSetDto> GetAll()
        {
            var stacks = _dbContext.FlashCardsSets
                         .Include(x => x.flashCards)
                         .ToList();

            if (stacks is null) { return null; }
            var stacksDto = _mapper.Map<List<FlashCardsSetDto>>(stacks);

            return stacksDto;
        }

        public int Create(CreateFlashCardsSetDto dto, int userId)
        {
            var stack = _mapper.Map<FlashCardSet>(dto);
            stack.CreatedById = userId;
            _dbContext.FlashCardsSets.Add(stack);
            _dbContext.SaveChanges();

            return stack.Id;
        }
        public void Update(UpdateFlashCardsSetDto dto, int id, ClaimsPrincipal user)
        {

            var Stack = _dbContext.FlashCardsSets.FirstOrDefault(x => x.Id == id);
                
            
            if(Stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(user, Stack, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }

            //Automapper option
            //var stack = _mapper.Map<Stack>(dto);
            //Stack = stack;

            Stack.Title= dto.Title;
            _dbContext.SaveChanges();
            
        }
        public void Delete(int id ,ClaimsPrincipal user)
        {
            _logger.LogError($"FlashCardsSet with id: {id} DELETE action invoked");
            var stack = _dbContext.FlashCardsSets.FirstOrDefault(x=>x.Id == id);
            if (stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            var authorizationresult = _authorizationService.AuthorizeAsync(user, stack, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbidException();
            }
            
            _dbContext.FlashCardsSets.Remove(stack);
            _dbContext.SaveChanges();

            
        }

    }
}
