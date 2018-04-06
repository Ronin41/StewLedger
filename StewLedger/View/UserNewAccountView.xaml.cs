/*
 * Creator: Michael Stewart
 * Date: 1/3/2018
 * Notes:
 *      - Add password constraints and database protections
 */


using System.Windows;
using System.Windows.Controls;
using Database.Utils;
using StewLedger.Tables.Users;
using StewLedger.Util;

namespace StewLedger.View
{
    /// <summary>
    /// Interaction logic for UserNewAccountView.xaml
    /// </summary>
    public partial class UserNewAccountView : Window, IHavePassword
    {
       
        Connection conn;
        UserTable ut = new UserTable();
       
        public UserNewAccountView()
        {
            InitializeComponent();

            //Make this using get rid of dispose code
            using (conn = new Connection(ConnectionString.connectionString))
            {
                if (conn.TableExist("MasterUserTable") == false)
                    conn.NonQueryCommand(ut.AddUserTable());
            }
        }

        public System.Security.SecureString Password
        {
            get
            {
                return PasswordBox.SecurePassword;
            }
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
    }
}
