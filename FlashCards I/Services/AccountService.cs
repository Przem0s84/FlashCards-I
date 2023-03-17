  using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Entities;
using FlashCards_I.Models;

namespace FlashCards_I.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegistrationUDto regdto);
    }

    public class AccountService : IAccountService
    {
        private readonly FlashCardsDbContext _context;


        public AccountService(FlashCardsDbContext context)
        {
            _context = context;

        }
        public void RegisterUser(RegistrationUDto regdto)
        {
            var createdUser = new User()
            {
                Email = regdto.Email,
                Password = regdto.Password,
                NickName = regdto.NickName,
                RoleId = regdto.UserRoleId

            };
            _context.Users.Add(createdUser);
            _context.SaveChanges();
        }
    }
}
