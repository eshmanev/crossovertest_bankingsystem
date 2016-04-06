using BankingSystem.Data;

namespace BankingSystem.DAL.Entities
{
    /// <summary>
    ///     Represents a bank balance.
    /// </summary>
    public class BankBalance : IBankBalance
    {
        /// <summary>
        ///     Gets or sets the tatal balance.
        /// </summary>
        /// <value>
        ///     The total balance.
        /// </value>
        public virtual decimal TotalBalance { get; set; }
    }
}