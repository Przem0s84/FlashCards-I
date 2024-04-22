using FlashCards.Entities;
using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlashCards.Controllers
{
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = "User")]
        public ActionResult AddNewStack([FromBody] CreateFlashCardsSetDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _flashcardService.Create(dto);

            return Created($"Set with id: {id} is created",null);

        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult<IEnumerable<FlashCardSet>> GetAll()
        {

            var flashcards = _flashcardService.GetAll();
            if(flashcards is null) { return NotFound(); }

            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public ActionResult<FlashCardsSetDto> Get([FromRoute] int id)
        {

            var flashcards = _flashcardService.GetById(id);
            
            

            return Ok(flashcards);
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult Update([FromBody] UpdateFlashCardsSetDto dto,[FromRoute]int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            _flashcardService.Update(dto,id);

            return Ok();

        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            _flashcardService.Delete(id);
            

            return NoContent();
        }





    }
}
