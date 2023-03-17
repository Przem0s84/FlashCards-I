using FlashCards_I.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlashCards_I.Models
{
    public class RegistrationUDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string NickName { get; set; }

        public int UserRoleId { get; set; } = 1;





    }
}