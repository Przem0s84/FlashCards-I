using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Services
{
    public interface IFlashCardsService
    {
        int Create(CreateFlashCardsSetDto dto);
        IEnumerable<FlashCardsSetDto> GetAll();
        FlashCardsSetDto GetById(int id);
        void Update(UpdateFlashCardsSetDto dto, int id);
        void Delete(int id);
    }

    public class FlashCardsSetService : IFlashCardsService
    {
        private readonly FlashCardsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashCardsSetService> _logger;
        public FlashCardsSetService(FlashCardsDbContext dbContext, IMapper mapper,ILogger<FlashCardsSetService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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

        public int Create(CreateFlashCardsSetDto dto)
        {
            var stack = _mapper.Map<FlashCardSet>(dto);
            _dbContext.FlashCardsSets.Add(stack);
            _dbContext.SaveChanges();

            return stack.Id;
        }
        public void Update(UpdateFlashCardsSetDto dto, int id)
        {
            var Stack = _dbContext.FlashCardsSets.FirstOrDefault(x => x.Id == id);
                
            
            if(Stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            //Automapper option
            //var stack = _mapper.Map<Stack>(dto);
            //Stack = stack;

            Stack.Title= dto.Title;
            _dbContext.SaveChanges();
            
        }
        public void Delete(int id)
        {
            _logger.LogError($"FlashCardsSet with id: {id} DELETE action invoked");
            var stack = _dbContext.FlashCardsSets.FirstOrDefault(x=>x.Id == id);
            if (stack is null) { throw new NotFoundException("FlashCard Set not found"); }

            _dbContext.Remove(stack);
            _dbContext.SaveChanges();

            
        }

    }
}
