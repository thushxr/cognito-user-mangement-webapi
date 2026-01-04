using AutoMapper;
using WebApi.Cognito.UserMng.Dtos;
using WebApi.Cognito.UserMng.ExternalServices;
using WebApi.Cognito.UserMng.Models;

namespace WebApi.Cognito.UserMng.Services;

public class UserAppService : IUserAppService
{
    
    public readonly ICognitoAppService _cognitoAppService;

    public UserAppService(ICognitoAppService cognitoAppService)
    {
        _cognitoAppService = cognitoAppService ?? throw new ArgumentNullException(nameof(cognitoAppService));
    }

    public void SaveUser(UserRequestDto request)
    {
        try
        {
            var user = new User()
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
            };
            
            _cognitoAppService.SaveUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ConfirmUser(UserOtp request)
    {
        try
        {
            _cognitoAppService.ConfirmUser(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public void ForgotPassword(ForgotPassword userName)
    {
        try
        {
            _cognitoAppService.ForgotPassword(userName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public void ConfirmForgotPassword(ConfirmForgotPassword request)
    {
        try
        {
            _cognitoAppService.ConfirmForgotPassword(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public void Login(Login request)
    {
        try
        {
            _cognitoAppService.Login(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public void LoginWithOtp(Login request)
    {
        try
        {
            _cognitoAppService.LoginWithOtp(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}