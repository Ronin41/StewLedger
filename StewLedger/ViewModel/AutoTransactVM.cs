using StewLedger.Data;
using StewLedger.Model;
using StewLedger.Tables.Accounts;
using StewLedger.Util;
using StewLedger.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StewLedger.ViewModel
{

    public class TransactModel : Notifier
    {
        private string name;
        private int id;

        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
    }

    public class AutoTransactVM : Notifier
    {
        LedgerVM ledgerVM = new LedgerVM();
        Transaction transaction;

        AccountVM accountVM = new AccountVM();
        Accounts accounts = new Accounts();

        List<TransactModel> accountInfo;

        private TransactModel accountItem;
        private TransactModel selectedAccountItem;
        private TransferList transferList;
        private TransferTracker transferTracker = new TransferTracker();


        public List<TransactModel> AccountInfo { get => accountInfo; set { accountInfo = value;  } }

        public TransactModel AccountItem { get => accountItem; set { accountItem = value; } }
        public TransactModel SelectedAccountItem
        { get => selectedAccountItem;
            set {
                try
                {
                    selectedAccountItem = value;
                    LoadTransact(value.Id, value.Name);
                    //LoadTransfers();
                }catch(Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
              
            }
        }

        public TransferList TransferList
        {
            get => transferList;

            set
            {
                transferList = value;
                OnPropertyChanged("TransferList");
            }
        }


        public Transaction Transaction
        {
            get => transaction;
            set
            {
                transaction = value;
                OnPropertyChanged("Transaction");
            }
        }

  
        public AutoTransactVM()
        {
            accounts = accountVM.LoadAccounts();
            FillAccountList();
        }

        public void FillAccountList()
        {
            AccountInfo = new List<TransactModel>();
 
            foreach (AccountModel account in accounts)
            {
                AccountItem = new TransactModel();
                AccountItem.Name = account.AccountName;
                AccountItem.Id = account.Id;
                AccountInfo.Add(AccountItem);

            }    
        }

        private void LoadTransact(int id, string name)
        {
            this.Transaction = ledgerVM.LoadLedger(id, name, true, null);
        }

        public void LoadTransfers()
        {
            Transaction temp = new Transaction();
            
            this.TransferList = transferTracker.LoadTransfers(true);

            foreach(TransferModel t in TransferList)
            {
                temp = ledgerVM.LoadLedger(t.SourceAcctId, t.Source, false, t.SourceId);
                t.Amount = temp[0].Amount;
                t.Comments = temp[0].Comments;
            }
        }

        private void UpdateAutoTransact(AutoTransactView view)
        {
            LedgerTable ledgerTable = new LedgerTable();

            accounts = accountVM.LoadAccounts();

           DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure?", "Update Transaction", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (LedgerModel l in Transaction)
                    {
                        foreach (AccountModel a in accounts)
                        {
                            if (a.Id == l.AccId)

                            {
                                ledgerTable.UpdateTransactAuto(l.Auto, l.AutoDate, Convert.ToInt32(l.Amount),
                                    User._username, a.AccountName, Convert.ToInt32(l.LedgerId));
                            }
                        }
                    }

                    MessageBox.Show("Transaction Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),"Error", MessageBoxButtons.OK ,MessageBoxIcon.Error);
                }
            }

        }

        private void UpdateAutoTransfer(AutoTransactView view)
        {
            LedgerTable ledgerTable = new LedgerTable();
            TransferTable transferTable = new TransferTable();

            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure?", 
                "Update Transfer", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach(TransferModel t in TransferList)
                    {
                        transferTable.UpdateTransferAuto(Convert.ToBoolean(t.Auto), User._username, t.Id);
                        ledgerTable.UpdateLedgerTable(t.Comments, t.Amount, User._username, t.Source, t.SourceId);
                        ledgerTable.UpdateLedgerTable(t.Comments, t.Amount, User._username, t.Dest, t.DestId);

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

       }

        private RelayCommand<AutoTransactView> updateAutoTransact = null;
        private RelayCommand<AutoTransactView> updateAutoTransfer = null;


        public RelayCommand<AutoTransactView> UpdateAutoTransactCmd => updateAutoTransact ??
            (updateAutoTransact = new RelayCommand<AutoTransactView>(UpdateAutoTransact, CanUpdateAutoTransact));

        public RelayCommand<AutoTransactView> UpdateAutoTransferCmd => updateAutoTransfer ??
           (updateAutoTransfer = new RelayCommand<AutoTransactView>(UpdateAutoTransfer, CanUpdateAutoTransfer));



        private bool CanUpdateAutoTransact(AutoTransactView transactView) => transactView != null;
        private bool CanUpdateAutoTransfer(AutoTransactView transactView) => transactView != null;

    }

}
