namespace AuthApi.Models
{
    public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponseModel
    {
        public UserModel? User { get; set; }
        public string? Token { get; set; }
    }
}
