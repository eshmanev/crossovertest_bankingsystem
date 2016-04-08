using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BankingSystem.WebPortal.Hubs
{
    /// <summary>
    ///     Token authorization.
    /// </summary>
    /// <remarks>
    ///     This class contains hard-coded values to simplify the implementation. 
    ///     In real world we should generate tokens for each client and require handshake.
    /// </remarks>
    /// <seealso cref="Microsoft.AspNet.SignalR.AuthorizeAttribute" />
    internal class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
        {
            if (request.Headers["SuperDuperSecretToken"] == "{3E663BA0-66A3-4E76-A1AE-3FB754042EDE}")
                return true;

            return base.AuthorizeHubConnection(hubDescriptor, request);
        }
    }
}