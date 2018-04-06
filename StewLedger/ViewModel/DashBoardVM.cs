using StewLedger.Data;
using StewLedger.Model;
using StewLedger.Tables.Accounts;
using StewLedger.Util;
using StewLedger.View;
using System;
using System.Windows;
using System.Windows.Media;

namespace StewLedger.ViewModel
{
    public class DashBoardVM : Notifier
    {
        private AccountVM accountVM;
        private UserAccountVM userAccountVM;
        private LedgerVM ledgerVM; 
        private Accounts accounts;

        public Accounts Accounts { get => accounts; set { accounts = value; OnPropertyChanged("Accounts"); } }


        public DashBoardVM()
        {
            accountVM = new AccountVM();
            userAccountVM = new UserAccountVM();
            ledgerVM = new LedgerVM();
            LoadAccounts();
        }


        public void LoadAccounts()
        {
            Accounts = accountVM.LoadAccounts();
        }

        public void DeleteAccounts(object parameter)
        {
            accountVM.DeleteAccount((View.DashBoardView)parameter);
        }

        public void DeleteUserAccounts(object parameter)
        {
            MainWindow mw = new MainWindow();
            userAccountVM.DeleteUserAccount((View.DashBoardView)parameter, mw);
        }

        /// <summary>
        /// Loads account ledger grid items from selected account
        /// </summary>
        /// <param name="parameter"></param>
        private void SelectionChanged(object parameter)
        {
         
        }


        private void CreateTransaction(object parameter)
        {
            if (AccountMediator.Account.AccountName != null)
            {
                ledgerVM.CreateTransaction((View.DashBoardView)parameter);
            }
            else
            {
                MessageBox.Show("Select Account", "Error", MessageBoxButton.OK);
            }
        }

        private void DeleteTransaction(object parameter)
        {
            ledgerVM.DeleteTransaction((View.DashBoardView)parameter);
        }

        private void CreateTransfer(object parameter)
        {
            ledgerVM.CreateTransfer((View.DashBoardView)parameter);
        }

