using FlashCards_I.Models;

namespace FlashCards_I.IServices
{
    public interface IAccountService
    {
        string GenerateToken(LoginUserDto loginUserDto);
        void RegisterUser(RegistrationUDto regdto);
        void ResetPassword(ResetPasswordDto resetdto);
    }
}
