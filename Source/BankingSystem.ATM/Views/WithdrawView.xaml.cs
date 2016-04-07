using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}