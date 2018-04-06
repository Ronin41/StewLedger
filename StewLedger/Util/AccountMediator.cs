using StewLedger.Model;
using StewLedger.ViewModel;
using System;
using System.Collections.Generic;

namespace StewLedger.Util
{

    public class AccountsArgs : EventArgs
    {
        Accounts accounts;
        public Accounts Accounts { get => accounts; set => accounts = value; }
    }


    public class AccountArgs : EventArgs
    {
        AccountModel account;

        public AccountModel Account { get => account; set => account = value; }
    }


    public sealed class AccountsMediator
    {

        private static readonly AccountsMediator instance = new AccountsMediator();

        public static AccountsMediator Instance => instance;



        static public void BroadcastAccounts(object sender, EventArgs eventArgs)
        {
            AccountsArgs args = (AccountsArgs)eventArgs;
            GlobalAccounts._accounts = args.Accounts; 
        }


    }

    public sealed class AccountMediator
    {

        private static readonly AccountMediator instance = new AccountMediator();
        private static AccountModel account = new AccountModel();


        public static AccountMediator Instance => instance;

        public static AccountModel Account { get => account; set => account = value; }

        static public void BroadcastAccounts(object sender, EventArgs eventArgs)
        {
            AccountArgs args = (AccountArgs)eventArgs;

            Account = args.Account;
           // GlobalAccount._account = args.Account;
        }


    }
}
