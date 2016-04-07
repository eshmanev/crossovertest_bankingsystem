namespace BankingSystem.ATM
{
    /// <summary>
    ///     Defines a special interface which allows to bind non-bindable controls.
    /// </summary>
    /// <typeparam name="T">The type of the bound parameter.</typeparam>
    public interface IWrappedValue<T>
    {
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        T Value { get; set; }
    }
}