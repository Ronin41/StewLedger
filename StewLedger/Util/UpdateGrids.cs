using StewLedger.Model;
using StewLedger.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StewLedger.Util
{
    public class UpdateGrids
    {
        AccountVM accountVM = new AccountVM();

        public void Update(View.DashBoardView dashBoard)
        {
            Accounts accounts = accountVM.LoadAccounts();
            ///////////////////////////////////////////////////
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

                        transaction[i].Balance = ledgerVM.CalcBalanceRecursive(accountModel.StartingBalance, transaction, 0);//ledgerVM.CalculateBalance(Convert.ToDouble(accountModel.Balance), transaction[i]);
                        accountModel.Balance = transaction[i].Balance;

                    }

                    for (int i = 0; i < accounts.Count; i++)
                    {
                        if (accounts[i].Id == accountModel.Id)
                        {
                            accounts[i].Balance = accountModel.Balance;
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

                //Adds last balance to table
                if (transaction.Count == 0)
                {
                    //accountVM.UpdateBalance(transaction[transaction.Count].Balance, accountModel.Id);
                }
                else
                {
                    accountVM.UpdateBalance(transaction[transaction.Count - 1].Balance, accountModel.Id);
                }


                dashBoard.gridAccounts.ItemsSource = accounts;
                dashBoard.gridLedger.ItemsSource = transaction;
            }


            ///////////////////////////////////////////////

        }

    }
}
