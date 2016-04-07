using System.Windows.Controls;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for ActionsView.xaml
    /// </summary>
    public partial class ActionsView : UserControl
    {
        public ActionsView(ActionsViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}