using System.Windows.Controls;
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
    }
}