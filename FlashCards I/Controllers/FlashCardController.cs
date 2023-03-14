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

            return Created($"api/{flashcardsetId}/flashcard/{newFlashCardId}",null);
        }
    }
}
