using FlashCards_I.Entities;

namespace FlashCards.Entities
{
    public class FlashCardSet
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual List<FlashCard> flashCards { get; set; }
        

    }
}
