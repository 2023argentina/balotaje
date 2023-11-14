using System.Threading.Tasks;
using Telemetry;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Utilities;

namespace Login
{
    public class LogInAnonymousController: ILoginService
    {
    private ITelemetrySender _telemetrySender;

    public LogInAnonymousController()
    {
        _telemetrySender = ServiceLocator.Instance.GetService<ITelemetrySender>();
    }
   
    public void LogIn()
    {
        SignInAnonymouslyAsync();
    }
   
    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
        
            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 
         
            UnityServices.ExternalUserId = AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}

}

