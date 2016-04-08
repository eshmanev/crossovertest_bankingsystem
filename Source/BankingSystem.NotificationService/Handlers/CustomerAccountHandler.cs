using System;
using System.Diagnostics;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;

namespace BankingSystem.NotificationService.Handlers
{
    /// <summary>
    ///     Represents a handler of customer account-specific messages.
    /// </summary>
    public class CustomerAccountHandler : IHandler<BalanceChangedMessage>
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly ICustomerService _customerService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerAccountHandler" /> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="customerService">The customer service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public CustomerAccountHandler(IAccountService accountService, IEmailService emailService, ICustomerService customerService)
        {
            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));

            if (customerService == null)
                throw new ArgumentNullException(nameof(customerService));

            _accountService = accountService;
            _emailService = emailService;
            _customerService = customerService;
        }

        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(BalanceChangedMessage message)
        {
            var account = _accountService.FindAccount(message.AccountNumber);
            var customer = _customerService.FindCustomerByAccount(message.AccountNumber);

            Debug.Assert(account != null);
            Debug.Assert(customer != null);

            // build body and subject
            var visitor = new NotificationCustomerVisitor(account, message);
            customer.Accept(visitor);

            // schedule the email
            _emailService.ScheduleEmail(customer.Email, visitor.Subject, visitor.Body);
        }
    }
}