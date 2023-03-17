using FlashCards_I.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlashCards_I.Models
{
    public class RegistrationUDto
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string NickName { get; set; }

        public int UserRoleId { get; set; } = 1;





    }
}