using System.Collections.Generic;

namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    ///     Provider error details.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the details.
        /// </summary>
        /// <value>
        ///     The details.
        /// </value>
        public Dictionary<string, string> Details { get; set; }
    }
}