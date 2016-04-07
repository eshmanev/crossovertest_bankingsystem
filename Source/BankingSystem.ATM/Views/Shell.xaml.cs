using System.Windows;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Shell" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Shell(ShellViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}