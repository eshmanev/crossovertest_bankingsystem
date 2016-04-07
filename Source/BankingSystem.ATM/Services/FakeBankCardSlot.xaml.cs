using System.Windows;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Interaction logic for FakeBankCardSlot.xaml
    /// </summary>
    public partial class FakeBankCardSlot : Window
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FakeBankCardSlot" /> class.
        /// </summary>
        public FakeBankCardSlot()
        {
            InitializeComponent();

            // add hard-coded bank card numbers
            listBox.Items.Add("1111222233331111");
            listBox.Items.Add("2111222233331111");
            listBox.Items.Add("3111222233331111");
            listBox.Items.Add("1111222233332222");
            listBox.Items.Add("2111222233332222");
            listBox.Items.Add("3111222233332222");
            listBox.Items.Add("1111222233333333");
            listBox.Items.Add("1111222233334444");
            listBox.Items.Add("2111222233334444");
            listBox.Items.Add("1111222233335555");
            listBox.Items.Add("2111222233335555");
        }

        /// <summary>
        ///     Gets the selected card.
        /// </summary>
        /// <value>
        ///     The selected card.
        /// </value>
        public string SelectedCard => listBox.SelectedItem as string;
    }
}