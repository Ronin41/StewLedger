using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Database.Utils;
using StewLedger.Tables.Accounts;
using StewLedger.Util;
using StewLedger.View;
using System.Windows.Media;
using static StewLedger.Tables.Base.BaseTable;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using StewLedger.Model;

namespace StewLedger.ViewModel
{
    public class AccountVM :Notifier, IDisposable
    {
        private Connection conn = new Connection(ConnectionString.connectionString);
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private AccountModel accountModel, gAccount;
        private AccountTable accountTable = new AccountTable();
        private AccountNotifier accountNotifier = new AccountNotifier();      
        private AccountArgs args = new AccountArgs();
  
       

   

        public AccountModel AccountModel { get => accountModel; set { accountModel = value; 
                OnPropertyChanged("AccountModel"); args.Account = value;
                if (args.Account.AccountName != null) accountNotifier.OnAccountChanged(this, args);
            } }

        public AccountModel GAccount
        {
            get => gAccount;
            set {
                gAccount = value;
                args.Account = value;
                if (args.Account.AccountName != null) accountNotifier.OnAccountChanged(this, args);
            }
        }
        public AccountVM()
        {
            AccountModel = new AccountModel();
        }

        public bool AddNewAccount(AccountModel account)
        {

            if (conn.NonQueryCommand(accountTable.AddAccount(account.AccountType, account.AccountName, 
                User._username, account.BankId, account.AccountNumber, account.StartingBalance, account.Balance)))
            {

                if(conn.TableExist(User._username + "TransferTable") == false)
                {
                    TransferTable transferTable = new TransferTable();
                    transferTable.CreateTable(User._username);
                }

                return true;
            }

            return false;
        }

        public Accounts LoadAccounts()
        {
            List<string> o = conn.QueryCommand(accountTable.GetAccounts(User._username));
            Accounts accounts = new Accounts();

            int numRecords = 0;

            numRecords = o.Count / 7;

            int item = 0, item1 = 1, item2 = 2, item3 = 3, item4 = 4, item5 = 5;

            for (int i = 0; i < numRecords; i++)
            {
                AccountModel accountModel = new AccountModel
                {
                    Id = Convert.ToInt16(o[item]),
                    AccountName = o[item1],
                    AccountType = o[item2],
                    Balance = Convert.ToDouble(o[item4]),
                    StartingBalance = Convert.ToDouble(o[item3]),
                    AccountNumber = o[item5]
                };

                item += 7;
                item1 += 7;
                item2 += 7;
                item3 += 7;
                item4 += 7;
                item5 += 7;

                accounts.addAccount(accountModel);
            }



            return accounts;
        }


        public bool UpdateBalance(double balance, int id)
        {
            AccountModel accountModel = new AccountModel();

            if (conn.NonQueryCommand(accountTable.UpdateBalance(User._username, balance, id)))
            {
                return true;
            }

            return false;
        }



        public void DeleteAccount(DashBoardView dashBoard)
        {
            AccountTable accountTable = new AccountTable();
            LedgerTable ledgerTable = new LedgerTable();
            AccountModel = AccountMediator.Account;
            UpdateGrids updateGrids = new UpdateGrids();



            using (Connection conn = new Connection(ConnectionString.connectionString))
            {

                TableInfo tableInfo = new TableInfo
                {
                    Id = AccountModel.Id,
                    Owner = User._username,
                    TableName = "Accounts",
                    T = TableInfo.Table.ACCT
                };

                conn.NonQueryCommand(accountTable.RemoveRow(tableInfo));
                if (conn.TableExist(User._username + AccountModel.AccountName + "Ledger") == true)
                {
                    conn.NonQueryCommand(ledgerTable.DropTable(User._username, AccountModel.AccountName));
                }

                dashBoard.gridAccounts.ItemsSource = LoadAccounts();


            };


            updateGrids.Update(dashBoard);


        }


        public void Dispose()
        {
            Dispose(false);
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


    public class AccountTypes : ObservableCollection<string>
    {
        public AccountTypes()
        {
            this.Add("Checking");
            this.Add("Savings");
            //Add credit and loans later will require seperate setup
        }
    }

    public class Accounts : ObservableCollection<AccountModel>
    {
        
        public Accounts()
        {
    
        }
        public void addAccount(AccountModel account)
        {
            this.Add(account);
        }

        public ObservableCollection<AccountModel> GetAccounts()
        {
            return this;
        }

        public void deleteAllAccounts()
        {
            this.Clear();
        }
       
    }
}
