/*Author: Michael Stewart
 *Date: 1/4/2018
 *Notes: 
 * Build all tables in login button. Implement loading progress bar
 */

using System;
using System.Windows;
using StewLedger.View;
using StewLedger.ViewModel;
using StewLedger.Util;
using Database.Utils;
using StewLedger.Tables.Accounts;
using System.IO;


namespace StewLedger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnectionString connectionString = 
            new ConnectionString(@"data source=|DataDirectory|StewLedgerDB.db;Foreign Keys=True");
        UserAccountVM user;
        DashBoardView wnd;
    
        private AccountTable accountTable = new AccountTable();
        
        private Connection conn;


        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserNewAccountView view = new UserNewAccountView();
                view.ShowDialog();
            }catch(Exception ex)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(folder + "/StewLedger/Data") == false)
                {
                    Directory.CreateDirectory(folder + "/StewLedger/Data");
                    Util.FileHandler fh = new Util.FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(ex.Message);
                }
                else
                {
                    Util.FileHandler fh = new Util.FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(ex.Message);
                }

               
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            using (user = new UserAccountVM())
            { 
                if(tbUserName.Text == "" || tbPassword.Text == "")
                {
                    lblInfo.Content = "Enter User Name and Password";
                }
                else if (user.VerifyPassword(tbUserName.Text, tbPassword.Text) == true)
                {

                    user.SetGlobalUser(tbUserName.Text);

                    using (conn = new Connection(ConnectionString.connectionString))
                    {
                        //Table Initialization
                        if (conn.TableExist(User._username + "Accounts") == false)
                        {
                            conn.NonQueryCommand(accountTable.AddTable(User._username));
                        }
                       
                    }

                    wnd = new DashBoardView();
                    
                        this.Close();
                        wnd.ShowDialog();
                    
                }else
                {
                    lblInfo.Content = "Invalid User Name or Password";
                }

            }
        }

       


       
    }
}
