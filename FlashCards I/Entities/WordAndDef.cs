namespace FlashCards.Entities
{
    public class WordAndDef
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Def { get; set; }

        public int StactId { get; set; } //odniesienie do klasy Stack , klucz obcy
        public virtual Stack Stack { get; set; }//referencja do klasy Stack
    }
}
