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
                if(!_dbContext.FlashCardsSets.Any())
                {
                    var stacks = GetStacks();
                    _dbContext.FlashCardsSets.AddRange(stacks);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<FlashCardSet> GetStacks()
        {
            var stacks = new List<FlashCardSet>()
            {
                new FlashCardSet()
                {
                    Title = "Animals",

                    flashCards = new List<FlashCard>()
                    {
                        new FlashCard()
                        {
                            Word = "Kot",
                            Def = "Cat"
                        },
                        new FlashCard()
                        {
                            Word = "Pies",
                            Def = "Dog"
                        },
                        new FlashCard()
                        {
                            Word = "Krowa",
                            Def = "Cow"
                        },
                        new FlashCard()
                        {
                            Word = "Pingwin",
                            Def = "Pinguin"
                        },


                    },
                    Type = "PL-ANG"

                },
                new FlashCardSet()
                {
                    Title = "House",

                    flashCards = new List<FlashCard>()
                    {
                        new FlashCard()
                        {
                            Word = "Salon",
                            Def = "Livingroom"
                        },
                        new FlashCard()
                        {
                            Word = "Łazienka",
                            Def = "Bathroom"
                        },
                        new FlashCard()
                        {
                            Word = "Sypialnia",
                            Def = "Bedroom"
                        },
                        new FlashCard()
                        {
                            Word = "Strych",
                            Def = "Attic"
                        },



                    },
                    Type = "PL-ANG"

                },
                new FlashCardSet()
                {
                    Title = "School",

                    flashCards = new List<FlashCard>()
                    {
                        new FlashCard()
                        {
                            Word = "Linijka",
                            Def = "Ruller"
                        },
                        new FlashCard()
                        {
                            Word = "Długopic",
                            Def = "Pen"
                        },
                        new FlashCard()
                        {
                            Word = "Ołówek",
                            Def = "Pencil"
                        },
                        new FlashCard()
                        {
                            Word = "Słownik",
                            Def = "Dictionary"
                        },


                    },
                    Type = "PL-ANG"

                },

              
            };
            return stacks;
        }
    }
}
