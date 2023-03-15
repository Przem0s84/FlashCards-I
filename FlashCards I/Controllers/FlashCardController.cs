using FlashCards.Entities;
using FlashCards_I.Models;
using FlashCards_I.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards_I.Controllers
{
    [Route("api/flashcards/{flashcardsetId}/flashcard")]
    [ApiController]
    public class FlashCardController: ControllerBase
    {
        private readonly IFlashcardService _flashcardService;
        public FlashCardController(IFlashcardService flashcardService)
        {
            _flashcardService= flashcardService;
        }

    [HttpPost]
    public ActionResult Post([FromRoute]int flashcardsetId, [FromBody] CreateFlashCardDto dto)
        {
            var newFlashCardId = _flashcardService.Create(flashcardsetId, dto);

            return Created($"api/restaurant/{flashcardsetId}/flashcard/{newFlashCardId}",null);
        }

        [HttpGet("{flashcardId}")]
        public ActionResult<FlashCardDto> Get([FromRoute]int flashcardsetId, [FromRoute]int flashcardId)
        {
            FlashCardDto flashcard = _flashcardService.GetById(flashcardsetId, flashcardId);
            
            return Ok(flashcard);

        }

        [HttpGet]
        public ActionResult <List<FlashCardDto>> Get([FromRoute] int flashcardsetId)
        {
            var flashcards = _flashcardService.GetAll(flashcardsetId);

            return Ok(flashcards);

        }
        [HttpDelete]
        public ActionResult Delete([FromRoute]int flashcardsetId)
        {
            _flashcardService.RemoveAll(flashcardsetId);
            return NoContent();
        }

    }


}
