using FlashCards.Entities;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace FlashCards
{
    public class FlashCardSeeder
    {
        private readonly FlashCardsDbContext _dbContext;
        public FlashCardSeeder(FlashCardsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Stacks.Any())
                {
                    var stacks = GetStacks();
                    _dbContext.Stacks.AddRange(stacks);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Stack> GetStacks()
        {
            var stacks = new List<Stack>()
            {
                new Stack()
                {
                    Title = "Animals",

                    wordAndDef = new List<WordAndDef>()
                    {
                        new WordAndDef()
                        {
                            Word = "Kot",
                            Def = "Cat"
                        },
                        new WordAndDef()
                        {
                            Word = "Pies",
                            Def = "Dog"
                        },
                        new WordAndDef()
                        {
                            Word = "Krowa",
                            Def = "Cow"
                        },
                        new WordAndDef()
                        {
                            Word = "Pingwin",
                            Def = "Pinguin"
                        },


                    }

                },
                new Stack()
                {
                    Title = "House",

                    wordAndDef = new List<WordAndDef>()
                    {
                        new WordAndDef()
                        {
                            Word = "Salon",
                            Def = "Livingroom"
                        },
                        new WordAndDef()
                        {
                            Word = "Łazienka",
                            Def = "Bathroom"
                        },
                        new WordAndDef()
                        {
                            Word = "Sypialnia",
                            Def = "Bedroom"
                        },
                        new WordAndDef()
                        {
                            Word = "Strych",
                            Def = "Attic"
                        },


                    }

                },
                new Stack()
                {
                    Title = "School",

                    wordAndDef = new List<WordAndDef>()
                    {
                        new WordAndDef()
                        {
                            Word = "Linijka",
                            Def = "Ruller"
                        },
                        new WordAndDef()
                        {
                            Word = "Długopic",
                            Def = "Pen"
                        },
                        new WordAndDef()
                        {
                            Word = "Ołówek",
                            Def = "Pencil"
                        },
                        new WordAndDef()
                        {
                            Word = "Słownik",
                            Def = "Dictionary"
                        },


                    }

                },

              
            };
            return stacks;
        }
    }
}
