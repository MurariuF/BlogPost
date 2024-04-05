namespace CodePulse.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }

        public LoginResponseDto(string email, string token, List<string> roles)
        {
            this.Email = email;
            this.Token = token;
            this.Roles = roles;
        }
    }
}
