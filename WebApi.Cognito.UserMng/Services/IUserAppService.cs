using WebApi.Cognito.UserMng.Dtos;
using WebApi.Cognito.UserMng.Models;

namespace WebApi.Cognito.UserMng.Services;

public interface IUserAppService
{
    void SaveUser(UserRequestDto request);
    void ConfirmUser(UserOtp request);
    void ForgotPassword(ForgotPassword request);
    void ConfirmForgotPassword(ConfirmForgotPassword request);
    void Login(Login request);
    void LoginWithOtp(Login request);
}