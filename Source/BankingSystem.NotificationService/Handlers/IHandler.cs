namespace BankingSystem.NotificationService.Handlers
{
    /// <summary>
    ///     Defines a message handler.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IHandler<in TMessage>
    {
        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(TMessage message);
    }
}