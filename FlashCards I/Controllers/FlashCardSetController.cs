using FlashCards.Entities;

using FlashCards.Services;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
    public class FlashCardSetController : ControllerBase
    {
        private readonly IFlashCardsService _flashcardService;
        private readonly FlashCardsDbContext _dbContext;
        public FlashCardSetController(IFlashCardsService flashcardService)
        {
            
            _flashcardService = flashcardService;
        }


        [HttpPost]
        public ActionResult AddNewStack([FromBody] CreateFlashCardsSetDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _flashcardService.Create(dto);

            return Created($"Set with id: {id} is created",null);

        }

        [HttpGet]
        public ActionResult<IEnumerable<FlashCardSet>> GetAll()
        {

            var flashcards = _flashcardService.GetAll();
            if(flashcards is null) { return NotFound(); }

            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<FlashCardsSetDto> Get([FromRoute] int id)
        {

            var flashcards = _flashcardService.GetById(id);
            
            

            return Ok(flashcards);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update([FromBody] UpdateFlashCardsSetDto dto,[FromRoute]int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            _flashcardService.Update(dto, id);

            

            return Ok();

        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _flashcardService.Delete(id);
            

            return NoContent();
        }





    }
}
