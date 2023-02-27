using FlashCards.Entities;

namespace FlashCards_I.Models
{
    public class StackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<WordAndDefDto> wordAndDefs { get; set; }
    }
}
