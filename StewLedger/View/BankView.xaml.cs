/*
 * Creator: Michael Stewart
 * Date: 1/3/2018
 * Notes:
 *      - Format phone number text (***)*** - **** Done
 *      - Add constraints for zipcode length (10) *****-****? Done
 */

using System.Windows;
using Database.Utils;
using StewLedger.Tables.Banks;
using StewLedger.Util;


namespace StewLedger.View
{
    /// <summary>
    /// Interaction logic for BankView.xaml
    /// </summary>
    public partial class BankView : Window
    {
        Connection conn;
        BankTable bankTable = new BankTable();
        

        public BankView()
        {
            InitializeComponent();

            using (conn = new Connection(ConnectionString.connectionString))
            {

                if (conn.TableExist(User._username + "MasterBankTable") == false)
                {
                    conn.NonQueryCommand(bankTable.AddBankTable(User._username));
                }
            }

            cmbStates.SelectedIndex = 0;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
