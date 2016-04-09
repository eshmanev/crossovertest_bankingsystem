using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for PinView.xaml
    /// </summary>
    public partial class PinView : UserControl
    {
        public PinView(PinViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void PinBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}