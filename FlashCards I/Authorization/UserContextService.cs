using System.Security.Claims;
using FlashCards_I.IServices;

namespace FlashCards_I.Authorization
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int? GetUserId => (User is null)?null:int.Parse(User.FindFirst(c=>c.Type==ClaimTypes.NameIdentifier).Value);





    }
}
