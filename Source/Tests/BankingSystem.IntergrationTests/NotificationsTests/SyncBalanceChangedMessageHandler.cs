using System.Threading;
using BankingSystem.Messages;
using BankingSystem.NotificationService.Handlers;

namespace BankingSystem.IntegrationTests.NotificationsTests
{
    /// <summary>
    ///     Represents a synchronized handler of the <see cref="BalanceChangedMessage" />.
    /// </summary>
    internal class SyncBalanceChangedMessageHandler : IHandler<BalanceChangedMessage>
    {
        private readonly IHandler<BalanceChangedMessage> _original;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncBalanceChangedMessageHandler" /> class.
        /// </summary>
        /// <param name="original">The original.</param>
        public SyncBalanceChangedMessageHandler(IHandler<BalanceChangedMessage> original)
        {
            _original = original;
            SyncRoot = new AutoResetEvent(false);
        }

        /// <summary>
        ///     Gets the synchronize root.
        /// </summary>
        /// <value>
        ///     The synchronize root.
        /// </value>
        public AutoResetEvent SyncRoot { get; }

        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(BalanceChangedMessage message)
        {
            _original.Handle(message);
            SyncRoot.Set();
        }
    }
}