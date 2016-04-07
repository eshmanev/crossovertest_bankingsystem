using System.Windows.Input;

namespace BankingSystem.ATM
{
    /// <summary>
    ///     Contains a list of commonly used commands
    /// </summary>
    public static class GlobalCommands
    {
        /// <summary>
        ///     Gets or sets the finish command.
        /// </summary>
        /// <value>
        ///     The finish command.
        /// </value>
        public static ICommand FinishCommand { get; set; }
    }
}