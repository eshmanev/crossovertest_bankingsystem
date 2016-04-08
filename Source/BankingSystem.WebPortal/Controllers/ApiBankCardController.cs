using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;
using BankingSystem.LogicTier.Impl;
using log4net;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Provides bank cards API.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [System.Web.Http.RoutePrefix("api/bankcard/{cardNumber}/pin/{pin}")]
    [RequireHttps]
    public class ApiBankCardController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ApiBankCardController));
        private readonly IBankCardService _bankCardService;
        private readonly IAccountService _accountService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiBankCardController" /> class.
        /// </summary>
        /// <param name="bankCardService">The bank card service.</param>
        /// <param name="accountService">The account service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ApiBankCardController(IBankCardService bankCardService, IAccountService accountService)
        {
            if (bankCardService == null)
                throw new ArgumentNullException(nameof(bankCardService));

            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            _bankCardService = bankCardService;
            _accountService = accountService;
        }

        /// <summary>
        ///     Checks the pin.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [System.Web.Http.Route]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CheckPin(string cardNumber, string pin)
        {
            Validate(cardNumber, pin);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Updates the pin.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        [System.Web.Http.Route]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdatePin(string cardNumber, string pin, NewPinMessage message)
        {
            Validate(cardNumber, pin);

            try
            {
                _bankCardService.UpdatePin(cardNumber, message.NewPin);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error has occured while updating pin code. Bank card: " + cardNumber, ex);
                throw;
            }
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [System.Web.Http.Route("balance")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetBalance(string cardNumber, string pin)
        {
            Validate(cardNumber, pin);

            try
            {
                var balance = _bankCardService.GetBalance(cardNumber);
                return Request.CreateResponse(HttpStatusCode.OK, balance);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error has occured while updating pin code. Bank card: " + cardNumber, ex);
                throw;
            }
        }

        /// <summary>
        ///     Updates the balance.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        [System.Web.Http.Route("balance")]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateBalance(string cardNumber, string pin, ChangeAmountMessage message)
        {
            Validate(cardNumber, pin);

            try
            {
                var bankCard = _bankCardService.FindBankCard(cardNumber);
                if (bankCard == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bank card cannot be found");

                _accountService.UpdateBalance(bankCard.Account, message.Amount);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (BankingServiceException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error has occured while updating bank card balance. Bank card: " + cardNumber, ex);
                throw;
            }
        }

        private void Validate(string cardNumber, string pin)
        {
            if (!_bankCardService.CheckPin(cardNumber, pin))
                throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}