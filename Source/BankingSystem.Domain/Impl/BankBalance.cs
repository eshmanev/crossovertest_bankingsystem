namespace BankingSystem.Domain.Impl
{
    /// <summary>
    ///     Represents a bank balance.
    /// </summary>
    internal class BankBalance : IBankBalance
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets or sets the tatal balance.
        /// </summary>
        /// <value>
        ///     The total balance.
        /// </value>
        public virtual decimal TotalBalance { get; set; }
    }
}