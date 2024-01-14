namespace zEzzyShoppingFrontend.Models.Auth
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginResult
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public LoginResponseDto Result { get; set; }
    }
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public UserLogin User { get; set; } = default!;
    }
}
