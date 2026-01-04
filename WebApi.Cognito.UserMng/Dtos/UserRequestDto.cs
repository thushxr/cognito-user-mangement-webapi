namespace WebApi.Cognito.UserMng.Dtos;

public class UserRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}