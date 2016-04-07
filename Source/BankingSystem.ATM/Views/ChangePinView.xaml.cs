using System.Windows.Controls;
using BankingSystem.ATM.ViewModels;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Interaction logic for ChangePinView.xaml
    /// </summary>
    public partial class ChangePinView : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePinView" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ChangePinView(ChangePinViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}