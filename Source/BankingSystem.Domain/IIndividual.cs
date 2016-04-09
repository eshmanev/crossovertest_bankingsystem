namespace BankingSystem.Domain
{
    /// <summary>
    ///     Defines an individual.
    /// </summary>
    public interface IIndividual : ICustomer
    {
        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        string FirstName { get; }

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        string LastName { get; }
    }
}