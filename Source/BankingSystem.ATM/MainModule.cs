using BankingSystem.ATM.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace BankingSystem.ATM
{
    /// <summary>
    ///     Represents a main module.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class MainModule : IModule
    {
        private readonly IUnityContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainModule" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public MainModule(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        ///     Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _container
                .RegisterType<object, PinView>(ViewName.PinView)
                .RegisterType<object, CurrentBalanceView>(ViewName.CurrentBalanceView)
                .RegisterType<object, WithdrawView>(ViewName.WithdrawView)
                .RegisterType<object, ChangePinView>(ViewName.ChangePinView)
                .RegisterType<object, ActionsView>(ViewName.ActionsView);
        }
    }
}