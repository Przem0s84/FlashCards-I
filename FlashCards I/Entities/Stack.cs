namespace FlashCards.Entities
{
    public class Stack
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        
        
        public virtual List<WordAndDef> wordAndDefs { get; set; }
        

    }
}
