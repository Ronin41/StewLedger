using System;
using System.Collections.Generic;
using StewLedger.Util;
using StewLedger.View;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Windows;
using StewLedger.Model;
using StewLedger.Tables.Users;
using Database.Utils;
using System.Security;
using StewLedger.Tables.Accounts;
using StewLedger.Tables.Banks;

namespace StewLedger.ViewModel
{
    public class UserAccountVM : Notifier, IDisposable
    {
        private UserAccountModel userModel;
        private UserTable userTable = new UserTable();
        private Hash hash = new Hash();
        private Connection conn = new Connection(ConnectionString.connectionString);
        private User globalUser;
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public UserAccountModel UserModel { get => userModel; set { userModel = value; OnPropertyChanged("UserModel"); } }

        public UserAccountVM()
        {
            this.userModel = new UserAccountModel();
        }

        public bool AddNewUser(UserAccountModel account)
        {
            string newHash = hash.GetMd5Hash(account.Password);

            if (conn.NonQueryCommand(
             userTable.AddUser(account.First, account.Last, newHash, account.UserName, "MasterUserTable")
             ) == true)
            {
                return true;
            }

            return false;
        }

        public bool VerifyPassword(string username, string password)
        {
            try
            {

                List<string> o = conn.QueryCommand(userTable.FindAccount(username));
                string storedPass = o[3].ToString();

                if (hash.VerifyMd5Hash(password, storedPass) == true)
                {
                    return true;
                }
            }
            catch (Exception e)
            {

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(folder + "/StewLedger/Data") == false)
                {
                    Directory.CreateDirectory(folder + "/StewLedger/Data");
                    Util.FileHandler fh = new Util.FileHandler(folder + "/StewLedger/Data", "ErrorLog.txt");
                    fh.Write(e.Message);
                }
                else
                {
                    Util.FileHandler fh = new Util.FileHandler(folder + "/StewLedger/Data", "ErrorLog.txt");
                    fh.Write(e.Message);
                }

                return false;
            }

            return false;
        }

        public void SetGlobalUser(string username)
        {
            List<string> o = conn.QueryCommand(userTable.FindAccount(username));

            globalUser = new User(Convert.ToInt16(o[0]), o[4]);

        }

        public void DeleteUserAccount(DashBoardView dashBoard, MainWindow mw)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Accounts accounts = new Accounts();
                AccountVM accountVM = new AccountVM();

                BankList bankList = new BankList();
                BankVM bankVM = new BankVM();

                LedgerTable ledgerTable = new LedgerTable();
                AccountTable accountTable = new AccountTable();
                BankTable bankTable = new BankTable();



                accounts = accountVM.LoadAccounts();
                bankList = bankVM.LoadBanks();
                

                if (accounts.Count > 0)
                {
                    using (Connection conn = new Connection(ConnectionString.connectionString))
                    {

                        for (int i = 0; i < accounts.Count; i++)
                        {
                            conn.NonQueryCommand(ledgerTable.DropTable(User._username, accounts[i].AccountName));
                            
                        }
                    }
                }

                using (Connection conn = new Connection(ConnectionString.connectionString))
                {
 
                        conn.NonQueryCommand(accountTable.DropTable(User._username)); 
                }

                using (Connection conn = new Connection(ConnectionString.connectionString))
                {

                         conn.NonQueryCommand(bankTable.DropTable(User._username));
                }

                UserTable userTable = new UserTable();

                using (Connection conn = new Connection(ConnectionString.connectionString))
                {
                    conn.NonQueryCommand(userTable.DeleteAccount(User._userId));
                    dashBoard.Close();

                    mw = new MainWindow();
                    
                        mw.ShowDialog();
                   
                };
            }
        }



        private void Save(object parameter)
        {


            var passwordContainer = parameter as IHavePassword;

            if(passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                userModel.Password = ConvertToUnsecureString(secureString);
            }

            if(AddNewUser(userModel) == true)
            {

                MessageBox.Show("Account Created");
            }
            else
            {
                MessageBox.Show("Account Could Not Be Created");
            }
        }

        private void ClearText(object parameter)
        {
            userModel.First = "";
            userModel.Last = "";
            userModel.UserName = "";
            userModel.Password = "";
        }


        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }


        //Relay Commands
        private RelayCommand<UserNewAccountView> saveUserAccount = null;
        private RelayCommand<UserAccountModel> clearUserText = null;
        
        

        public RelayCommand<UserNewAccountView> SaveUserAccount => saveUserAccount ??
            (saveUserAccount = new RelayCommand<UserNewAccountView>(Save, CanSaveUser));

        public RelayCommand<UserAccountModel> ClearUserText => clearUserText ??
            (clearUserText = new RelayCommand<UserAccountModel>(ClearText, CanClearText));

        private bool CanSaveUser(UserNewAccountView account) => account != null;
        private bool CanClearText(UserAccountModel account) => account != null;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            //Free managed resources
            if (disposing)
            {
                handle.Dispose();
                conn.Dispose();
            }

            disposed = true;
        }

    }
}
