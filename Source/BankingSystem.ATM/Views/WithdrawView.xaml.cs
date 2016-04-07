using System.Windows.Controls;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for WithdrawView.xaml
    /// </summary>
    public partial class WithdrawView : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WithdrawView" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public WithdrawView(WithdrawViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}