/*Author: Michael
 *Date: 1/6/18 
 *Notes: 
 * btnDeleteAccounts -Add drop tables for users deleted tables -done
 * Transfer color set to green
 * Add edit button for transactions - needs implementation
 * Add edit button for accounts
 * Add tabs at bottom of ledger tab for transfer and transactions - done
 * 
 */

using StewLedger.Model;
using StewLedger.Util;
using StewLedger.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace StewLedger.View
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : Window
    {

        LedgerVM ledgerVM = new LedgerVM();
       
        
        public DashBoardView()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;

            ledgerVM.UpdateAutoTransfer();
            ledgerVM.UpdateAutoTransaction();
            ledgerVM.LoadAccountData(this);

            test();
        }

        private void MenuBank_Click(object sender, RoutedEventArgs e)
        {
            BankView bv = new BankView();
            bv.ShowDialog();
        }


        private void MenuExitProgram_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Move the following code to the DashBoardVM
        /// </summary>
        private AccountArgs args = new AccountArgs();
        private AccountNotifier accountNotifier = new AccountNotifier();
        private AccountModel accountModel = new AccountModel();
        UpdateGrids updateGrids = new UpdateGrids();
        public AccountModel AccountModel
        {
            get => accountModel;

            set
            {
                accountModel = value;
                args.Account = value;
                if (args != null) { accountNotifier.OnAccountChanged(this, args); }
            }
        }


        private void gridAccounts_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            accountNotifier.accountChanged += AccountMediator.BroadcastAccounts;
            AccountModel = (AccountModel)gridAccounts.SelectedItem;

            updateGrids.Update(this);
            
        }

        private void menuAutoTransaction_Click(object sender, RoutedEventArgs e)
        {
            AutoTransactView view = new AutoTransactView();
            view.ShowDialog();
        }

        private void test()
        {
            ((PieSeries)PieChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Food", 1000),
                    new KeyValuePair<string, int>("Entertainment", 2300)
                };
        }
    }
}
