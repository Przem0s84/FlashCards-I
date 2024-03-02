namespace FlashCards_I.Models
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string SecAnswer { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassword { get; set; }
    }
}
