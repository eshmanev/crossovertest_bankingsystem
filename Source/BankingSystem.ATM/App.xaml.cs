using System.Windows;

namespace BankingSystem.ATM
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            log4net.Config.XmlConfigurator.Configure();

            var boostrapper = new Bootstrapper();
            boostrapper.Run();
        }
    }
}