using StewLedger.Model;
using StewLedger.Util;
using StewLedger.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StewLedger.ViewModel
{
    public class AccountCreationVM : Notifier
    {
        private AccountModel accountModel;
        public AccountsNotifier accountNotifier = new AccountsNotifier();
        private LedgerVM ledgerVM;
        private BankVM bankVM;
        private AccountCreationModel acctCreationModel;
        private BankModel bank;
        private BankList bankList;
        Accounts newAccounts;
        AccountsArgs args = new AccountsArgs();
        public AccountCreationVM()
        {
            accountModel = new AccountModel();
            AcctCreationModel = new AccountCreationModel();
         
            bankVM = new BankVM();
            ledgerVM = new LedgerVM();
            Bank = new BankModel();
            BankList = new BankList();

            BankList = bankVM.LoadBanks();
        }

        public Accounts Accounts { get => newAccounts; set { newAccounts = value;
                args.Accounts = value;  accountNotifier.OnAccountsChanged(this, args); } }

        public AccountCreationModel AcctCreationModel { get => acctCreationModel;
            set { acctCreationModel = value; OnPropertyChanged("AcctCreationModel"); } }

        public BankList BankList { get => bankList; set
            {
                bankList = value;
                OnPropertyChanged("BankList");
            }
        }

        public BankModel Bank
        {
            get => bank;
            set { bank = value; OnPropertyChanged("Bank"); }
        }

        private void CreateAccounts()
        {

            try
            {
                accountModel.AccountName = AcctCreationModel.AccountName;
                accountModel.AccountType = AcctCreationModel.AccountType;
                accountModel.StartingBalance = Convert.ToDouble(AcctCreationModel.Balance);
                accountModel.Balance = Convert.ToDouble(AcctCreationModel.Balance);
                accountModel.BankId = Bank.Id;
                accountModel.AccountNumber = AcctCreationModel.AccountNumber;
                ledgerVM.AddNewLedger(User._username, accountModel.AccountName);

                using (AccountVM accountVM = new AccountVM())
                {
                    accountVM.AddNewAccount(accountModel);

                    accountNotifier.accountsChanged += AccountsMediator.BroadcastAccounts;

                    Accounts = accountVM.LoadAccounts();
                }

                MessageBox.Show("Account Added","Success", MessageBoxButton.OK);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK);
            }

          
        }


        private RelayCommand createAccount = null;

        public RelayCommand CreateAccountCmd => createAccount ??
            (createAccount = new RelayCommand(CreateAccounts, CanCreateAccount));



        private bool CanCreateAccount() => true;


    }
}
