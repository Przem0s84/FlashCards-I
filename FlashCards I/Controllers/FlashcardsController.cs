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
        private readonly IFlashCardsService _flashcardService;
        private readonly FlashCardsDbContext _dbContext;
        public FlashcardsController(IFlashCardsService flashcardService)
        {
            
            _flashcardService = flashcardService;
        }


        [HttpPost]
        public ActionResult AddNewStack([FromBody] CreateStackDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _flashcardService.Create(dto);

            return Created($"Stack with id: {id} is created",null);

        }

        [HttpGet]
        public ActionResult<IEnumerable<Stack>> GetAll()
        {

            var flashcards = _flashcardService.GetAll();
            if(flashcards is null) { return NotFound(); }

            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<StackDto> Get([FromRoute] int id)
        {

            var flashcards = _flashcardService.GetById(id);
            
            if(flashcards is null)
            {
                return NotFound();
            }

            return Ok(flashcards);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update([FromBody] UpdateStackDto dto,[FromRoute]int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var isUpdated = _flashcardService.Update(dto, id);

            if(!isUpdated) { return NotFound(); }

            return Ok();

        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var Deleted = _flashcardService.Delete(id);
            if(!Deleted) { return NotFound(); }

            return NoContent();
        }





    }
}
