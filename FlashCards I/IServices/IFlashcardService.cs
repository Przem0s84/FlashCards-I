using FlashCards_I.Models;

namespace FlashCards_I.IServices
{
    public interface IFlashcardService
    {
        int Create(int flashcardId, CreateFlashCardDto dto);
        FlashCardDto GetById(int flashcardsetId, int flashcardId);
        List<FlashCardDto> GetAll(int flashcardsetId);
        void RemoveAll(int flashcardsetId);
    }
}
