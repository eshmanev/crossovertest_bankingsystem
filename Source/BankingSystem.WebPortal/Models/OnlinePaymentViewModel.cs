using System;
using System.Collections.Generic;

namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    ///     Represents a view model for online payment page.
    /// </summary>
    public class OnlinePaymentViewModel
    {
        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        ///     The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        ///     Gets or sets the currency.
        /// </summary>
        /// <value>
        ///     The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the name of the card holder.
        /// </summary>
        /// <value>
        ///     The name of the card holder.
        /// </value>
        public string CardHolderName { get; set; }

        /// <summary>
        ///     Gets or sets the card number.
        /// </summary>
        /// <value>
        ///     The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        ///     Gets or sets the month expired.
        /// </summary>
        /// <value>
        ///     The month expired.
        /// </value>
        public int MonthExpired { get; set; }

        /// <summary>
        ///     Gets or sets the months.
        /// </summary>
        /// <value>
        ///     The months.
        /// </value>
        public List<int> Months { get; set; }

        /// <summary>
        ///     Gets or sets the year expired.
        /// </summary>
        /// <value>
        ///     The year expired.
        /// </value>
        public int YearExpired { get; set; }

        /// <summary>
        ///     Gets or sets the years.
        /// </summary>
        /// <value>
        ///     The years.
        /// </value>
        public List<int> Years { get; set; }

        /// <summary>
        ///     Gets or sets the security code.
        /// </summary>
        /// <value>
        ///     The security code.
        /// </value>
        public string SecurityCode { get; set; }
    }
}