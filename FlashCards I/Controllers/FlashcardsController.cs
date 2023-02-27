using FlashCards.Entities;

using FlashCards.Services;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
    public class FlashcardsController : ControllerBase
    {
        private readonly FlashCardsService _flash;
        private readonly FlashCardsDbContext _dbContext;
        public FlashcardsController(FlashCardsDbContext dbContext, FlashCardsService flash)
        {
            _dbContext = dbContext;
            _flash = flash;
        }


        [HttpPost]
        public ActionResult AddNewStack([FromBody] CreateStackDto db)
        {
            Stack stacks = new Stack()
            {
                Title = db.Title,

            };
            _dbContext.Stacks.Add(stacks);

            _dbContext.SaveChanges();

            return Ok();

        }

        [HttpGet]
        public ActionResult<IEnumerable<Stack>> GetAll()
        {

            var flashcards = _flash.GetAll();

            return Ok(flashcards);
        }

        
        


    }
}
