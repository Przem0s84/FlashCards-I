﻿using AutoMapper;
using FlashCards.Entities;
using FlashCards_I.Entities;
using FlashCards_I.Exceptions;
using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlashCards_I.Services
{

    public class AccountService : IAccountService
    {
        private readonly FlashCardsDbContext _context;

        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(FlashCardsDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;

        }

        public string GenerateToken(LoginUserDto loginUserDto)
        {
            var user = _context.Users.Include(u=>u.Role).FirstOrDefault(p=>p.Email == loginUserDto.Email);
            if(user is null) { throw new BadRequestException("Email or Password is wrong"); }

            var iscorrect = _passwordHasher.VerifyHashedPassword(user,user.Password,loginUserDto.Password);
           if(iscorrect == PasswordVerificationResult.Failed) {throw new BadRequestException("Email or Password is wrong");}

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim(ClaimTypes.Name, user.NickName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,_authenticationSettings.JwtIssuer,claims,expires:expires,signingCredentials:credentails);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegistrationUDto regdto)
        {

            var createdUser = new User()
            {
                Email = regdto.Email,
                NickName = regdto.NickName,
                SecurityQuestion = regdto.SecQuestion,
                RoleId = 1,
            };
            createdUser.Password= _passwordHasher.HashPassword(createdUser, regdto.Password);
            createdUser.SecurityAnswer = _passwordHasher.HashPassword(createdUser,regdto.SecAnswer);
            _context.Users.Add(createdUser);
            _context.SaveChanges();
        }

        public void ResetPassword(ResetPasswordDto resetdto)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(p => p.Email == resetdto.Email);
            if (user is null) { throw new BadRequestException("Email or SecureAnswer is wrong"); }

            var iscorrect = _passwordHasher.VerifyHashedPassword(user, user.SecurityAnswer, resetdto.SecAnswer);
            if(iscorrect==PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Email or SecurityAnswer is wrong");
            }

            user.Password = _passwordHasher.HashPassword(user,resetdto.NewPassword);
            _context.SaveChanges();

        }
    }
}
