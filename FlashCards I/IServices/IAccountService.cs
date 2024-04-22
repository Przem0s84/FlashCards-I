using FlashCards_I.Models;

namespace FlashCards_I.IServices
{
    public interface IAccountService
    {
        void DeleteAccount(string email);
        string GenerateToken(LoginUserDto loginUserDto);
        void RegisterUser(RegistrationUDto regdto);
        void ResetPassword(ResetPasswordDto resetdto);
        List<ReturnUsersDto> GetUsers();
    }
}
