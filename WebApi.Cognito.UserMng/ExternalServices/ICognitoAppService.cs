using WebApi.Cognito.UserMng.Models;

namespace WebApi.Cognito.UserMng.ExternalServices;

public interface ICognitoAppService
{
    Task<bool> SaveUser(User user);
    Task<bool> ConfirmUser(UserOtp request);
    Task<bool> ForgotPassword(ForgotPassword userName);
    Task<bool> ConfirmForgotPassword(ConfirmForgotPassword request);
    Task<bool> Login(Login request);
    Task<bool> LoginWithOtp(Login request);
}