        private void OpenNewAccountWnd(object parameter)
        {
            AccountCreation ac = new AccountCreation();
            
            DashBoardView dashBoard = (DashBoardView)parameter;

            ac.ShowDialog();

            Transaction transaction = new Transaction();
            LedgerVM ledgerVM = new LedgerVM();
            AccountModel accountModel = AccountMediator.Account;

            if (accountModel != null)
            {
                if (accountModel.AccountName != null)
                {

                    transaction = ledgerVM.LoadLedger(accountModel.Id, accountModel.AccountName, false, null);

                    for (int i = 0; i < accounts.Count; i++)
                    {
                        if (accounts[i].Id == accountModel.Id)
                        {
                            accountModel.Balance = accounts[i].StartingBalance;
                        }
                    }

                    for (int i = 0; i < transaction.Count; i++)
                    {

                        transaction[i].Balance = ledgerVM.CalculateBalance(Convert.ToDouble(accountModel.Balance), transaction[i]);
                        accountModel.Balance = transaction[i].Balance;

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
            }

            Accounts = GlobalAccounts._accounts; 

            dashBoard.gridAccounts.ItemsSource = Accounts;
            dashBoard.gridLedger.ItemsSource = transaction;
        }

        private void OpenNewUpdateTransferWnd(object parameter)
        {
        //////////////////////////////////////////////////////////////////
            AutoTransactView view = new AutoTransactView();
            
            DashBoardView dashBoard = (DashBoardView)parameter;

            view.ShowDialog();
        /////////////////////////////////////////////////////////////////


            Transaction transaction = new Transaction();
            Transaction transaction2 = new Transaction();
            LedgerVM ledgerVM = new LedgerVM();
            AccountModel accountModelSource = new AccountModel();
            AccountModel accountModelDest = new AccountModel();
            TransferList transferList = new TransferList();
            TransferTracker transferTracker = new TransferTracker();

            transferList = transferTracker.LoadTransfers(true);


            if (transferList != null)
            {
                foreach (TransferModel t in transferList)
                {

                    accountModelSource.AccountName = t.Source;
                    accountModelSource.Id = t.SourceAcctId;

                    accountModelDest.AccountName = t.Dest;
                    accountModelDest.Id = t.DestAcctId;


                            transaction = ledgerVM.LoadLedger(accountModelSource.Id, accountModelSource.AccountName, false, null);

                            for (int i = 0; i < Accounts.Count; i++)
                            {
                                if (Accounts[i].Id == accountModelSource.Id)
                                {
                                accountModelSource.Balance = Accounts[i].StartingBalance;
                                }
                            }

                            for (int i = 0; i < transaction.Count; i++)
                            {
                                transaction[i].Balance = ledgerVM.CalculateBalance(Convert.ToDouble(accountModelSource.Balance), transaction[i]);
                                accountModelSource.Balance = transaction[i].Balance;
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

                    ////////////////////////////////////////////////////////////////////////////////////

                    transaction2 = ledgerVM.LoadLedger(accountModelDest.Id, accountModelDest.AccountName, false, null);

                    for (int i = 0; i < Accounts.Count; i++)
                    {
                        if (Accounts[i].Id == accountModelDest.Id)
                        {
                            accountModelDest.Balance = Accounts[i].StartingBalance;
                        }
                    }

                    for (int i = 0; i < transaction2.Count; i++)
                    {
                        transaction2[i].Balance = ledgerVM.CalculateBalance(Convert.ToDouble(accountModelDest.Balance), transaction2[i]);
                        accountModelDest.Balance = transaction2[i].Balance;
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

                }
            }



            if (AccountMediator.Account != null)
            {
                
                    if (transaction.Count > 0)
                    {
                        accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModelSource.Id);
                    }
                    else
                    {
                        accountVM.UpdateBalance(accountModelSource.StartingBalance, accountModelSource.Id);
                    }

                   
                
                    if (transaction2.Count > 0)
                    {
                        accountVM.UpdateBalance(transaction2[transaction2.Count - 1].Balance, accountModelDest.Id);
                    }
                    else
                    {
                        accountVM.UpdateBalance(accountModelDest.StartingBalance, accountModelDest.Id);
                    }


                if (AccountMediator.Account.Id == accountModelSource.Id)
                {
                    dashBoard.gridLedger.ItemsSource = transaction;
                }
                else if (AccountMediator.Account.Id == accountModelDest.Id)
                {
                    dashBoard.gridLedger.ItemsSource = transaction2;
                }

            }

            Accounts = accountVM.LoadAccounts();

           
            dashBoard.gridAccounts.ItemsSource = Accounts;

        }

        private RelayCommand<DashBoardView> deleteAccounts = null;
        private RelayCommand<DashBoardView> selectionChange = null;
        private RelayCommand<DashBoardView> deleteUserAccount = null;
        private RelayCommand<DashBoardView> createTransactionCmd = null;
        private RelayCommand<DashBoardView> deleteTransactionCmd = null;
        private RelayCommand<DashBoardView> createTransferCmd = null;
        private RelayCommand<DashBoardView> openNewAccountWnd = null;
        private RelayCommand<DashBoardView> openNewUpdateTransferWnd = null;


        public RelayCommand<DashBoardView> OpenNewAccountWndCmd => openNewAccountWnd ??
            (openNewAccountWnd = new RelayCommand<DashBoardView>(OpenNewAccountWnd, CanOpenNewAccountWnd));


        public RelayCommand<View.DashBoardView> DeleteAccount => deleteAccounts ??
            (deleteAccounts = new RelayCommand<View.DashBoardView>(DeleteAccounts, CanDeleteAccount));

        public RelayCommand<View.DashBoardView> SelectionChange => selectionChange ??
            (selectionChange = new RelayCommand<View.DashBoardView>(SelectionChanged, CanSelectionChange));

        public RelayCommand<View.DashBoardView> DeleteUserAcount => deleteUserAccount ??
            (deleteUserAccount = new RelayCommand<View.DashBoardView>(DeleteUserAccounts, CanDeleteUserAccount));

        public RelayCommand<View.DashBoardView> CreateTransactionCmd => createTransactionCmd ??
            (createTransactionCmd = new RelayCommand<View.DashBoardView>(CreateTransaction, CanCreateTransaction));
        public RelayCommand<View.DashBoardView> DeleteTransactionCmd => deleteTransactionCmd ??
            (deleteTransactionCmd = new RelayCommand<View.DashBoardView>(DeleteTransaction, CanDeleteTransaction));
        public RelayCommand<View.DashBoardView> CreateTransferCmd => createTransferCmd ??
           (createTransferCmd = new RelayCommand<View.DashBoardView>(CreateTransfer, CanCreateTransfer));

        public RelayCommand<DashBoardView> OpenNewUpdateTransferWndCmd => openNewUpdateTransferWnd ??
            (openNewUpdateTransferWnd = new RelayCommand<DashBoardView>(OpenNewUpdateTransferWnd, CanOpenNewUpdateTransferWnd));



        private bool CanSelectionChange(View.DashBoardView account) => account != null;
        private bool CanDeleteAccount(View.DashBoardView account) => account != null;
        private bool CanDeleteUserAccount(View.DashBoardView userAccount) => userAccount != null;
        private bool CanCreateTransfer(View.DashBoardView ledger) => ledger != null;
        private bool CanCreateTransaction(View.DashBoardView ledger) => ledger != null;
        private bool CanDeleteTransaction(View.DashBoardView ledger) => ledger != null;
        private bool CanOpenNewAccountWnd(DashBoardView accountWnd) => accountWnd != null;

        private bool CanOpenNewUpdateTransferWnd(DashBoardView transferWnd) => transferWnd != null;


    }
}
