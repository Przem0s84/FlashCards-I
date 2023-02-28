using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Services
{
    public interface IFlashCardsService
    {
        int Create(CreateStackDto dto);
        IEnumerable<StackDto> GetAll();
        StackDto GetById(int id);
        bool Update(UpdateStackDto dto, int id);
        bool Delete(int id);
    }

    public class FlashCardsService : IFlashCardsService
    {
        private readonly FlashCardsDbContext _dbContext;
        private readonly IMapper _mapper;
        public FlashCardsService(FlashCardsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public StackDto GetById(int id)
        {
            var stack = _dbContext.Stacks
                        .Include(x => x.wordAndDefs)
                        .FirstOrDefault(x => x.Id == id);

            if (stack is null) { return null; }

            var result = _mapper.Map<StackDto>(stack);

            return result;

        }
        public IEnumerable<StackDto> GetAll()
        {
            var stacks = _dbContext.Stacks
                         .Include(x => x.wordAndDefs)
                         .ToList();

            if (stacks is null) { return null; }
            var stacksDto = _mapper.Map<List<StackDto>>(stacks);

            return stacksDto;
        }

        public int Create(CreateStackDto dto)
        {
            var stack = _mapper.Map<Stack>(dto);
            _dbContext.Stacks.Add(stack);
            _dbContext.SaveChanges();

            return stack.Id;
        }
        public bool Update(UpdateStackDto dto, int id)
        {
            var Stack = _dbContext.Stacks.FirstOrDefault(x => x.Id == id);
                
            
            if(Stack is null) { return false; }

            //Automapper option
            //var stack = _mapper.Map<Stack>(dto);
            //Stack = stack;

            Stack.Title= dto.Title;
            _dbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var stack = _dbContext.Stacks.FirstOrDefault(x=>x.Id == id);
            if (stack is null) { return false; }

            _dbContext.Remove(stack);
            _dbContext.SaveChanges();

            return true;
        }

    }
}
