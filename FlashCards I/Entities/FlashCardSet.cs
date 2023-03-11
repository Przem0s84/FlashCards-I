namespace FlashCards.Entities
{
    public class FlashCardSet
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        
        public virtual List<FlashCard> flashCards { get; set; }
        

    }
}
