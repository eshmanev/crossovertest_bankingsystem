using System.Windows;
using BankingSystem.ATM.Services;
using BankingSystem.ATM.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace BankingSystem.ATM
{
    /// <summary>
    ///     Represents the application boostrapper.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        ///     The shell of the application.
        /// </returns>
        protected override DependencyObject CreateShell()
        {
            var shell = Container.Resolve<Shell>();
            Application.Current.MainWindow = shell;
            Application.Current.MainWindow.Show();
            return shell;
        }

        /// <summary>
        ///     Creates the <see cref="T:Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof (MainModule));
            return catalog;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // services
            Container
                .RegisterType<ICredentialsProvider, FakeCredentialsProvider>()
                .RegisterType<IBankingService, BankingServiceProxy>();
        }
    }
}