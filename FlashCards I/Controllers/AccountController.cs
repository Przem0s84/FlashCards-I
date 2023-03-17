using FlashCards_I.Models;
using FlashCards_I.Services;
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

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegistrationUDto registerdto)
        {
            _accountService.RegisterUser(registerdto);
            return Ok();

        }
    }
}
