using System.Security.Claims;

namespace FlashCards_I.IServices
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}