using System.Windows.Controls;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for CurrentBalanceView.xaml
    /// </summary>
    public partial class CurrentBalanceView : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrentBalanceView" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CurrentBalanceView(CurrentBalanceViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}