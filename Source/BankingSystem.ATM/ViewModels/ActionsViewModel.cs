using System.Windows.Input;
using Prism.Commands;

namespace BankingSystem.ATM.ViewModels
{
    public class ActionsViewModel
    {
        public ActionsViewModel()
        {
            CheckBalanceCommand = new DelegateCommand(CheckBalance);
            WithdrawCacheCommand = new DelegateCommand(WithdrawCache);
            ChangePinCommand = new DelegateCommand(ChangePin);
        }

        /// <summary>
        ///     Gets the check balance command.
        /// </summary>
        /// <value>
        ///     The check balance command.
        /// </value>
        public ICommand CheckBalanceCommand { get; }

        /// <summary>
        ///     Gets the withdraw cache command.
        /// </summary>
        /// <value>
        ///     The withdraw cache command.
        /// </value>
        public ICommand WithdrawCacheCommand { get; }

        /// <summary>
        ///     Gets the change pin command.
        /// </summary>
        /// <value>
        ///     The change pin command.
        /// </value>
        public ICommand ChangePinCommand { get; }

        private void ChangePin()
        {
            throw new System.NotImplementedException();
        }

        private void WithdrawCache()
        {
            throw new System.NotImplementedException();
        }

        private void CheckBalance()
        {
            throw new System.NotImplementedException();
        }
    }
}