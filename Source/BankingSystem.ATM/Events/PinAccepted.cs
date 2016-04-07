namespace BankingSystem.ATM.Events
{
    /// <summary>
    ///     Represents an empty payload.
    /// </summary>
    public class EmptyPayload
    {
        /// <summary>
        ///     The instance.
        /// </summary>
        public static readonly EmptyPayload Instance = new EmptyPayload();
    }

    public class PinAcceptedEvent : Prism.Events.PubSubEvent<EmptyPayload>
    {
    }
}