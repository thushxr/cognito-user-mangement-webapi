using Microsoft.AspNetCore.Mvc;
using WebApi.Cognito.UserMng.Dtos;
using WebApi.Cognito.UserMng.Models;
using WebApi.Cognito.UserMng.Services;

namespace WebApi.Cognito.UserMng.Controllers;

[ApiController]
[Route("v1/api/users")]
public class UserController : ControllerBase
{
    public readonly ILogger<UserController> _logger;
    public readonly IUserAppService _userAppService;

    public UserController(ILogger<UserController> logger,
                            IUserAppService userAppService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
    }

    [HttpPost("save")]
    public void SaveUser([FromBody] UserRequestDto request)
    {
        _userAppService.SaveUser(request);
    }
    
    [HttpPost("confirm")]
    public void ConfirmUser([FromBody] UserOtp request)
    {
        _userAppService.ConfirmUser(request);
    }
    
    [HttpPost("forgot-password")]
    public void ForgotPassword([FromBody] ForgotPassword request)
    {
        _userAppService.ForgotPassword(request);
    }
    
    [HttpPost("confirm-forgot-password")]
    public void ConfirmForgotPassword([FromBody] ConfirmForgotPassword request)
    {
        _userAppService.ConfirmForgotPassword(request);
    }
    
    [HttpPost("login")]
    public void Login([FromBody] Login request)
    {
        _userAppService.Login(request);
    }
    
    [HttpPost("login-otp")]
    public void LoginWithOtp([FromBody] Login request)
    {
        _userAppService.LoginWithOtp(request);
    }
    
    //[HttpPost("otp")]
    //public void LoginWithOtp([FromBody] Login request)
    //{
    //    _userAppService.LoginWithOtp(request);
    //}
    
}