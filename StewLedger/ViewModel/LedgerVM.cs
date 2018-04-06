using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Database.Utils;
using Microsoft.Win32.SafeHandles;
using StewLedger.Data;
using StewLedger.Model;
using StewLedger.Tables.Accounts;
using StewLedger.Util;
using StewLedger.View;

namespace StewLedger.ViewModel
{
    public class LedgerVM : IDisposable
    {

        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private LedgerTable ledgerTable = new LedgerTable();
        private AccountModel account = new AccountModel();

        public LedgerVM()
        {

        }

        /// <summary>
        /// Add transaction row to database
        /// </summary>
        /// <param name="vM"></param>
        /// <param name="acctName"></param>
        public void AddNewTransaction(LedgerModel ledger, string acctName)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(ledgerTable.AddTransact(ledger.Name, ledger.Amount, ledger.Comments,
                ledger.Date, ledger.TransType, ledger.CatType, ledger.AccId, User._username, ledger.Auto, 
                ledger.AutoDate, acctName));
            }
        }

        public void DeleteTransaction(LedgerModel ledger, string acctName)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(ledgerTable.DeleteTransact(User._username, acctName, ledger.LedgerId));
            }
        }



        public void DeleteTransaction(DashBoardView boardView)
        {
            AccountModel accountModel = AccountMediator.Account;
            AccountModel model = new AccountModel();
            LedgerModel ledgerModel = (LedgerModel)boardView.gridLedger.SelectedItem;
            LedgerModel ledgerModel2 = new LedgerModel();
            AccountVM accountVM = new AccountVM();
            Accounts accounts = accountVM.LoadAccounts();
            Transaction transaction = new Transaction();
            Transaction transaction2 = new Transaction();
            TransferTracker transferTracker = new TransferTracker();
            TransferList transferList = transferTracker.LoadTransfers(false);
            TransferTable transferTable = new TransferTable();
            int transferId = -1;

            switch (ledgerModel.Name)
            {
                case "Transfer":
                    {
                        using (Connection conn = new Connection(ConnectionString.connectionString))
                        {


                            if (ledgerModel.TransType == "Deposit")
                            {
                                foreach (TransferModel t in transferList)
                                {
                                    if (t.DestId == ledgerModel.LedgerId)
                                    {
                                        transferId = t.Id;

                                        DeleteTransaction(ledgerModel, t.Dest);
                                        transaction2 = LoadLedger(t.SourceAcctId, t.Source, false, null);


                                        foreach (LedgerModel l in transaction2)
                                        {
                                            if (l.LedgerId == t.SourceId)
                                            {
                                                ///////////////////////////////////////////////////////////////////////////
                                                foreach (AccountModel a in accounts)
                                                {
                                                    if (a.Id == t.SourceAcctId)
                                                    {
                                                        model = a;
                                                    }
                                                }
                                                ////////////////////////////////////////////////////////////////////////////

                                                if (conn.TableExist(User._username + model.AccountName + "Ledger") == true)
                                                {
                                                    DeleteTransaction(ledgerModel, t.Source);
                                                    transaction2 = LoadLedger(t.SourceAcctId, t.Source, false, null);

                                                    /*******************************************
                                                       * Gets selected balance from account list
                                                      *****************************************/
                                                    for (int i = 0; i < accounts.Count; i++)
                                                    {
                                                        if (accounts[i].Id == model.Id)
                                                        {
                                                            model.Balance = accounts[i].StartingBalance;
                                                        }
                                                    }

                                                    if (transaction2.Count > 0)
                                                    {
                                                        model.Balance = CalcBalanceRecursive(model.StartingBalance, transaction2, 0);


                                                        for (int i = 0; i < accounts.Count; i++)
                                                        {
                                                            if (accounts[i].Id == model.Id)
                                                            {
                                                                accounts[i].Balance = model.Balance;
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }

                                if (transaction2.Count > 0)
                                {
                                    accountVM.UpdateBalance(transaction2[transaction2.Count - 1].Balance, model.Id);
                                }
                                else
                                {
                                    accountVM.UpdateBalance(model.StartingBalance, model.Id);
                                }


                            }
                            else if (ledgerModel.TransType == "Withdrawal")
                            {
                                foreach (TransferModel t in transferList)
                                {
                                    if (t.SourceId == ledgerModel.LedgerId)
                                    {
                                        transferId = t.Id;

                                        DeleteTransaction(ledgerModel, t.Source);
                                        transaction2 = LoadLedger(t.DestAcctId, t.Dest, false, null);


                                        foreach (LedgerModel l in transaction2)
                                        {
                                            if (l.LedgerId == t.DestId)
                                            {
                                                ///////////////////////////////////////////////////////////////////////////
                                                foreach (AccountModel a in accounts)
                                                {
                                                    if (a.Id == t.DestAcctId)
                                                    {
                                                        model = a;
                                                    }
                                                }
                                                ////////////////////////////////////////////////////////////////////////////

                                                if (conn.TableExist(User._username + model.AccountName + "Ledger") == true)
                                                {
                                                    DeleteTransaction(ledgerModel, t.Dest);
                                                    transaction2 = LoadLedger(t.DestAcctId, t.Dest, false, null);

                                                    /*******************************************
                                                       * Gets selected balance from account list
                                                      *****************************************/
                                                    for (int i = 0; i < accounts.Count; i++)
                                                    {
                                                        if (accounts[i].Id == model.Id)
                                                        {
                                                            model.Balance = accounts[i].StartingBalance;
                                                        }
                                                    }

                                                    if (transaction2.Count > 0)
                                                    {
                                                        model.Balance = CalcBalanceRecursive(model.StartingBalance, transaction2, 0);


                                                        for (int i = 0; i < accounts.Count; i++)
                                                        {
                                                            if (accounts[i].Id == model.Id)
                                                            {
                                                                accounts[i].Balance = model.Balance;
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }

                                if (transaction2.Count > 0)
                                {
                                    accountVM.UpdateBalance(transaction2[transaction2.Count - 1].Balance, model.Id);
                                }
                                else
                                {
                                    accountVM.UpdateBalance(model.StartingBalance, model.Id);
                                }

                            }

                            if (conn.TableExist(User._username + accountModel.AccountName + "Ledger") == true)
                            {


                                transaction = LoadLedger(accountModel.Id, accountModel.AccountName, false, null);


                                /*****************************************
                                * Gets selected balance from account list
                                *****************************************/
                                for (int i = 0; i < accounts.Count; i++)
                                {
                                    if (accounts[i].Id == accountModel.Id)
                                    {
                                        accountModel.Balance = accounts[i].StartingBalance;
                                    }
                                }

                                if (transaction.Count > 0)
                                {

                                    accountModel.Balance = CalcBalanceRecursive(accountModel.StartingBalance, transaction, 0);

                                    for (int i = 0; i < accounts.Count; i++)
                                    {
                                        if (accounts[i].Id == accountModel.Id)
                                        {
                                            accounts[i].Balance = accountModel.Balance;
                                        }
                                    }
                                }

                                if (transaction != null)
                                {

                                    for (int i = 0; i < transaction.Count; i++)
                                    {
                                        if (transaction[i].TransType == "Withdrawal")
                                        {
                                            transaction[i].Color = new SolidColorBrush(Colors.Red);
                                        }
                                        else if (transaction[i].TransType == "Deposit")
                                        {
                                            transaction[i].Color = new SolidColorBrush(Colors.SteelBlue);
                                        }
                                        else
                                        {
                                            transaction[i].Color = new SolidColorBrush(Colors.Green);
                                        }
                                    }
                                }
                            }

                            if (transaction.Count > 0)
                            {
                                accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModel.Id);
                            }
                            else
                            {
                                accountVM.UpdateBalance(accountModel.StartingBalance, accountModel.Id);
                            }
                        }

                            accounts = accountVM.LoadAccounts();

                            transferTable.DeleteRow(User._username,  transferId);

                            boardView.gridLedger.ItemsSource = transaction;
                            boardView.gridAccounts.ItemsSource = accounts;
                        
                    } break;

                default:
                {
                        using (Connection conn = new Connection(ConnectionString.connectionString))
                        {
                            if (conn.TableExist(User._username + accountModel.AccountName + "Ledger") == true)
                            {
                                DeleteTransaction(ledgerModel, accountModel.AccountName);
                            }
                            transaction = LoadLedger(accountModel.Id, accountModel.AccountName, false, null);


                            /*****************************************
                            * Gets selected balance from account list
                            *****************************************/
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                if (accounts[i].Id == accountModel.Id)
                                {
                                    accountModel.Balance = accounts[i].StartingBalance;
                                }
                            }

                            if (transaction.Count > 0)
                            {

                                accountModel.Balance = CalcBalanceRecursive(accountModel.StartingBalance, transaction, 0);

                                for (int i = 0; i < accounts.Count; i++)
                                {
                                    if (accounts[i].Id == accountModel.Id)
                                    {
                                        accounts[i].Balance = accountModel.Balance;
                                    }
                                }
                            }

                            if (transaction != null)
                            {

                                for (int i = 0; i < transaction.Count; i++)
                                {
                                    if (transaction[i].TransType == "Withdrawal")
                                    {
                                        transaction[i].Color = new SolidColorBrush(Colors.Red);
                                    }
                                    else if (transaction[i].TransType == "Deposit")
                                    {
                                        transaction[i].Color = new SolidColorBrush(Colors.SteelBlue);
                                    }
                                    else
                                    {
                                        transaction[i].Color = new SolidColorBrush(Colors.Green);
                                    }
                                }
                            }
                        }

                        if (transaction.Count > 0)
                        {
                            accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModel.Id);
                        }
                        else
                        {
                            accountVM.UpdateBalance(accountModel.StartingBalance, accountModel.Id);
                        }


                        accounts = accountVM.LoadAccounts();

                        boardView.gridLedger.ItemsSource = transaction;
                        boardView.gridAccounts.ItemsSource = accounts;

                    }break;
            
        }
    }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acctId"></param>
        /// <param name="acctName"></param>
        /// <param name="autoOnly">true if returning only automated transactions</param>
        /// <param name="ledgerId">if not used make null</param>
        /// <returns></returns>
        public Transaction LoadLedger(int acctId, string acctName, bool autoOnly, int? ledgerId)
        {
            Transaction transaction = new Transaction();

            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                if (conn.TableExist(User._username + acctName + "Ledger") == true)
                {
                    LedgerTable ledgerTable = new LedgerTable();

                    List<string> list = conn.QueryCommand(ledgerTable.GetLedger(User._username, acctName));

                    int numRecords = list.Count / 10;

                    int item = 0, item1 = 1, item2 = 2, item3 = 3, item4 = 4, item5 = 5, item6 = 6
                        , item7 = 7, item8 =8, item9=9;

                    for (int i = 0; i < numRecords; i++)
                    {

                        LedgerModel ledger = new LedgerModel
                        {
                            AccId = Convert.ToInt32(list[item9]),
                            LedgerId = Convert.ToInt32(list[item]),
                            Name = list[item1],
                            Date = DateTime.Parse(list[item2]),
                            TransType = list[item3],
                            Comments = list[item4],
                            Amount = Convert.ToDouble(list[item5]),
                            CatType = list[item6],
                            Auto = Convert.ToBoolean(list[item7]),
                            AutoDate = DateTime.Parse(list[item8])
                        };

                        if (autoOnly == true && ledger.Auto == true)
                        {
                            transaction.Add(ledger);
                        }else if(autoOnly == false)
                        {
                            transaction.Add(ledger);
                        }

                        item += 10;
                        item1 += 10;
                        item2 += 10;
                        item3 += 10;
                        item4 += 10;
                        item5 += 10;
                        item6 += 10;
                        item7 += 10;
                        item8 += 10;
                        item9 += 10;
                    }
                }

                if (ledgerId != null)
                {
                    Transaction temp = new Transaction();

                    foreach (LedgerModel l in transaction)
                    {
                        if(l.LedgerId == ledgerId)
                        {
                            temp.Add(l);
                        }
                    }

                    transaction = temp;
                 }

            }
       


            return transaction;
        }

        public double CalculateBalance(double currentBalance, LedgerModel ledger)
        {
            if (ledger.TransType == "Withdrawal")
            {
                currentBalance -= ledger.Amount;
            }
            else
            {
                currentBalance += ledger.Amount;
            }

            return currentBalance;
        }



        public void AddNewLedger(string owner, string accountName)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(ledgerTable.AddLedgerTable(owner, accountName));
            }
        }

        //
        public void CreateTransaction(DashBoardView dashBoard)
        {
            
            AccountModel accountModel = AccountMediator.Account;
            LedgerTable ledgerTable = new LedgerTable();
            AccountVM accountVM = new AccountVM();
            Transaction transaction = new Transaction();
            Accounts accounts = accountVM.LoadAccounts();


            if (accountModel != null)
            {
                if (accountModel.AccountName != null)
                {
                    using (Connection conn = new Connection(ConnectionString.connectionString))
                    {
                        if (conn.TableExist(User._username + accountModel.AccountName + "Ledger") == false)
                        {

                            conn.NonQueryCommand(ledgerTable.AddLedgerTable(User._username, accountModel.AccountName));

                            LedgerModel ledger = new LedgerModel
                            {
                                Name = dashBoard.tbLedgerName.Text,
                                Amount = Convert.ToDouble(dashBoard.tbLedgerAmount.Text),
                                Comments = dashBoard.tbLedgerComments.Text,
                                CatType = dashBoard.listCat.Text,
                                TransType = dashBoard.listTransact.Text,
                                Date = (DateTime)dashBoard.ledgerDate.SelectedDate,
                                Auto =  Convert.ToBoolean(dashBoard.cbAutoTransaction.IsChecked),
                                AccId = accountModel.Id
                            };

                            if(ledger.Auto == true)
                            {
                                ledger.AutoDate = DateTime.Now.AddMonths(1);
                            }

                            AddNewTransaction(ledger, accountModel.AccountName);

                            transaction = LoadLedger(accountModel.Id, accountModel.AccountName, false, null);

                            /*****************************************
                            * Gets selected balance from account list
                            *****************************************/
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                if (accounts[i].Id == accountModel.Id)
                                {
                                    accountModel.Balance = accounts[i].StartingBalance;
                                }
                            }

                            accountModel.Balance = CalcBalanceRecursive(accountModel.StartingBalance, transaction, 0);


                            for (int i = 0; i < transaction.Count; i++)
                            {
                                if (transaction[i].TransType == "Withdrawal")
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.Red);
                                }
                                else if (transaction[i].TransType == "Deposit")
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.SteelBlue);
                                }
                                else
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.Green);
                                }
                            }

                            //Adds last balance to table
                            if (transaction.Count == 0)
                            {
                                accountVM.UpdateBalance(transaction[transaction.Count].Balance, accountModel.Id);
                            }
                            else
                            {
                                accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModel.Id);
                            }

                            dashBoard.gridLedger.ItemsSource = transaction;
                        }
                        else
                        {
                            LedgerModel ledger = new LedgerModel
                            {
                                Name = dashBoard.tbLedgerName.Text,
                                Amount = Convert.ToDouble(dashBoard.tbLedgerAmount.Text),
                                Comments = dashBoard.tbLedgerComments.Text,
                                CatType = dashBoard.listCat.Text,
                                TransType = dashBoard.listTransact.Text,
                                Date = (DateTime)dashBoard.ledgerDate.SelectedDate,
                                Auto = Convert.ToBoolean(dashBoard.cbAutoTransaction.IsChecked),
                                AccId = accountModel.Id
                            };


                            if (ledger.Auto == true)
                            {
                                ledger.AutoDate = DateTime.Now.AddMonths(1);
                            }


                            AddNewTransaction(ledger, accountModel.AccountName);

                            transaction = LoadLedger(accountModel.Id, accountModel.AccountName, false, null);

                            /*****************************************
                             * Gets selected balance from account list
                             *****************************************/

                            for (int i = 0; i < accounts.Count; i++)
                            {
                                if (accounts[i].Id == accountModel.Id)
                                {
                                    accountModel.Balance = accounts[i].Balance;
                                }
                            }


                            accountModel.Balance = CalcBalanceRecursive(accountModel.StartingBalance, transaction, 0);


                            for (int i = 0; i < accounts.Count; i++)
                            {
                                if (accounts[i].Id == accountModel.Id)
                                {
                                    accounts[i].Balance = accountModel.Balance;
                                }
                            }

                            ////////////////////////////////////////////////////////////////////////////////////////////

                            for (int i = 0; i < transaction.Count; i++)
                            {
                                if (transaction[i].TransType == "Withdrawal")
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.Red);
                                }
                                else if (transaction[i].TransType == "Deposit")
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.SteelBlue);
                                }
                                else
                                {
                                    transaction[i].Color = new SolidColorBrush(Colors.Green);
                                }
                            }

                            //Adds last balance to table
                            if (transaction.Count == 0)
                            {
                                accountVM.UpdateBalance(transaction[transaction.Count].Balance, accountModel.Id);
                            }
                            else
                            {
                                accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModel.Id);
                            }

                            dashBoard.gridLedger.ItemsSource = transaction;
                            dashBoard.gridAccounts.ItemsSource = accounts;

                        }
                    }
                }
            }
        }

        double balance;
        int nextItem;
        public double CalcBalanceRecursive(double startBalance, Transaction t, int count)
        {
            if (t.Count != 0)
            {
                if (count > t.Count - 1)
                {
                    balance = t[count - 1].Balance;
                }

                else if (count == 0)
                {
                    t[count].Balance = +startBalance;
                    t[count].Balance = CalculateBalance(Convert.ToDouble(t[count].Balance),
                        t[count]);


                    nextItem = count + 1;
                    CalcBalanceRecursive(startBalance, t, nextItem);
                }
                else
                {

                    t[nextItem].Balance = +t[nextItem - 1].Balance;
                    t[nextItem].Balance = CalculateBalance(Convert.ToDouble(t[nextItem].Balance),
                        t[nextItem]);


                    nextItem = count + 1;
                    CalcBalanceRecursive(startBalance, t, nextItem);
                }

            }
            return balance;
        }

        public void Transfer(string source, string dest, double amount, int sourceId, int destId, string comments)
        {


            LedgerModel ledger = new LedgerModel
            {
                Amount = amount,
                CatType = "Misc",
                Comments = comments,
                Name = "Transfer",
                TransType = "Withdrawal",
                Date = DateTime.Now,
                Auto = false,
                AccId = sourceId
            };


            AddNewTransaction(ledger, source);
            

            ledger.Amount = amount;
            ledger.CatType = "Misc";
            ledger.Comments = comments;
            ledger.Name = "Transfer";
            ledger.TransType = "Deposit";
            ledger.Date = DateTime.Now;
            ledger.Auto = false;
            ledger.AccId = destId;

            AddNewTransaction(ledger, dest);
           
        }

        public void UpdateAutoTransaction()
        {
            AccountVM accountVM = new AccountVM();
            Accounts accounts = new Accounts();
            LedgerModel ledger = new LedgerModel();
            Transaction transaction = new Transaction();
            bool done = false;

            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                if (conn.TableExist(User._username + "Accounts"))
                {
                    accounts = accountVM.LoadAccounts();

                    if (accounts.Count > 0)
                    {
                        foreach(AccountModel account in accounts)
                        {
                                while (!done)
                                {
                                transaction = LoadLedger(account.Id, account.AccountName, false, null);
                                if (transaction.Count > 0)
                                {
                                    foreach (LedgerModel l in transaction)
                                    {


                                        if (l.AutoDate.Month == DateTime.Now.AddMonths(1).Month && l.AutoDate.Year == DateTime.Now.Year
                                            && l.Auto == true)
                                        {
                                            done = true;
                                        }
                                        else if (l.Auto == true)
                                        {
                                            done = false;
                                        }


                                        if (l.Auto == true && l.AutoDate.Month <= DateTime.Now.Month && l.AutoDate.Year == DateTime.Now.Year)
                                        {

                                            ledger = l;
                                            ledger.Date = ledger.Date.AddMonths(1);
                                            ledger.AutoDate = ledger.AutoDate.AddMonths(1);
                                      
                                            AddNewTransaction(ledger, account.AccountName);

                                            ledgerTable.UpdateTransactAuto(false, User._username, account.AccountName, l.LedgerId);

                                        }
                                        else if (l.Auto == true && l.AutoDate.Year < DateTime.Now.Year) //Checks if there are older auto transactions from previous year
                                        {

                                            ledger = l;
                                            ledger.AutoDate = ledger.AutoDate.AddMonths(1);

                                            if (ledger.Date.Month != DateTime.Now.Month)
                                            {
                                                ledger.Date = ledger.Date.AddMonths(1);
                                            }

                                            AddNewTransaction(ledger, account.AccountName);

                                            ledgerTable.UpdateTransactAuto(false, User._username, account.AccountName, l.LedgerId);

                                        }
                                    }
                                    done = true;
                                }
                                else
                                {
                                    done = true;
                                }//end if
                            }//end while
                        }

                    }
                }
            }
        }

     

        public void LoadAccountData(DashBoardView dashBoard)
        {
            AccountVM accountVM = new AccountVM();
            Accounts accounts = accountVM.LoadAccounts();
            Transaction transaction = new Transaction();

            foreach (AccountModel account in accounts)
            {
                transaction = LoadLedger(account.Id, account.AccountName, false, null);
                if (transaction.Count > 0)
                {
                    account.Balance = CalcBalanceRecursive(account.StartingBalance, transaction, 0);
                    accountVM.UpdateBalance(account.Balance, account.Id);
                }
            }

            dashBoard.gridAccounts.ItemsSource = accounts;
        }

        public void UpdateAutoTransfer()
        {
            TransferList transferList = new TransferList();
            TransferTracker transferTracker = new TransferTracker();
            
            LedgerVM ledgerVM = new LedgerVM();
            double ammount = 0;

            //Check if transaction table exist also at the end of method 
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                if (conn.TableExist(User._username + "TransferTable"))
                {
                    transferList = transferTracker.LoadTransfers(false);

                    foreach (TransferModel t in transferList)
                    {
                        if (t.Auto == "True" && t.AutoDate.Month <= DateTime.Now.Month && t.AutoDate.Year <= DateTime.Now.Year) //<- create date check to make sure transaction is in proper month
                        {
                            Transaction transaction1 = ledgerVM.LoadLedger(t.SourceAcctId, t.Source, false, null);
                         

                            foreach (LedgerModel l in transaction1)
                            {
                                if (l.LedgerId == t.SourceId)
                                {
                                    ammount = l.Amount;
                                }
                            }

                            TransferTable transferTable = new TransferTable();

                           

                            Transfer(t.Source, t.Dest, ammount, t.SourceAcctId, t.DestAcctId,  
                                /*Load comments from old transaction and add it here*/ "Auto Transaction");

                            transaction1 = ledgerVM.LoadLedger(t.SourceAcctId, t.Source, false, null);
                            Transaction transaction2 = ledgerVM.LoadLedger(t.DestAcctId, t.Dest, false, null);

                            transferTable.AddTransfer(t.Source, t.Dest, t.SourceAcctId,
                                t.DestAcctId, transaction1[transaction1.Count-1].LedgerId, transaction2[transaction2.Count - 1].LedgerId,
                                DateTime.Now, DateTime.Now.AddMonths(1), User._userId, User._username, t.Auto);

                            //update origional transfer auto to false and reload list
                            transferTable.UpdateTransferAuto(false, User._username, t.Id);
                           
                        }

                    }

                }

            }

        }


        public void CreateTransfer(DashBoardView dashBoard)
        {
            LedgerModel ledgerModel = new LedgerModel();
            AccountModel source = (AccountModel)dashBoard.listSourceAccnt.SelectedItem;
            AccountModel dest = (AccountModel)dashBoard.listDestinationAccnt.SelectedItem;
            Transaction transaction = new Transaction();
            Transaction transaction2 = new Transaction();
            Accounts accounts = new Accounts();
            AccountVM accountVM = new AccountVM();
            AccountModel accountModel = AccountMediator.Account;
            TransferTable transferTable = new TransferTable();

           

            accounts = accountVM.LoadAccounts();

            Transfer(source.AccountName, dest.AccountName,
                Convert.ToDouble(dashBoard.tbTransferAmnt.Text), source.Id, dest.Id, dashBoard.tbTransferComments.Text);

            transaction = LoadLedger(source.Id, source.AccountName, false, null);
            transaction2 = LoadLedger(dest.Id, dest.AccountName, false, null);




            /*****************************************
             * Gets selected balance from account list
             *****************************************/
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == source.Id)
                {
                    source.Balance = accounts[i].StartingBalance;
                }

                if (accounts[i].Id == dest.Id)
                {
                    dest.Balance = accounts[i].StartingBalance;
                }
            }


            source.Balance = CalcBalanceRecursive(source.StartingBalance, transaction, 0);

            dest.Balance = CalcBalanceRecursive(dest.StartingBalance, transaction2, 0);

            if (transaction != null)
            {

                for (int i = 0; i < transaction.Count; i++)
                {
                    if (transaction[i].TransType == "Withdrawal")
                    {
                        transaction[i].Color = new SolidColorBrush(Colors.Red);
                    }
                    else if (transaction[i].TransType == "Deposit")
                    {
                        transaction[i].Color = new SolidColorBrush(Colors.SteelBlue);
                    }
                    else
                    {
                        transaction[i].Color = new SolidColorBrush(Colors.Green);
                    }
                }
            }

            if (transaction2 != null)
            {

                for (int i = 0; i < transaction2.Count; i++)
                {
                    if (transaction2[i].TransType == "Withdrawal")
                    {
                        transaction2[i].Color = new SolidColorBrush(Colors.Red);
                    }
                    else if (transaction2[i].TransType == "Deposit")
                    {
                        transaction2[i].Color = new SolidColorBrush(Colors.SteelBlue);
                    }
                    else
                    {
                        transaction2[i].Color = new SolidColorBrush(Colors.Green);
                    }
                }
            }

            if (transaction.Count == 0)
            {
                accountVM.UpdateBalance(transaction[transaction.Count].Balance, source.Id);
            }
            else
            {
                accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, source.Id);
            }


            if (transaction2.Count == 0)
            {
                accountVM.UpdateBalance(transaction2[transaction2.Count].Balance, dest.Id);
            }
            else
            {
                accountVM.UpdateBalance(transaction2[transaction2.Count - 1].Balance, dest.Id);
            }

            if (dashBoard.cbAutoTransfer.IsChecked.ToString() == "False")
            {
                transferTable.AddTransfer(source.AccountName, dest.AccountName, source.Id, dest.Id, transaction[transaction.Count - 1].LedgerId,
                    transaction2[transaction2.Count - 1].LedgerId, DateTime.Now, DateTime.Now, User._userId, 
                    User._username, dashBoard.cbAutoTransfer.IsChecked.ToString());
            }else
            {
                transferTable.AddTransfer(source.AccountName, dest.AccountName, source.Id, dest.Id, transaction[transaction.Count - 1].LedgerId,
              transaction2[transaction2.Count - 1].LedgerId, DateTime.Now, DateTime.Now.AddMonths(1), User._userId,
              User._username, dashBoard.cbAutoTransfer.IsChecked.ToString());
            }

            accounts = accountVM.LoadAccounts();
            dashBoard.gridAccounts.ItemsSource = accounts;

            if (AccountMediator.Account != null)
            {
                if (transaction[0].AccId == AccountMediator.Account.Id)
                {
                    dashBoard.gridLedger.ItemsSource = transaction;
                }
                else if (transaction2[0].AccId == AccountMediator.Account.Id)
                {
                    dashBoard.gridLedger.ItemsSource = transaction2;
                }
            }else
            {
                MessageBox.Show("Account not selected", "Error", MessageBoxButton.OK);
            }

        }


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
            }

            disposed = true;
        }
    
}

    public class Transaction : ObservableCollection<LedgerModel>
    {
        public Transaction() { }
        public void AddTransaction(LedgerModel ledger)
        {
            this.Add(ledger);
        }
    }

    public class CatTypes : ObservableCollection<string>
    {
        public CatTypes()
        {
            Add("Gas");
            Add("Groceries");
            Add("Entertainment");
            Add("Home");
            Add("Utilities");
            Add("Bills");
            Add("Vehicle");
            Add("Other");
        }
    }

    public class TransactType : ObservableCollection<string>
    {
        public TransactType()
        {
            Add("Withdrawal");
            Add("Deposit");
        }
    }

    public class ExpenseBreakdownList : ObservableCollection<ExpenseBreakdownModel>
    {
        public ExpenseBreakdownList()
        { }
    }
}
