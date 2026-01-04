namespace WebApi.Cognito.UserMng.Models;

public class ConfirmForgotPassword
{
    public string UserName { get; set; }
    public string OtpCode { get; set; }
    public string Password { get; set; }
}