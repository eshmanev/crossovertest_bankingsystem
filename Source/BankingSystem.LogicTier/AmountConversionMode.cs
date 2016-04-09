namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a currency conversion mode.
    /// </summary>
    public enum AmountConversionMode
    {
        /// <summary>
        ///     Converts the amount from currency of source account to currency of destination account.
        /// </summary>
        SourceToTarget,

        /// <summary>
        ///     Converts the amount from currency of destination account to currency of source account.
        /// </summary>
        TargetToSource
    }
}