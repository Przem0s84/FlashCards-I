namespace FlashCards.Entities
{
    public class FlashCard
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Def { get; set; }

        
        public virtual FlashCardSet FlashCardsSet { get; set; }
        public int FlashCardsSetId { get; set; }
    }
}
