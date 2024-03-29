﻿using FlashCards_I.IServices;
using FlashCards_I.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards_I.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService= accountService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = _accountService.GenerateToken(loginUserDto);
            return Ok(token);

        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegistrationUDto registerdto)
        {
            _accountService.RegisterUser(registerdto);
            return Ok();

        }

        [HttpPost("reset")]
        public ActionResult ResetPassword([FromBody] ResetPasswordDto resetdto)
        {
            _accountService.ResetPassword(resetdto);
            return Ok();

        }
    }
}
