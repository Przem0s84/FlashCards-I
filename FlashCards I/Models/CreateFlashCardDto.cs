using System.ComponentModel.DataAnnotations;

namespace FlashCards_I.Models
{
    public class CreateFlashCardDto
    {

        [Required]
        public string Word { get; set; }
        [Required]
        public string Def { get; set; }

        
    }
}
