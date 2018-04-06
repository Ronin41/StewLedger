
using StewLedger.Model;
using StewLedger.ViewModel;
using System.Windows;

namespace StewLedger.View
{
    /// <summary>
    /// Interaction logic for AutoTransact.xaml
    /// </summary>
    public partial class AutoTransactView : Window
    {
        LedgerModel ledger = new LedgerModel();

        public AutoTransactView()
        {
            InitializeComponent();
        }

        private void cmbAccountsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            grid.Items.Refresh();
          
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TabItem_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            grid.Items.Refresh();
            AutoTransactVM vM = new AutoTransactVM();
            vM.LoadTransfers();
            gridTransfer.ItemsSource = vM.TransferList;
        }
    }
}
