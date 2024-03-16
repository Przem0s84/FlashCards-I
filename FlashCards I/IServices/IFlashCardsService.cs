using FlashCards_I.Models;
using System.Security.Claims;

namespace FlashCards_I.IServices
{
    public interface IFlashCardsService
    {
        int Create(CreateFlashCardsSetDto dto);
        IEnumerable<FlashCardsSetDto> GetAll();
        FlashCardsSetDto GetById(int id);
        void Update(UpdateFlashCardsSetDto dto,int id);
        void Delete(int id);

    }
}
