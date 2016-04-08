namespace BankingSystem.Common.Data
{
    /// <summary>
    ///     Defines a customer visitor.
    /// </summary>
    public interface ICustomerVisitor
    {
        /// <summary>
        ///     Visits the specified individual.
        /// </summary>
        /// <param name="individual">The individual.</param>
        void Visit(IIndividual individual);

        /// <summary>
        ///     Visits the specified merchant.
        /// </summary>
        /// <param name="merchant">The merchant.</param>
        void Visit(IMerchant merchant);
    }
}