using System;
using System.Text;
using BankingSystem.Common.Data;
using BankingSystem.Common.Messages;

namespace BankingSystem.NotificationService.Handlers
{
    /// <summary>
    ///     Represents a customer visitor which builds notification email body.
    /// </summary>
    /// <seealso cref="BankingSystem.Common.Data.ICustomerVisitor" />
    internal class NotificationCustomerVisitor : ICustomerVisitor
    {
        private readonly IAccount _account;
        private readonly BalanceChangedMessage _message;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotificationCustomerVisitor" /> class.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public NotificationCustomerVisitor(IAccount account, BalanceChangedMessage message)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            _account = account;
            _message = message;
        }

        /// <summary>
        ///     Gets the result subject.
        /// </summary>
        /// <value>
        ///     The result subject.
        /// </value>
        public string Subject { get; private set; }

        /// <summary>
        ///     Gets the result body.
        /// </summary>
        /// <value>
        ///     The result body.
        /// </value>
        public string Body { get; private set; }

        /// <summary>
        ///     Visits the specified individual.
        /// </summary>
        /// <param name="individual">The individual.</param>
        public void Visit(IIndividual individual)
        {
            Subject = "Balance of your account has changed";

            // build message body
            var builder = new StringBuilder();
            builder.AppendLine($"Dear {individual.FirstName} {individual.LastName},");
            builder.AppendLine();
            builder.AppendLine($"Balance of your account {_account.AccountNumber} ({_account.Currency}) has changed");
            builder.AppendLine(
                _message.ChangeAmount > 0 ?
                    $"Total income is {_message.ChangeAmount} {_message.Currency}" :
                    $"Total outcome is {Math.Abs(_message.ChangeAmount)} {_message.Currency}");
            builder.AppendLine();
            builder.AppendLine("Best regards.");
            Body = builder.ToString();
        }

        /// <summary>
        ///     Visits the specified merchant.
        /// </summary>
        /// <param name="merchant">The merchant.</param>
        public void Visit(IMerchant merchant)
        {
            // build message body
            var builder = new StringBuilder();
            builder.AppendLine($"Dear {merchant.ContactPerson},");
            builder.AppendLine();
            if (_message.ChangeAmount > 0)
            {
                Subject = "Your organization has received a new payment";
                builder.AppendLine($"Your organization {merchant.MerchantName} has received a new payment.");
                builder.AppendLine($"Total payment sum is {_message.ChangeAmount} {_message.Currency}.");
            }
            else
            {
                Subject = "Your organization has paid some expences";
                builder.AppendLine($"Your organization {merchant.MerchantName} has paid some expences.");
                builder.AppendLine($"Total payment sum is {Math.Abs(_message.ChangeAmount)} {_message.Currency}.");
            }

            builder.AppendLine("Best regards.");
            Body = builder.ToString();
        }
    }
}