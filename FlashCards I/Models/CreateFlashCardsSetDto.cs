using FlashCards.Entities;

namespace FlashCards_I.Models
{
    public class CreateFlashCardsSetDto
    {
        public  string Title { get; set; }
        public string Type { get; set; }
        public virtual List<FlashCard> flashCards { get; set; }


    }
}
