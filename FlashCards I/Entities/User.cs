using Microsoft.AspNetCore.Identity;

namespace FlashCards_I.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public virtual Role Role { get; set; }

    }
}
