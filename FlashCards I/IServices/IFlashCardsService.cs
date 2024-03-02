using FlashCards_I.Models;
using System.Security.Claims;

namespace FlashCards_I.IServices
{
    public interface IFlashCardsService
    {
        int Create(CreateFlashCardsSetDto dto, int userId);
        IEnumerable<FlashCardsSetDto> GetAll();
        FlashCardsSetDto GetById(int id);
        void Update(UpdateFlashCardsSetDto dto, int id, ClaimsPrincipal user);
        void Delete(int id, ClaimsPrincipal user);

    }
}
