public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}
