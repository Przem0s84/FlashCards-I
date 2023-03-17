  using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Entities;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Identity;

namespace FlashCards_I.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegistrationUDto regdto);
    }

    public class AccountService : IAccountService
    {
        private readonly FlashCardsDbContext _context;

        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(FlashCardsDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }
        public void RegisterUser(RegistrationUDto regdto)
        {
            
            var createdUser = new User()
            {
                Email = regdto.Email,
                
                NickName = regdto.NickName,
                RoleId = regdto.UserRoleId

            };
            createdUser.Password= _passwordHasher.HashPassword(createdUser, regdto.Password);
            _context.Users.Add(createdUser);
            _context.SaveChanges();
        }
    }
}
