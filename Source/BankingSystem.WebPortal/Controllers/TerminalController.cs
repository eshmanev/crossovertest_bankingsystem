using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankingSystem.Common.Messages;

namespace BankingSystem.WebPortal.Controllers
{
    [RoutePrefix("api/bankcard/{cardNumber}/pin/{pin}")]
    public class TerminalController : ApiController
    {
        public TerminalController()
        {
            
        }
        
        [Route]
        [HttpGet]
        public HttpResponseMessage CheckPin(string cardNumber, string pin)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route]
        [HttpPut]
        public HttpResponseMessage UpdatePin(string cardNumber, string pin, NewPinMessage message)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("balance")]
        [HttpGet]
        public string GetBalance(string cardNumber, string pin)
        {
            return null;
        }

        [Route("balance")]
        [HttpPut]
        public HttpResponseMessage UpdateBalance(string cardNumber, string pin, ChangeAmountMessage message)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}