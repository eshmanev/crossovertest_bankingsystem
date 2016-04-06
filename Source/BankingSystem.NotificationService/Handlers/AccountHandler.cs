﻿using System;
using System.Diagnostics;
using System.Text;
using BankingSystem.Common.Messages;
using BankingSystem.LogicTier;

namespace BankingSystem.NotificationService.Handlers
{
    /// <summary>
    ///     Represents a handler of account-specific messages.
    /// </summary>
    public class AccountHandler : IHandler<BalanceChangedMessage>
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountHandler" /> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// <param name="emailService">The email service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public AccountHandler(IAccountService accountService, IEmailService emailService)
        {
            if (accountService == null)
                throw new ArgumentNullException(nameof(accountService));

            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));

            _accountService = accountService;
            _emailService = emailService;
        }

        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(BalanceChangedMessage message)
        {
            var account = _accountService.FindAccount(message.AccountNumber);
            Debug.Assert(account != null);

            // build message body
            var builder = new StringBuilder();
            builder.AppendLine($"Dear {account.Customer.FirstName} {account.Customer.LastName},");
            builder.AppendLine();
            builder.AppendLine($"Balance of your account {account.AccountNumber} ({account.Currency}) has changed");
            builder.AppendLine(
                message.ChangeAmount > 0 ?
                    $"Total income is {message.ChangeAmount} {message.Currency}" :
                    $"Total outcome is {Math.Abs(message.ChangeAmount)} {message.Currency}");
            builder.AppendLine();
            builder.AppendLine("Best regards.");

            // schedule the email
            _emailService.ScheduleEmail(account.Customer.Email, "Balance of your account has changed", builder.ToString());
        }
    }
}