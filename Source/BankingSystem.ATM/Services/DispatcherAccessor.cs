using System.Windows;
using System.Windows.Threading;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents an accessor for application dispatcher.
    /// </summary>
    /// <seealso cref="BankingSystem.ATM.Services.IDispatcherAccessor" />
    public class DispatcherAccessor : IDispatcherAccessor
    {
        /// <summary>
        ///     Gets the dispatcher.
        /// </summary>
        /// <value>
        ///     The dispatcher.
        /// </value>
        public Dispatcher Dispatcher => Application.Current.Dispatcher;
    }
}