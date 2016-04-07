using System.Windows.Threading;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Defines an accessor for application dispatcher.
    /// </summary>
    public interface IDispatcherAccessor
    {
        /// <summary>
        ///     Gets the dispatcher.
        /// </summary>
        /// <value>
        ///     The dispatcher.
        /// </value>
        Dispatcher Dispatcher { get; }
    }
}