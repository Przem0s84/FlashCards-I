using FlashCards.Entities;
using FlashCards_I.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace FlashCards
{
    public class FlashCardSeeder
    {
        private readonly FlashCardsDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;
        public FlashCardSeeder(FlashCardsDbContext dbContext,IConfiguration config,IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _config = config;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.FlashCardsSets.Any())
                {
                    var stacks = GetStacks();
                    _dbContext.FlashCardsSets.AddRange(stacks);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    _dbContext.Users.AddRange(GetDevelopmentAudminAndUser());
                    _dbContext.SaveChanges();

                }
            }
        }

        private IEnumerable<User> GetDevelopmentAudminAndUser()
        {
            var users = new List<User>() { };

            var adminUser = new User()
            {

                Email = "admin@flashcards.com",
                NickName = "Admin",
                RoleId = 2,
                SecurityQuestion = _config["DevelopmentAuthentication:AdminQuestion"],
                SecurityAnswer = _config["DevelopmentAuthentication:AdminAnswer"],

            };
            adminUser.Password = _passwordHasher.HashPassword(adminUser, _config["DevelopmentAuthentication:AdminPassword"]);

            users.Add(adminUser);

            var standardUser = new User()
            {

                Email = "user@flashcards.com",
                NickName = "User",
                RoleId = 1,
                SecurityQuestion = _config["DevelopmentAuthentication:UserQuestion"],
                SecurityAnswer = _config["DevelopmentAuthentication:UserAnswer"],

            };

            standardUser.Password = _passwordHasher.HashPassword(standardUser, _config["DevelopmentAuthentication:UserPassword"]);

            users.Add(standardUser);
            return users;
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

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = "User"
                    
                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin"
                }

            };
            return roles;
        }


    }
}
