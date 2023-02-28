namespace FlashCards.Entities
{
    public class WordAndDef
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Def { get; set; }

        public int StackId { get; set; } 
        public virtual Stack Stack { get; set; }
    }
}
