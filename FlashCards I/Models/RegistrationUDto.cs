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

        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }





    }
}