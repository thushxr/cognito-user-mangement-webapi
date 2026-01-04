using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using WebApi.Cognito.UserMng.Models;

namespace WebApi.Cognito.UserMng.ExternalServices;

public class CognitoAppService : ICognitoAppService
{
    public static readonly string CLIENT_ID = Environment.GetEnvironmentVariable("CLIENT_ID");
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient = new (RegionEndpoint.USEast1);
    
    public async Task<bool> SaveUser(User user)
    {
        try
        {
            var request = new SignUpRequest()
            {
                ClientId = CLIENT_ID,
                Username = user.Email,
                Password = user.Password,
            };

            var signUpResponse = await _cognitoClient.SignUpAsync(request);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> ConfirmUser(UserOtp user)
    {
        try
        {
            var request = new ConfirmSignUpRequest()
            {
                ClientId = CLIENT_ID,
                Username = user.Email,
                ConfirmationCode = user.OtpCode
            };
            
            var response = await _cognitoClient.ConfirmSignUpAsync(request);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> ForgotPassword(ForgotPassword userName)
    {
        try
        {
            var request = new ForgotPasswordRequest()
            {
                ClientId = CLIENT_ID,
                Username = userName.UserName,
            };
            
            var response = await _cognitoClient.ForgotPasswordAsync(request);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> ConfirmForgotPassword(ConfirmForgotPassword request)
    {
        try
        {
            var req = new ConfirmForgotPasswordRequest()
            {
                ClientId = CLIENT_ID,
                Username = request.UserName,
                Password = request.Password,
                ConfirmationCode = request.OtpCode
            };
            
            var response = await _cognitoClient.ConfirmForgotPasswordAsync(req);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> Login(Login request)
    {
        try
        {
            var req = new InitiateAuthRequest()
            {
                ClientId = CLIENT_ID,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>()
                {
                    {"USERNAME", request.UserName},
                    {"PASSWORD", request.Password}
                }
            };
            
            var response = await _cognitoClient.InitiateAuthAsync(req);
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> LoginWithOtp(Login request)
    {
        try
        {
            var iden = request.UserName.Contains('@') ? "EMAIL" : "PHONE_NUMBER";
            
            var req = new InitiateAuthRequest()
            {
                ClientId = CLIENT_ID,
                AuthFlow = AuthFlowType.CUSTOM_AUTH,
                AuthParameters = new Dictionary<string, string>()
                {
                    {iden, request.UserName},
                }
            };
            
            var response = await _cognitoClient.InitiateAuthAsync(req);
            
            if (response.ChallengeName == ChallengeNameType.CUSTOM_CHALLENGE)
            {
                return await HandleCustomChallenge(response, request.UserName);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    private async Task<bool> HandleCustomChallenge(InitiateAuthResponse authResponse, string userName)
    {
        Console.WriteLine("Enter the OTP sent to your provided contact method:");
        var otpCode = Console.ReadLine();

        var respondRequest = new RespondToAuthChallengeRequest()
        {
            ClientId = CLIENT_ID,
            Session = authResponse.Session,
            ChallengeName = ChallengeNameType.CUSTOM_CHALLENGE,
            ChallengeResponses = new Dictionary<string, string>
            {
                {"USERNAME", userName},
                {"CUSTOM_CHALLENGE_RESPONSE", otpCode}
            }
        };

        var respondResponse = await _cognitoClient.RespondToAuthChallengeAsync(respondRequest);

        return respondResponse.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }
}